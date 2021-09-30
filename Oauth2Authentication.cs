using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.PeopleService.v1;
using System;
using System.IO;
using System.Net;
using System.Resources;
using System.Threading;
using Google.Apis.PeopleService.v1.Data;
using System.Linq;

namespace FermBook
{
    public static class Oauth2Authentication
    {
        public static CancellationTokenSource OauthCT = new CancellationTokenSource();
        /// <summary>
        /// Авторизация в Google Oauth2
        /// </summary>
        /// <param name="Scopes">Запрашиваемые доступы для приложения</param>
        /// <returns>Авторизованный пользователь </returns>
        internal static UserCredential Oauth(string[] Scopes, string ApplicationName)
        {
            UserCredential credential;
            try
            {

                using (var stream =
                     new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        OauthCT.Token,
                        new FileDataStore(credPath, true)).Result;
                }

                return credential;
                // Create Drive API service.

            }

            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        internal static DriveService GetDriveService(UserCredential userCredential, string AppName)
        {
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = AppName,
            });
        }

        internal static PeopleServiceService GetPeopleService(UserCredential userCredential, string AppName)
        {
            return new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = AppName,
            });
        }

        internal static Person GetPerson(PeopleServiceService peopleService, params string[] parameters)
        {
            var request = AppFuncG.PService.People.Get("people/me");
            request.PersonFields = string.Join(",", parameters); 
            return request.Execute();
        }

    }

}
