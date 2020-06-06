using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView.Components
{
    /// <summary>
    /// A text box that only accepts numeric value
    /// </summary>
    public class NumericTextBox : TextBox
    {
        /// <summary>
        /// Get or Set if negative values are allowed
        /// </summary>
        public bool AllowNegative { get; set; }

        /// <summary>
        /// Get or Set if decimal values are allowed
        /// </summary>
        public bool AllowDecimal { get; set; }

        /// <summary>
        /// Sets the value that should be given if the numeric value is invalid. Default is 0.
        /// </summary>
        public decimal DefaultValue { get; set; }

        /// <summary>
        /// Get the numeric value of the textbox
        /// </summary>
        public decimal Value
        {
            get
            {
                string text = this.Text;
                decimal d;
                if(decimal.TryParse(text, out d))
                {
                    if( (d < 0 && AllowNegative) || d >= 0)
                    {
                        return d;
                    }
                }

                return DefaultValue;
            }
            set
            {
                if (AllowDecimal)
                {
                    if (AllowNegative || (!AllowNegative && value > 0))
                    {
                        this.Text = value.ToString();
                    }
                    else
                    {
                        this.Text = DefaultValue.ToString();
                    } 
                }
                else
                {
                    if (AllowNegative || (!AllowNegative && value > 0))
                    {
                        this.Text = ((int)value).ToString();
                    }
                    else
                    {
                        this.Text = ((int)DefaultValue).ToString();
                    }
                }
            }
        }

        public NumericTextBox(bool allowNegative, bool allowDecimal, decimal defaultValue = 0.0m)
        {
            this.AllowDecimal = allowDecimal;
            this.AllowNegative = allowNegative;
            this.DefaultValue = defaultValue;
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            bool result = true;

            bool numericKeys = (
                ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
                && e.Modifiers != Keys.Shift);

            bool ctrlA = e.KeyCode == Keys.A && e.Modifiers == Keys.Control;

            bool editKeys = (
                (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control) ||
                (e.KeyCode == Keys.X && e.Modifiers == Keys.Control) ||
                (e.KeyCode == Keys.C && e.Modifiers == Keys.Control) ||
                (e.KeyCode == Keys.V && e.Modifiers == Keys.Control) ||
                e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Back);

            bool navigationKeys = (
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Home ||
                e.KeyCode == Keys.End);

            bool period = (e.KeyCode == Keys.Decimal) || (e.KeyCode == Keys.OemPeriod);

            bool minus = e.KeyCode == Keys.OemMinus;

            if (!(numericKeys || editKeys || navigationKeys || (AllowDecimal && period) || (AllowNegative && minus)))
            {
                if (ctrlA)
                    SelectAll();
                result = false;
            }

            if (!result) // If not valid key then suppress and handle.
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
                
        }



    }
}
