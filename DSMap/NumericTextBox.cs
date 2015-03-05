using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DSMap
{
    public class NumericTextBox : TextBox
    {
        private NumberStyles numberStyle;
        private uint maxValue, minValue;

        public NumericTextBox()
        {
            numberStyle = NumberStyles.Decimal;
            maxValue = uint.MaxValue - 1;
            minValue = 0;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Limit Key presses
            if (numberStyle == NumberStyles.Decimal)
            {
                if (char.IsDigit(e.KeyChar)) { }
                else if (e.KeyChar == '\b') { }
                else e.Handled = true;
            }
            else if (numberStyle == NumberStyles.Hexadecimal)
            {
                if (char.IsDigit(e.KeyChar)) { }
                else if (e.KeyChar == '\b') { }
                else if (e.KeyChar >= 'a' && e.KeyChar <= 'f') { }
                else if (e.KeyChar >= 'A' && e.KeyChar <= 'F') { }
                else if (e.KeyChar == 'x' && Text.StartsWith("0") && TextLength == 1) { }
                else e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // Max/min
            if (Value > maxValue)
            {
                Value = maxValue;
            }
            else if (Value < minValue)
            {
                Value = minValue;
            }

            // Yeah...
            base.OnTextChanged(e);
        }

        [Description("Gets or sets the number base used by the TextBox."), DefaultValue(NumberStyles.Decimal)]
        public NumberStyles NumberStyle
        {
            get { return numberStyle; }
            set
            {
                uint val = Value;
                numberStyle = value;
                Value = val; // Yeah.
            }
        }

        public uint Value
        {
            get
            {
                if (TextLength > 0)
                {
                    // Safe-check the characters. There are better ways.
                    for (int i = 0; i < TextLength; i++)
                    {
                        if (numberStyle == NumberStyles.Hexadecimal)
                        {
                            if (char.IsDigit(Text[i])) { }
                            else if (Text[i] >= 'a' && Text[i] <= 'f') { }
                            else if (Text[i] >= 'A' && Text[i] <= 'F') { }
                            else if (Text[i] == 'x' || Text[i] == 'X' && i == 1) { }
                            else return 0;
                        }
                        else if (numberStyle == NumberStyles.Decimal)
                        {
                            if (char.IsDigit(Text[i])) { }
                            else return 0;
                        }
                        else return 0;
                    }

                    if (numberStyle == NumberStyles.Decimal) return Convert.ToUInt32(Text, 10);
                    else if (numberStyle == NumberStyles.Hexadecimal)
                    {
                        if (Text == "0x") return 0;
                        else return Convert.ToUInt32(Text, 16);
                    }
                    else return 0;
                }
                else return 0;
            }
            set
            {
                if (numberStyle == NumberStyles.Decimal) Text = value.ToString();
                else if (numberStyle == NumberStyles.Hexadecimal) Text = "0x" + value.ToString("X");
            }
        }

        [Description("Gets or sets the maximum value allowed by the TextBox."), DefaultValue(uint.MaxValue - 1)]
        public uint MaximumValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        [Description("Gets or sets the minimum value allowed by the TextBox."), DefaultValue((uint)0)]
        public uint MinimumValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        public enum NumberStyles : int
        {
            Decimal = 10, Hexadecimal = 16
        }
    }

    public class SignedNumericTextBox : TextBox
    {
        private int maxValue, minValue;

        public SignedNumericTextBox()
        {
            maxValue = int.MaxValue - 1;
            minValue = int.MinValue + 1;
            //Value = 0;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Limit Key presses
            if (char.IsDigit(e.KeyChar)) { }
            else if (e.KeyChar == '\b') { }
            else if (e.KeyChar == '-') { }
            else e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Value > MaxValue)
            {
                Value = MaxValue;
            }
            else if (Value < MinValue)
            {
                Value = MinValue;
            }

            base.OnTextChanged(e);
        }

        public int Value
        {
            get
            {
                if (TextLength > 0 && Text != "-")
                {
                    // Safe-check the characters. There should be a better way.
                    for (int i = 0; i < TextLength; i++)
                    {
                        if (char.IsDigit(Text[i])) { }
                        else if (Text[i] == '-' && i == 0) { }
                        else return 0;
                    }

                    return Convert.ToInt32(Text, 10);
                }
                else return 0;
            }
            set
            {
                Text = value.ToString();
            }
        }

        [Description("Gets or sets the maximum value allowed by the TextBox."), DefaultValue(int.MaxValue - 1)]
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        [Description("Gets or sets the minimum value allowed by the TextBox."), DefaultValue(int.MinValue + 1)]
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
    }
}
