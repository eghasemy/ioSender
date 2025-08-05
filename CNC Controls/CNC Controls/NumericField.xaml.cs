/*
 * NumericField.xaml.cs - part of CNC Controls library
 *
 * v0.43 / 2023-06-28 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018-2023, Io Engineering (Terje Io)
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

using Avalonia;
using Avalonia.Controls;
using CNC.Core;

namespace CNC.Controls
{
    /// <summary>
    /// Interaction logic for NumericField.xaml
    /// </summary>
    public partial class NumericField : UserControl
    {
        protected string format;
        protected bool metric = true, allow_dp = true, allow_sign = false;
        protected int precision = 3;

        public NumericField()
        {
            InitializeComponent();

            data.DataContext = this;
        }

        public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<NumericField, double>(nameof(Value), 0.0);
        public double Value
        {
            // TODO: Convert to Avalonia property getter - get { double v = (double)GetValue(ValueProperty); return double.IsNaN(v) ? 0d : v; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
        private static void OnValueChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if (double.IsNaN((double)e.NewValue))
                ((NumericField)d).data.Clear();
        }

                public static readonly StyledProperty<string> FormatProperty = AvaloniaProperty.Register<NumericField, string>(nameof(Format), string.Empty);
        public string Format
        {
            get { return GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

                public static readonly StyledProperty<string> LabelProperty = AvaloniaProperty.Register<NumericField, string>(nameof(Label), string.Empty);
        public string Label
        {
            get { return GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

                public static readonly StyledProperty<string> UnitProperty = AvaloniaProperty.Register<NumericField, string>(nameof(Unit), string.Empty);
        public string Unit
        {
            get { return GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

                public static readonly StyledProperty<string> Tooltip2Property = AvaloniaProperty.Register<NumericField, string>(nameof(Tooltip2), string.Empty);
        public string Tooltip2
        {
            get { return GetValue(Tooltip2Property); }
            set { SetValue(Tooltip2Property, value); }
        }

                public static readonly StyledProperty<bool> IsReadOnlyProperty = AvaloniaProperty.Register<NumericField, bool>(nameof(IsReadOnly), false);
        public bool IsReadOnly
        {
            get { return GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

                public static readonly StyledProperty<double> ColonAtProperty = AvaloniaProperty.Register<NumericField, double>(nameof(ColonAt), 0.0);
        public double ColonAt
        {
            get { return GetValue(ColonAtProperty); }
            set { SetValue(ColonAtProperty, value); }
        }
        private static void OnColonAtChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            ((NumericField)d).OnColonAtChanged();
        }
        private void OnColonAtChanged()
        {
            grid.ColumnDefinitions[0].Width = new GridLength(ColonAt);
        }

        public string Text
        {
            get { return Value.ToInvariantString(data.DisplayFormat); }
        }

        public static bool IsValidReading(object value)
        {
            double v = (double)value;
            return (!v.Equals(double.PositiveInfinity));
        }
                   
        public Control Field { get { return data; } }

        public void Clear()
        {
            data.Clear();
        }
    }
}

