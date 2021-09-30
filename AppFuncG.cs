using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace FermBook
{
    static class AppFuncG
    {
        public static bool IsOffline = false;
        internal static DriveService DService { get; private set; }
        internal static UserCredential Credential { get; private set; }
        internal static PeopleServiceService PService { get; private set; }
        internal static Person People { get; private set; }
        internal static string[] Scopes = { DriveService.Scope.DriveAppdata, Google.Apis.PeopleService.v1.PeopleServiceService.Scope.UserinfoProfile };
        internal static string ApplicationName = "FermBook";
        internal static string FileName = "DatabaseSQL.mdf";

        private static WindowLoad WinActive;

        private static FileStream fileW;

        public static void Auth()
        {
            try
            {
                Credential = Oauth2Authentication.Oauth(Scopes, ApplicationName);
                DService = Oauth2Authentication.GetDriveService(Credential, ApplicationName);
                PService = Oauth2Authentication.GetPeopleService(Credential, ApplicationName);
            }
            catch (Exception e)
            {
                Mes.View(e.Message);
            }

            //var t = PService.People.Get("people/me");
            //t.PersonFields = "nicknames,names";
            //var r = t.Execute();

            //Debug.WriteLine(DriveLoadFile.Upload("SkillBoxUpload.html", service).ToString());
            //var t = DriveLoadFile.GetFile(service, "", 10);

        }
        public static void LoadPersone()
        {
            People = Oauth2Authentication.GetPerson(AppFuncG.PService, "nicknames", "names", "photos");
        }
        public static long UpLoadFileDB(WindowLoad Win)
        {
            WinActive = Win;
            Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File()
            {
                Name = FileName,
                Parents = new List<string>()
                {
                    "appDataFolder"
                }
            };
            try
            {
                fileW = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                FilesResource.CreateMediaUpload media = DService.Files.Create(file, fileW, "");

                media.ProgressChanged += Media_ProgressChanged;
                media.ResponseReceived += Media_ResponseReceived;

                media.UploadAsync();

                return fileW.Length;
            }
            catch (Exception e)
            {
                if(fileW != null) fileW.Close();
                throw new Exception(e.Message, e);
            }

        }

        public static void DownloadFileDB(WindowLoad Win)
        {
            WinActive = Win;
            var status = DriveLoadFile.Download(FileName, DService);
            Mes.View($"Файл загружен со статусом {status}");
            WinActive.AvtivePanel(true);
        }

        private static void Media_ResponseReceived(Google.Apis.Drive.v3.Data.File obj)
        {
            fileW.Close();
            WinActive.EndProgressBar();
        }

        private static void Media_ProgressChanged(IUploadProgress obj)
        {
            WinActive.UpdateProgressBar(obj.Status, obj.BytesSent);
            if (obj.Status == UploadStatus.Failed)
            {
                Mes.View(obj.Exception.Message);
            }
        }
    }

    public class SwitchBindingExtension : Binding
    {
        public SwitchBindingExtension()
        {
            Initialize();
        }

        public SwitchBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        public SwitchBindingExtension(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            Initialize();
            this.ValueIfTrue = valueIfTrue;
            this.ValueIfFalse = valueIfFalse;
        }

        private void Initialize()
        {
            this.ValueIfTrue = Binding.DoNothing;
            this.ValueIfFalse = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        [ConstructorArgument("valueIfTrue")]
        public object ValueIfTrue { get; set; }

        [ConstructorArgument("valueIfFalse")]
        public object ValueIfFalse { get; set; }

        private class SwitchConverter : IValueConverter
        {
            public SwitchConverter(SwitchBindingExtension switchExtension)
            {
                _switch = switchExtension;
            }

            private SwitchBindingExtension _switch;

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    bool b = System.Convert.ToBoolean(value);
                    return b ? _switch.ValueIfTrue : _switch.ValueIfFalse;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Binding.DoNothing;
            }

            #endregion
        }

    }
}
