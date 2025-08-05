using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using ToggleSwitch;

using Avalonia.Data;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
// using System.Windows.Navigation;  // Platform-specific in Avalonia
using Avalonia.Controls.Shapes;

namespace CNC.Controls
{
    /// <summary>
    /// Interaction logic for ToggleControl.xaml
    /// </summary>
    public partial class ToggleControl : UserControl
    {

        public event RoutedEventHandler Click;

        public ToggleControl()
        {
            InitializeComponent();

            tsw.Click += tsw_Click;
        }

        private void tsw_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        public static readonly DependencyProperty LabelProperty = null; // TODO: Convert to Avalonia StyledProperty
        public string Label
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }

        public static readonly DependencyProperty IsCheckedProperty = null; // TODO: Convert to Avalonia StyledProperty
        public bool IsChecked
        {
            get { /* TODO: Implement Avalonia property getter */ return default; }
            set { /* TODO: Implement Avalonia property setter */ }
        }
    }
}
