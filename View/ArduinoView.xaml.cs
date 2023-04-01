namespace Arduino.WPF.View
{
    using Arduino.WPF.ViewModel;

    using System.Windows.Controls;
    /// <summary>
    /// Interaction logic for ArduinoView.xaml
    /// </summary>
    public partial class ArduinoView : Page
    {
        public ArduinoView()
        {
            InitializeComponent();
            this.DataContext = new ArduinoViewModel();
        }
    }
}
