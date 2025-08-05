/*
 * OverrideControl.xaml.cs - part of CNC Controls library
 *
 * v0.46 / 2025-05-13 / Io Engineering (Terje Io)
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
using Avalonia;
using Avalonia.Controls;
using CNC.Core;
using Avalonia.Input;

namespace CNC.Controls
{
    public partial class OverrideControl : UserControl
    {
        private double lastValue;

        public delegate void CommandGeneratedHandler(byte[] commands, int len);
        public event CommandGeneratedHandler CommandGenerated;

        public OverrideControl()
        {
            InitializeComponent();
        }

        public byte ResetCommand { set; get; }
        public byte FinePlusCommand { set; get; }
        public byte FineMinusCommand { set; get; }
        public byte CoarsePlusCommand { set; get; }
        public byte CoarseMinusCommand { set; get; }

        #region dependencyproperties

        public static readonly StyledProperty MinimumProperty = null; // TODO: Convert to Avalonia StyledProperty
        public int Minimum
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly StyledProperty MaximumProperty = null; // TODO: Convert to Avalonia StyledProperty
        public int Maximum
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly StyledProperty TicksProperty = null; // TODO: Convert to Avalonia StyledProperty
        public System.Windows.Media.DoubleCollection Ticks
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly StyledProperty TickFrequencyProperty = null; // TODO: Convert to Avalonia StyledProperty
        public int TickFrequency
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly StyledProperty SliderValueProperty = null; // TODO: Convert to Avalonia StyledProperty
        public double SliderValue
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
        private static void OnSliderValueChanged(AvaloniaObject d, StyledPropertyChangedEventArgs e)
        {
            ((OverrideControl)d).txtOverride.Text = Math.Round((double)e.NewValue).ToString() + "%";
        }

        public static readonly StyledProperty ValueProperty = null; // TODO: Convert to Avalonia StyledProperty
        public double Value
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
        private static void OnValueChanged(AvaloniaObject d, StyledPropertyChangedEventArgs e)
        {
            ((OverrideControl)d).SliderValue = Math.Round((double)e.NewValue);
        }

        public static readonly StyledProperty EncoderModeProperty = null; // TODO: Convert to Avalonia StyledProperty
        public GrblEncoderMode EncoderMode
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        #endregion

        private void Slider_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            int len = 0;
            byte[] cmd = new byte[30];

            if (FinePlusCommand == 0) // Rapids override
            {
                switch((int)SliderValue)
                {
                    case 25:
                        // TODO: Convert to Avalonia property setter - cmd.SetValue(CoarseMinusCommand, len++);
                        break;
                    case 50:
                        // TODO: Convert to Avalonia property setter - cmd.SetValue(FineMinusCommand, len++);
                        break;
                    default:
                        // TODO: Convert to Avalonia property setter - cmd.SetValue(ResetCommand, len++);
                        break;
                }
            } else {

                double coarseDelta = Math.Round(SliderValue) - Value, fineDelta = coarseDelta % 10d;
                byte coarseCmd = coarseDelta < 0d ? CoarseMinusCommand : CoarsePlusCommand,
                     fineCmd = fineDelta < 0d ? FineMinusCommand : FinePlusCommand;

                coarseDelta = Math.Abs(coarseDelta - fineDelta);
                fineDelta = Math.Abs(fineDelta);

                while (coarseDelta != 0d)
                {
                    // TODO: Convert to Avalonia property setter - cmd.SetValue(coarseCmd, len++);
                    coarseDelta -= 10d;
                }
                while (fineDelta != 0d)
                {
                    // TODO: Convert to Avalonia property setter - cmd.SetValue(fineCmd, len++);
                    fineDelta -= 1d;
                }
            }

            if(cmd.Length > 0)
                CommandGenerated?.Invoke(cmd, len);
        }

        void btnOverrideClick(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { ResetCommand };

            CommandGenerated?.Invoke(cmd, 1);
        }

        private void Slider_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            lastValue = Math.Round(Value);
        }
    }
}
