/*
 * NumericTextBox.cs - part of CNC Controls library
 *
 * v0.45 / 2024-01-09 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018-2024, Io Engineering (Terje Io)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

· Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

· Redistributions in binary form must reproduce the above copyright notice, this
list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

· Neither the name of the copyright holder nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace CNC.Controls
{
    public class NumericTextBox : TextBox
    {
        private NumericProperties np = new NumericProperties();

        private bool updateText = true;

        public NumericTextBox()
        {
            Height = 24;
            HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Right;
            VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Bottom;
            TextWrapping = Avalonia.Media.TextWrapping.NoWrap;
            if (Format == NumericProperties.MetricFormat)
                NumericProperties.OnFormatChanged(this, np, Format);
                
            // Wire up text changed event handler
            this.TextChanged += OnTextChanged;
        }

        public new string Text { get { return base.Text; } set { base.Text = value; } }
        public NumberStyles Styles { get { return np.Styles; } }
        public string DisplayFormat { get { return np.DisplayFormat; } }

        public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<NumericTextBox, double>(nameof(Value), 0.0);
        public double Value
        {
            get { double v = GetValue(ValueProperty); return double.IsNaN(v) ? 0d : v; }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if (((NumericTextBox)d).updateText)
                ((NumericTextBox)d).Text = double.IsNaN((double)e.NewValue) || double.IsNegativeInfinity((double)e.NewValue) ? string.Empty : Math.Round((double)e.NewValue, ((NumericTextBox)d).np.Precision).ToString(((NumericTextBox)d).np.DisplayFormat, CultureInfo.InvariantCulture);
        }
        //        public static bool CoerceValueChanged(AvaloniaObject d, object value)
        //        {
        //            double v = (double)value;
        //            NumericTextBox ntb = (NumericTextBox)d;
        //            return get { return (double.IsNaN(ValueMin) || Value >= ValueMin) && (double.IsNaN(ValueMax) || Value <= ValueMax); }
        //;
        //        }

        // TODO: Convert FormatProperty to Avalonia StyledProperty  
        public static readonly StyledProperty<string> FormatProperty = AvaloniaProperty.Register<NumericTextBox, string>(nameof(Format), string.Empty);
        public string Format
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
        private static void OnFormatChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            NumericProperties.OnFormatChanged(d, ((NumericTextBox)d).np, (string)e.NewValue);
        }

        public new void Clear()
        {
            updateText = false;
            Value = double.NaN;
            updateText = true;
            base.Text = string.Empty;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                int selectionStart = SelectionStart;
                int selectionEnd = SelectionEnd;
                string text = selectionEnd > selectionStart ? Text.Remove(selectionStart, selectionEnd - selectionStart) : Text;

                updateText = false;
                Value = double.Parse(text == string.Empty || text == "." ? "0" : (text == "-" || text == "-." ? "-0" : text), np.Styles, CultureInfo.InvariantCulture);
                updateText = true;
            }
        }

        protected override void OnTextInput(Avalonia.Input.TextInputEventArgs e)
        {
            TextBox textBox = this;
            int selectionStart = textBox.SelectionStart;
            int selectionEnd = textBox.SelectionEnd;
            string text = selectionEnd > selectionStart ? textBox.Text.Remove(selectionStart, selectionEnd - selectionStart) : textBox.Text;
            text = text.Insert(selectionStart, e.Text);
            if (!(e.Handled = !NumericProperties.IsStringNumeric(text, np)))
            {
                updateText = false;
                Value = double.Parse(text == "" || text == "." ? "0" : (text == "-" || text == "-." ? "-0" : text), np.Styles, CultureInfo.InvariantCulture);
                updateText = true;
            }

            base.OnPreviewTextInput(e);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            double val = 0d;
            if (double.TryParse(Text == string.Empty ? "NaN" : Text, np.Styles, CultureInfo.InvariantCulture, out val))
            {
                if (!IsReadOnly && IsEnabled)
                {
                    updateText = false;
                    Value = val;
                    updateText = true;
                }

                // Don't call base.OnTextChanged for event handler
            }
            else if(Text == string.Empty || Text == ".")
            {
                updateText = false;
                Value = 0d;
                updateText = true;
            }
            else if (Text == "-" || Text == "-.")
            {
                updateText = false;
                Value = -0d;
                updateText = true;
            }
            else
                Text = Math.Round(Value, (np.Precision)).ToString(np.DisplayFormat, CultureInfo.InvariantCulture);
        }
    }
}
