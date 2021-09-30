using System.Windows;

namespace FermBook
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {

            if (Oauth2Authentication.OauthCT.Token != null)
            {
                Oauth2Authentication.OauthCT.Cancel();
                Oauth2Authentication.OauthCT.Dispose();
            }
            
        }
    }
}
