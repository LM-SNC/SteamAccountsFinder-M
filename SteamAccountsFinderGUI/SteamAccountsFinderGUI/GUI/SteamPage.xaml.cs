using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using SteamAccountsFinder;
using SteamAccountsFinder.AccountsFindMethods;
using SteamAccountsFinder.LocationFindMethods;

namespace SteamAccountsFinderGUI
{
    public partial class SteamPage : Page
    {
        private readonly CornerRadius[] _cornerRadii =
        {
            new (0, 0, 8, 8),
            new (8, 8, 8, 8)
        };
        
        private int _previewOffset = 0;
        public SteamPage()
        {
            InitializeComponent();
            AnimateBar();
            UpdateAccounts();

            // accountsList.ItemsSource = steamAccounts;

            // steamAccounts.Add(new SteamAccount()
            // {
            //     SteamId64 = 223,
            //     ProfileLink = "https://hui.ru"
            // });



            // Task.Run((() =>
            // {
            //     while (steamAccounts == null)
            //     {
            //         Task.Delay(1000).Wait();
            //     }
            //     
            //     if (steamAccounts.Count > 0)
            //     {
            //         header.Visibility = Visibility.Visible;
            //         if (steamAccounts.Count > 9)
            //             accountsList.Margin = new Thickness(12, 0, 0, 0);
            //         accountsList.ItemsSource = steamAccounts;
            //         
            //         accountsList.UpdateLayout();
            //     }
            //     else
            //     {
            //         error.Visibility = Visibility.Visible;
            //     }
            // }));
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true; 
            myProcess.StartInfo.FileName = e.Uri.AbsoluteUri;
            myProcess.Start();
            e.Handled = true;
        }


        private void AccountsList_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int offset = (int)e.VerticalOffset;

            int offsetsResult = offset - _previewOffset;
            if (offsetsResult == 0)
                return;
            
            UpdateBorder(offset, _cornerRadii[0]);
            if (offsetsResult < -1)
                for (int i = offset+1; i <= _previewOffset; i++)
                    UpdateBorder(i, _cornerRadii[1]);
            else
                UpdateBorder(offset+1, _cornerRadii[1]);
            
            _previewOffset = offset;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.Source;
            if (!textBlock.Text.Equals("Сopied"))
            {
                try
                {
                    Clipboard.SetDataObject(textBlock.Text);
                    UpdateTextAnimation(textBlock);
                }
                catch (Exception exception)
                {
                    // ignored
                }
            }
        }

        private async void UpdateTextAnimation(TextBlock textBlock)
        {
            string baseValue = textBlock.Text;
            textBlock.Text = "Сopied";
            await Task.Delay(1000);
            textBlock.Text = baseValue;
        }
        
        private async void UpdateAccounts()
        {
            new Steam();
            AccountsFinder steamAccountFinder = new AccountsFinder();

            //Methods for finding a steam location
            steamAccountFinder.AddLocationMethod(new RegeditMethod(@"SOFTWARE\Valve\Steam1"));
            steamAccountFinder.AddLocationMethod(new DefaultPathMethod()
                .AddDefaultPath(@"С:\Program Files (x86)\Steam\steam.exe")
                .AddDefaultPath(@"E:\Steam\steam.exe")
                .AddDefaultPath(@"C:\Steam\steam.exe")
                .AddDefaultPath(@"F:\Steam\steam.exe")
                .AddDefaultPath(@"D:\Steam\steam.exe")
                .AddDefaultPath(@"G:\Steam\steam.exe")
                .AddDefaultPath(@"N:\Steam\steam.exe"));
            steamAccountFinder.AddLocationMethod(new BruteMethod());
            steamAccountFinder.Init();
            
            //Account search methods
            steamAccountFinder.AddAccountsMethod(new ALogsMethod());
            steamAccountFinder.AddAccountsMethod(new ARegeditMethod());
            steamAccountFinder.AddAccountsMethod(new AConfigFindMethod());
            steamAccountFinder.AddAccountsMethod(new AFoldersMethod());
            steamAccountFinder.AddAccountsMethod(new ACacheMethod());

            accountsList.ItemsSource = await steamAccountFinder.GetAccounts();


            loader.Value = 100;
            loader.Visibility = Visibility.Collapsed;

            if (accountsList.Items.Count > 0)
            {
                header.Visibility = Visibility.Visible;
                if (accountsList.Items.Count > 9)
                    accountsList.Margin = new Thickness(12, 0, 0, 0);
            }
            else
            {
                error.Visibility = Visibility.Visible;
            }
        }

        private async void AnimateBar()
        {
            DoubleAnimation animation = new DoubleAnimation(100, new Duration(new TimeSpan(0, 0, 5)));
            loader.BeginAnimation(ProgressBar.ValueProperty, animation);
        }
        private void UpdateBorder(int id, CornerRadius cornerRadius)
        {
            Border border = FindElementInVisualTree<Border>((ListBoxItem)accountsList.ItemContainerGenerator?
                    .ContainerFromItem(accountsList.Items[id]), "fBorder");

            if (border != null)
                border.CornerRadius = cornerRadius;
            
        }
        
        private T FindElementInVisualTree<T>(DependencyObject parentElement, string name) where T : DependencyObject
        {
            try
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parentElement); i++)
                {
                    var child = VisualTreeHelper.GetChild(parentElement, i);
                    if (((FrameworkElement)child).Name.Equals(name))
                        return (T)child;
                    
                    var result = FindElementInVisualTree<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
            return null;
        }
    }
}