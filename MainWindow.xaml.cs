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

namespace FermBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                if (Directory.Exists("token"))
                {
                    AppFuncG.Auth();

                    Google.Apis.PeopleService.v1.Data.Person person = Oauth2Authentication.GetPerson(AppFuncG.PService, "nicknames", "names", "photos");
                    EnterActive.Visibility = Visibility.Visible;

                    ImageBrush Brush = new ImageBrush(new BitmapImage(
                        new Uri(person.Photos[0].Url)));

                    EnterActive.Background = Brush;
                }
            }
            catch(Exception e)
            {
                Mes.View(e.Message);
            }
            
        }

        private void EnterGoogle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Directory.Exists("token"))  Directory.Delete("token", true);
                AppFuncG.Auth();
                AppFuncG.LoadPersone();
                ActiveProgram();
            }
            catch (Exception ex)
            {
                Mes.View(ex.Message);
            }
        }

        private void EnterOffline_Click(object sender, RoutedEventArgs e)
        {
            AppFuncG.IsOffline = true;
            ActiveProgram();
        }

        private void EnterActive_Click(object sender, RoutedEventArgs e)
        {
            AppFuncG.LoadPersone();
            ActiveProgram();
        }

        private void ActiveProgram()
        {
            WindowProgram window = new WindowProgram();
            window.Show();
            this.Close();
        }
    }
}
