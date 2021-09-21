using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
        internal static string FileName = "DataBase.xsd";

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
    }
}
