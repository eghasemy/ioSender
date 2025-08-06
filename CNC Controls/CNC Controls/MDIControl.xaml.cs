/*
 * MDIControl.xaml.cs - part of CNC Controls library for Grbl
 *
 * v0.46 / 2024-12-27 / Io Engineering (Terje Io)
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

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CNC.Core;
using CNC.GCode;

using Avalonia.Interactivity;
namespace CNC.Controls
{
    public partial class MDIControl : UserControl
    {
        public MDIControl()
        {
            InitializeComponent();

            Commands = new ObservableCollection<string>();
        }

        public new bool IsFocused { get { return txtMDI.IsKeyboardFocusWithin; } }

                public static readonly StyledProperty<string> CommandProperty = AvaloniaProperty.Register<MDIControl, string>(nameof(Command), string.Empty);
        public string Command
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

                public static readonly StyledProperty<ObservableCollection<string>> CommandsProperty = AvaloniaProperty.Register<MDIControl, ObservableCollection<string>>(nameof(Commands), default);
        public ObservableCollection<string> Commands
        {
            get { return GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }

        private void OnDataContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is GrblViewModel) switch (e.PropertyName)
            {
                case nameof(GrblViewModel.MDIText):
                    var txt = (sender as GrblViewModel).MDIText;
                    if (!string.IsNullOrEmpty(txt) && !Commands.Contains(txt))
                        Commands.Insert(0, txt);
                    Command = txt;
                    break;
            }
        }

        private void txtMDI_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && (DataContext as GrblViewModel).MDICommand.CanExecute(null))
            {
                string cmd = (sender as ComboBox).SelectedItem?.ToString() ?? "";
                var model = DataContext as GrblViewModel;
                if (!string.IsNullOrEmpty(cmd) && (Commands.Count == 0 || Commands[0] != cmd))
                    Commands.Insert(0, cmd);
                if (model.GrblError != 0)
                    model.ExecuteCommand("");
                model.MDICommand.Execute(cmd);
                (sender as ComboBox).SelectedIndex = -1;
            }
        }

        private void txtMDI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Avalonia doesn't support Template.FindName, skip this functionality for now
            if (sender is ComboBox comboBox)
            {
                comboBox.Focus();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as GrblViewModel).GrblError != 0)
                (DataContext as GrblViewModel).ExecuteCommand("");

            if (!string.IsNullOrEmpty(Command) && !Commands.Contains(Command))
                Commands.Insert(0, Command);
        }

        private void MDIControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Avalonia doesn't support Template.FindName, skip this functionality for now
            if(DataContext != null && DataContext is GrblViewModel)
                (DataContext as GrblViewModel).PropertyChanged += OnDataContextPropertyChanged;
        }
    }
}
