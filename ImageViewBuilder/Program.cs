using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace ImageViewBuilder
{
    class Program
    {
        public enum RETURN_CODE : int
        {
            OK = 0,
            ERR_SETUP_BUILD = 1,
            ERR_UNKNOWN = -1,
        }


        public enum FileType
        {
            PadFile,
            SetupFile,
            PortableFile
        }


        static Version getVersion()
        {
            Version v = new Version();

            //get run path
            string runPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //get assembly
            string assembly = "../../../../ImageView/Properties/AssemblyInfo.cs";
            assembly = Path.Combine(runPath, assembly);

            //get the build version
            
            using (StreamReader sr = new StreamReader(assembly))
            {
                while (sr.Peek() > 0)
                {
                    //[assembly: AssemblyVersion("1.4.1.0")]
                    string line = sr.ReadLine();

                    if (line.StartsWith("[assembly: AssemblyVersion(\""))
                    {
                        string[] versionValue = line.Split(new char[] { '"', '.' }, StringSplitOptions.RemoveEmptyEntries);

                        int major = int.Parse(versionValue[1]);
                        int minor = int.Parse(versionValue[2]);
                        int build = int.Parse(versionValue[3]);
                        int rev = int.Parse(versionValue[4]);
                        v = new Version(major, minor, build, rev);

                        break;
                    }
                }
            }

            return v;
        }

        static RETURN_CODE buildSetupFile(Version v, ref FileInfo setupFile)
        {

            Console.Write("Building setup file... ");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = Properties.Resources.NSISExePath;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = Properties.Resources.NSISScriptPath;
            startInfo.UseShellExecute = false;

            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string[] lines = output.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string setupFilename = String.Empty;
            foreach(string line in lines)
            {
                if (line.StartsWith("Output: "))
                {
                    setupFilename = line.Substring("Output: ".Length);
                    setupFilename = setupFilename.Trim('"');

                }
            }

            if(setupFilename != String.Empty)
            {
                setupFile = new FileInfo(setupFilename);

                Console.WriteLine(String.Format("{0} OK!", setupFilename));

                //rename setup file to have the version in filename
                string setupFileWithVersion = String.Format("ImageView_{0}.{1}.{2}_Setup.exe", v.Major, v.Minor, v.Build);
                setupFileWithVersion = Path.Combine(setupFile.Directory.FullName, setupFileWithVersion);
                setupFile.CopyTo(setupFileWithVersion, true);

                setupFile = new FileInfo(setupFileWithVersion);

                Console.WriteLine(String.Format("Setup file copied to: {0}", setupFileWithVersion));

                return RETURN_CODE.OK;
            }
            else
            {
                return RETURN_CODE.ERR_SETUP_BUILD;
            }

        }


        static RETURN_CODE createPortableApp(Version v, ref FileInfo portableAppFile)
        {

            string portableAppFilename = String.Format("ImageView_{0}.{1}.{2}_Portable.zip", v.Major, v.Minor, v.Build);
            string portableAppFullName = Path.Combine(Properties.Resources.PortableAppPath, "..", portableAppFilename);

            DirectoryInfo di = new DirectoryInfo(Properties.Resources.PortableAppPath);

            //clean up folder
            FileInfo[] exes = di.GetFiles("*.exe");
            foreach(FileInfo exe in exes)
            {
                exe.Delete();
            }

            //Move newest build there
            //FileInfo buildExe = new FileInfo(Path.Combine(Properties.Resources.ExeBuildPath, Properties.Resources.ExeFileName));
            //buildExe.CopyTo(Path.Combine(Properties.Resources.PortableAppPath, Properties.Resources.ExeFileName));

            //move stuff to be zipped
            string[] filter = new string[] { ".md", ".dll", ".exe" };
            string[] releaseFiles = Directory.EnumerateFiles(Properties.Resources.ExeBuildPath, "*.*", SearchOption.TopDirectoryOnly).Where(file => filter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
            foreach(string f in releaseFiles)
            {
                FileInfo fi = new FileInfo(f);
                string dstF = Path.Combine(Properties.Resources.PortableAppPath, fi.Name);
                fi.CopyTo(  dstF    , true);
                Console.WriteLine(String.Format("Copied file to {0}", dstF));
            }

            //Zip it -- but first delete previous
            File.Delete(portableAppFullName);
            ZipFile.CreateFromDirectory(Properties.Resources.PortableAppPath, portableAppFullName, CompressionLevel.Optimal, false);
            portableAppFile = new FileInfo(portableAppFullName);

            Console.WriteLine(String.Format("Created Portable Version: {0}", portableAppFile.FullName));

            return RETURN_CODE.OK;
        }

        static RETURN_CODE fileUpload(Version v, FileType fileType, FileInfo file)
        {
            Console.Write("Uploading file " + file.Name + "... ");

            WebClient client = new WebClient();

#if DEBUG
            string url = Properties.Resources.DEBUG_postFileURL;
            Uri uri = new Uri(url);
            var credentialCache = new CredentialCache();
            credentialCache.Add(
              new Uri(uri.GetLeftPart(UriPartial.Authority)),
              "Basic",
              new NetworkCredential(Properties.Resources.DEBUG_do_release_user, Properties.Resources.DEBUG_do_release_pwd)
            );
            client.UseDefaultCredentials = true;
            client.Credentials = credentialCache;
#else
            string url = Properties.Resources.postFileURL;
            Uri uri = new Uri(url);
            var credentialCache = new CredentialCache();
            credentialCache.Add(
              new Uri(uri.GetLeftPart(UriPartial.Authority)),
              "Basic",
              new NetworkCredential(Properties.Resources.do_release_user, Properties.Resources.do_release_pwd)
            );
            client.UseDefaultCredentials = true;
            client.Credentials = credentialCache;
#endif


            client.Headers.Add("X-Version", String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build ) );
            client.Headers.Add("X-Checksum", Hash( file  ) );
            client.Headers.Add("X-Filetype",  fileType.ToString() );
            byte[] response = client.UploadFile(url, "POST", file.FullName);
            string responseStr = Encoding.UTF8.GetString(response);
            Console.WriteLine(responseStr);

            client.Dispose();

            return RETURN_CODE.OK;
        }

        static RETURN_CODE updatePadFile(Version v, FileInfo setupFile, ref FileInfo padFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( Properties.Resources.PadFileFullName );

            //version
            XmlNode n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/Program_Version");
            n.InnerText = String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);

            //release date
            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/Program_Release_Month");
            n.InnerText = DateTime.Now.Month.ToString();
            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/Program_Release_Day");
            n.InnerText = DateTime.Now.Day.ToString();
            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/Program_Release_Year");
            n.InnerText = DateTime.Now.Year.ToString();

            //setup size
            long sz = setupFile.Length;
            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/File_Info/File_Size_Bytes");
            n.InnerText = sz.ToString();

            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/File_Info/File_Size_K");
            n.InnerText = (sz / 1024).ToString();

            n = doc.SelectSingleNode("/XML_DIZ_INFO/Program_Info/File_Info/File_Size_MB");
            n.InnerText = String.Format("{0:0.##}", ((double)sz / 1024.0 / 1024.0));

            doc.Save(Properties.Resources.PadFileFullName);

            Console.WriteLine(String.Format("Updated PAD file: {0}", Properties.Resources.PadFileFullName));

            padFile = new FileInfo(Properties.Resources.PadFileFullName);

            return RETURN_CODE.OK;
        }

        static string Hash(FileInfo fi)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(File.ReadAllBytes(fi.FullName));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        static void clearConsoleBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo c;
            Version v = getVersion();
            FileInfo setupFile = null;
            FileInfo portableAppFile = null;
            FileInfo padFile = null;

            //generate setup file
            buildSetupFile(v, ref setupFile);

            //Build the Portable app
            createPortableApp(v, ref portableAppFile);

            //Update the PAD file
            updatePadFile(v, setupFile, ref padFile);

            //end of fiddling with files, now proceed to upload if needed
            Console.Write("Would you like to upload new version (Y/n)? ");
            c = Console.ReadKey();
            if(c.KeyChar == 'y' || c.KeyChar == 'Y' || c.KeyChar == '\r')
            {
                Console.WriteLine(c.KeyChar);
                fileUpload(v, FileType.PadFile, padFile);
                fileUpload(v, FileType.SetupFile, setupFile);
                fileUpload(v, FileType.PortableFile, portableAppFile);
            }
            clearConsoleBuffer();


            Console.Write("Press any key to exit . . .");
            c = Console.ReadKey();


            return;

        }
    }
}
