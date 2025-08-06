using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
// using ToggleSwitch; // Removed third-party ToggleSwitch dependency for cross-platform build

using Avalonia.Data;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
// using System.Windows.Navigation;  // Platform-specific in Avalonia
using Avalonia.Controls.Shapes;

namespace CNC.Controls
{
    /// <summary>
    /// Interaction logic for ToggleControl.xaml
    /// </summary>
    public partial class ToggleControl : UserControl
    {

        public event EventHandler<RoutedEventArgs>? Click;

        public ToggleControl()
        {
            InitializeComponent();

            tsw.Click += tsw_Click;
        }

        private void tsw_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

                public static readonly StyledProperty<string> LabelProperty = AvaloniaProperty.Register<ToggleControl, string>(nameof(Label), string.Empty);
        public string Label
        {
            get { return GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

                public static readonly StyledProperty<bool> IsCheckedProperty = AvaloniaProperty.Register<ToggleControl, bool>(nameof(IsChecked), false);
        public bool IsChecked
        {
            get { return GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
    }
}
