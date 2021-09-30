using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FermBook
{
    /// <summary>
    /// Логика взаимодействия для WindowLoad.xaml
    /// </summary>
    public partial class WindowLoad : Window
    {
        public WindowLoad()
        {
            InitializeComponent();
        }

        public void UpdateProgressBar(UploadStatus status, long update)
        {
            this.Dispatcher.Invoke(() => {
                StatusGoogle.Content = status;
                ProgressBarGoogle.Value += update;
            });
        }

        public void EndProgressBar()
        {
            this.Dispatcher.Invoke(() => {
                ProgressBarGoogle.Value = 0;
                StatusGoogle.Content = "";
            });
            Mes.View("Файл загружен");
        }

        public void AvtivePanel(bool enabled)
        {
            this.Dispatcher.Invoke(() => {
                BodyGridLoad.IsEnabled = enabled;
                statusFileLabel.Content = "Файл доступен";
            });
        }

        private void CheckFileButton_Click(object sender, RoutedEventArgs e)
        {
            WindowCheckFileDB win = new WindowCheckFileDB();
            win.Owner = this;
            win.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarGoogle.Maximum = AppFuncG.UpLoadFileDB(this);
        }

        private void LoadingButton_Click(object sender, RoutedEventArgs e)
        {
            AvtivePanel(false);
            AppFuncG.DownloadFileDB(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
