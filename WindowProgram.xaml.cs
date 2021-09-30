using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.Entity;
using Google.Apis.Upload;

namespace FermBook
{
    /// <summary>
    /// Логика взаимодействия для WindowProgram.xaml
    /// </summary>
    public partial class WindowProgram : Window
    {
        private DatabaseSQLEntities context = new DatabaseSQLEntities();
        public WindowProgram()
        {
            InitializeComponent();
            this.Title = AppFuncG.ApplicationName;
            wrapGoogleAuth.IsEnabled = !AppFuncG.IsOffline;
            if (AppFuncG.IsOffline) LabelNameGoogle.Content = "Offline";
            else LabelNameGoogle.Content = AppFuncG.People.Names[0].DisplayName;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource cowsViewSource = (CollectionViewSource)FindResource("cowsViewSource");
            context.cows.Load();
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // cowsViewSource.Source = [универсальный источник данных]
            cowsViewSource.Source = context.cows.Local;

        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {


            Mes.View($"Затронуто строк {SaveBase()}");

            // Refresh the grids so the database generated values show up.
            cowsDataGrid.Items.Refresh();
            calveDataGrid.Items.Refresh();
            diseaseDataGrid.Items.Refresh();
            inseminationDataGrid.Items.Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
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

        protected int SaveBase()
        {
            foreach (var calve in context.calve.Local.ToList())
            {
                if (calve.cow_mam == null)
                {
                    context.calve.Remove(calve);
                }
            }
            foreach (var disease in context.disease.Local.ToList())
            {
                if (disease.cows == null)
                {
                    context.disease.Remove(disease);
                }
            }
            foreach (var insemination in context.insemination.Local.ToList())
            {
                if (insemination.cows == null)
                {
                    context.insemination.Remove(insemination);
                }
            }

            return context.SaveChanges();
        }

        private void buttonSaveGoogle_Click(object sender, RoutedEventArgs e)
        {
            //ProgressBarGoogle.Maximum = AppFuncG.UpLoadFileDB(this);
            //this.Close();
        }

        
    }
}
