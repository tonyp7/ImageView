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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            this.workingData = workingData;
        }

        private void FrmInformation_Load(object sender, EventArgs e)
        {
            //file
            dgvFile.Rows.Add("Name", workingData.fileInfo.Name);
            dgvFile.Rows.Add("Dimensions", String.Format("{0} x {1}", workingData.image.Width, workingData.image.Height));
            dgvFile.Rows.Add("Pixel Format", Program.HumanReadablePixelFormat(workingData.image.PixelFormat));
            dgvFile.Rows.Add("Size (bytes)", workingData.fileInfo.Length.ToString());
            dgvFile.Rows.Add("Created", workingData.fileInfo.CreationTime.ToString());
            dgvFile.Rows.Add("Last Written", workingData.fileInfo.LastWriteTime.ToString());
            dgvFile.Rows.Add("Path", workingData.fileInfo.DirectoryName);
            dgvFile.Rows.Add("File Attributes", workingData.fileInfo.Attributes.ToString());


            //exif
            foreach (PropertyItem property in workingData.propertyItems)
            {
                object propValue;
                switch ((PropertyTagType)property.Type)
                {
                    case PropertyTagType.ASCII:
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        propValue = encoding.GetString(property.Value, 0, property.Len - 1);
                        break;
                    case PropertyTagType.Int16:
                        propValue = BitConverter.ToUInt16(property.Value, 0);
                        break;
                    case PropertyTagType.SLONG:
                        propValue = BitConverter.ToInt32(property.Value, 0);
                        break;
                    case PropertyTagType.Int32:
                        propValue = BitConverter.ToUInt32(property.Value, 0);
                        break;
                    case PropertyTagType.SRational:
                        int sdividend = BitConverter.ToInt32(property.Value, 0);
                        int sdivisor = BitConverter.ToInt32(property.Value, 4);
                        if (sdivisor == 0)
                        {
                            propValue = double.NaN;
                        }
                        else
                        {
                            propValue = Math.Round(((double)sdividend / (double)sdivisor), 1);
                        }
                        break;
                    case PropertyTagType.Rational:
                        uint dividend = BitConverter.ToUInt32(property.Value, 0);
                        uint divisor = BitConverter.ToUInt32(property.Value, 4);
                        if(divisor == 0)
                        {
                            propValue = double.NaN;
                        }
                        else
                        {
                            propValue = Math.Round( ((double)dividend / (double)divisor), 1);
                        }
                        break;
                    case PropertyTagType.Undefined:
                    default:
                        propValue = String.Empty;
                        break;
                }

                dgvExif.Rows.Add((PropertyTagId)property.Id, propValue.ToString());
            }


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
