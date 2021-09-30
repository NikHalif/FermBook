using System;
using System.Collections.Generic;
using System.Windows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Net;

namespace FermBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public MainWindow()
        {

            InitializeComponent();
            try
            {
                if (CheckForInternetConnection())
                {
                    if (Directory.Exists("token"))
                    {
                        AppFuncG.Auth();
                        AppFuncG.LoadPersone();
                        EnterActive.Content = $"{AppFuncG.People.Names[0].DisplayName}";
                        EnterActive.Visibility = Visibility.Visible;

                        ImageBrush Brush = new ImageBrush(new BitmapImage(
                            new Uri(AppFuncG.People.Photos[0].Url)));

                        EnterActive.Background = Brush;
                    }
                }
                else
                {
                    Mes.View("Доступ к интернету не найден.\nРезервное копирование недоступно.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    EnterGoogle.Visibility = Visibility.Hidden;
                    EnterActive.Visibility = Visibility.Hidden;
                }
                
            }
            catch(Exception e)
            {
                Mes.View(e.Message, e.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void EnterGoogle_Click(object sender, RoutedEventArgs e)
        {
            AllGrid.IsEnabled = false;
            try
            {
                if (Directory.Exists("token")) Directory.Delete("token", true);
                AppFuncG.Auth();
                AppFuncG.LoadPersone();
                ActiveProgram();
            }
            catch (Exception ex)
            {
                AllGrid.IsEnabled = true;
                Mes.View(ex.Message);
            }
        }

        private void EnterOffline_Click(object sender, RoutedEventArgs e)
        {
            AllGrid.IsEnabled = false;
            AppFuncG.IsOffline = true;
            WindowProgram window = new WindowProgram();
            window.Show();
            this.Close();
        }

        private void EnterActive_Click(object sender, RoutedEventArgs e)
        {
            AllGrid.IsEnabled = false;
            ActiveProgram();
        }

        private void ActiveProgram()
        {
            var win = new WindowLoad();
            win.Show();
            this.Close();
        }
    }
}
