/*
 * TaperControl.xaml.cs - part of CNC Controls library
 *
 * v0.45 / 2024-04-21 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2019-2024, Io Engineering (Terje Io)
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
using Avalonia;
using Avalonia.Controls;

namespace CNC.Controls.Lathe
{
    /// <summary>
    /// Interaction logic for TaperControl.xaml
    /// </summary>
    public partial class TaperControl : UserControl
    {
        public Action<double> OnValueChanged;
        public Action<bool> OnTaperEnabledChanged;

        public TaperControl()
        {
            InitializeComponent();

            data.TextChanged += Data_TextChanged;
            chkTaper.Checked += ChkTaper_Checked;
            chkTaper.Unchecked += ChkTaper_Checked;
        }

        private void ChkTaper_Checked(object sender, RoutedEventArgs e)
        {
            OnTaperEnabledChanged?.Invoke(chkTaper.IsChecked == true);
        }

        private void Data_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnValueChanged?.Invoke(double.IsNaN(Value) ? 0d : Value);
        }

      //  public string Format { get { return data.Format;  } }


        public static readonly StyledProperty IsTaperEnabledProperty = null; // TODO: Convert to Avalonia StyledProperty
        public bool IsTaperEnabled
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly StyledProperty FormatProperty = null; // TODO: Convert to Avalonia StyledProperty
        public string Format
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        private static void OnFormatChanged(AvaloniaObject d, StyledPropertyChangedEventArgs e)
        {
            ((TaperControl)d).data.Format = (string)e.NewValue;
        }

        public static readonly StyledProperty ValueProperty = null; // TODO: Convert to Avalonia StyledProperty
        public double Value
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
    }
}
