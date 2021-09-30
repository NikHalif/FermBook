using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WindowCheckFileDB.xaml
    /// </summary>
    public partial class WindowCheckFileDB : Window
    {
        public WindowCheckFileDB()
        {
            InitializeComponent();
        }

        private bool Check()
        {
            try
            {
                using (FileStream strem = File.OpenRead("DatabaseSQL.mdf"))
                {

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                ((WindowLoad)this.Owner).AvtivePanel(true);
            }
            else Mes.View("Файл занят!");

            this.Close();
        }
    }
}
