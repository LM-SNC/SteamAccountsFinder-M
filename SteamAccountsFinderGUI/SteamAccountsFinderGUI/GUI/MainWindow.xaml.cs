using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SteamAccountsFinderGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            InitializeComponent();
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
                
            File.AppendAllText("debug.log", exception.ToString());
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((Button)e.Source).Visibility = Visibility.Collapsed;
            MainFrame.Content = new SteamPage();
        }
    }
}