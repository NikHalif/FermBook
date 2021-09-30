using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermBook
{
    class DriveLoadFile
    {
        //internal static readonly string SaveFolder = "FermBookBack";
        /// <summary>
        /// Загрузка файла по имени
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="service">Авторизованный сервис, откуда производится загрузка</param>
        /// <returns>Статус загрузки</returns>
        internal static DownloadStatus Download(string fileName, DriveService service)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"\"{nameof(fileName)}\" не может быть неопределенным или пустым.", nameof(fileName));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            try
            {
                var files = GetFile(service, $"name = '{fileName}' and 'appDataFolder' in parents");

                if (files != null && files.Count > 0)
                {
                    IDownloadProgress result;
                    using (FileStream fileW = File.Create(files[0].Name))
                    {
                        result = service.Files.Get(files[0].Id).DownloadWithStatus(fileW);
                    }
                    return result.Status;
                }
                else throw new Exception("Файла для загрузки нет");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        internal static UploadStatus Upload(string fileName, DriveService service)
        {
            Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string>()
                {
                    "appDataFolder"
                }
            };
            try
            {
                IUploadProgress result;
                using (FileStream fileW = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var files = GetFile(service, $"name = '{fileName}' and 'appDataFolder' in parents");
                    if (files != null && files.Count > 0) result = service.Files.Update(file, files[0].Id, fileW, "").Upload();
                    else result = service.Files.Create(file, fileW, "").Upload();
                }
                return result.Status;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        internal static IList<Google.Apis.Drive.v3.Data.File> GetFile(DriveService service, string filtr, int count = 1)
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Spaces = "appDataFolder";
            listRequest.PageSize = count;
            listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Q = filtr;
            try
            {
                return listRequest.Execute()
                .Files;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            
        }

        /*private static Google.Apis.Drive.v3.Data.File CreateFolder(string name, DriveService service)
        {
            Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Description = "document description",
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>()
                {
                    "appDataFolder"
                }
            };

            return service.Files.Create(body).Execute();
        }*/
    }
}
