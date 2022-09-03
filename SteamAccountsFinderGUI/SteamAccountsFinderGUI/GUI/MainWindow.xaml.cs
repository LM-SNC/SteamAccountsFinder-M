using System.Windows;
using System.Windows.Controls;

namespace SteamAccountsFinderGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((Button)e.Source).Visibility = Visibility.Collapsed;
            MainFrame.Content = new SteamPage();
        }
    }
}