/*
MIT License

Copyright (c) 2020 Tony Pottier

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

 */

using ImageMagick;
using ImageView.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmInformation : Form
    {
        private WorkingData workingData;
        public FrmInformation(WorkingData workingData)
        {
            InitializeComponent(); 
            InitalizeComponentsCultureAware();
            this.workingData = workingData;
        }


        public void InitalizeComponentsCultureAware()
        {
            var lang = Settings.Get.General;

            Text = lang.GetString("Information");
            tabPageFile.Text = lang.GetString("File");

            colProperty.HeaderText = lang.GetString("Property");
            colValue.HeaderText = lang.GetString("Value");
            colExifProperty.HeaderText = lang.GetString("Property");
            colExifValue.HeaderText = lang.GetString("Value");
        }

        private void FrmInformation_Load(object sender, EventArgs e)
        {


            //file
            dgvFile.Rows.Add("Name", workingData.activeEntry.Name);

            //image relation
            dgvFile.Rows.Add("Dimensions", String.Format("{0} x {1}", workingData.nativeImage.BaseWidth, workingData.nativeImage.BaseHeight));
            dgvFile.Rows.Add("Format", workingData.nativeImage.Format.ToString());
            dgvFile.Rows.Add("Color Space", workingData.nativeImage.ColorSpace.ToString()  );
            dgvFile.Rows.Add("Color Type", workingData.nativeImage.ColorType.ToString() );
            //dgvFile.Rows.Add("Unique colors", workingData.nativeImage.TotalColors.ToString() );  //very slow


            //file related
            dgvFile.Rows.Add("Size (bytes)", workingData.activeEntry.Length.ToString());
            dgvFile.Rows.Add("Created", workingData.activeEntry.CreationTime.ToString());
            dgvFile.Rows.Add("Last Written", workingData.activeEntry.LastWriteTime.ToString());
            dgvFile.Rows.Add("Path", workingData.activeEntry.DirectoryName);
            
            IExifProfile profile = workingData.nativeImage.GetExifProfile();
            if (profile != null)
            {
                foreach (IExifValue value in profile.Values)
                {
                    dgvExif.Rows.Add(value.Tag.ToString(), value.ToString());
                }
            }


            //            propValue = BitConverter.ToUInt16(property.Value, 0);
            //            break;
            //        case PropertyTagType.SLONG:
            //            propValue = BitConverter.ToInt32(property.Value, 0);
            //            break;
            //        case PropertyTagType.Int32:
            //            propValue = BitConverter.ToUInt32(property.Value, 0);
            //            break;
            //        case PropertyTagType.SRational:
            //            int sdividend = BitConverter.ToInt32(property.Value, 0);
            //            int sdivisor = BitConverter.ToInt32(property.Value, 4);
            //            if (sdivisor == 0)
            //            {
            //                propValue = double.NaN;
            //            }
            //            else
            //            {
            //                propValue = Math.Round(((double)sdividend / (double)sdivisor), 1);
            //            }
            //            break;
            //        case PropertyTagType.Rational:
            //            uint dividend = BitConverter.ToUInt32(property.Value, 0);
            //            uint divisor = BitConverter.ToUInt32(property.Value, 4);
            //            if(divisor == 0)
            //            {
            //                propValue = double.NaN;
            //            }
            //            else
            //            {
            //                propValue = Math.Round( ((double)dividend / (double)divisor), 1);
            //            }
            //            break;
            //        case PropertyTagType.Undefined:
            //        default:
            //            propValue = String.Empty;
            //            break;
            //    }

            //    dgvExif.Rows.Add((PropertyTagId)property.Id, propValue.ToString());
            //}


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
