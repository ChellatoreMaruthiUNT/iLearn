using Azure;
using Azure.Storage.Files.Shares;
using iLearn.Services.Interfaces;
using System.Text;

namespace iLearn.Services
{
    public class FileStorageService: IFileStorageService
    {
        private readonly IConfiguration _config;

        public FileStorageService(IConfiguration config)
        {
            _config = config;
        }
        public string UploadFile(string filePath, string destinationFolderToStore)
        {
            var azureFileStorage = _config.GetSection("AzureFileStorage");
            var connectionString = azureFileStorage["AzureConnectionString"];
            var fileShareName = azureFileStorage["AzureFileShareName"];
            var folderName = azureFileStorage["AzureDirectoryName"];
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(filePath);
            ShareClient share = new ShareClient(connectionString, fileShareName);
            share.CreateIfNotExists();
            if (share.Exists())
            {
                ShareDirectoryClient directory = share.GetDirectoryClient(folderName);
                directory.CreateIfNotExists();
                ShareDirectoryClient subDirectory = directory.GetSubdirectoryClient(destinationFolderToStore);
                subDirectory.CreateIfNotExists();                
                if (subDirectory.Exists())
                {
                    ShareFileClient fileCreate = subDirectory.GetFileClient(fileName);
                    using (FileStream stream = File.OpenRead(filePath))
                    {
                        fileCreate.Create(stream.Length);
                        fileCreate.UploadRange(
                            new HttpRange(0, stream.Length),
                            stream);
                    }
                }
            }
            else
            {
                Console.WriteLine($"File is not upload successfully");
            }
            return fileName;
        }
        public void LogException(Exception ex)
        {
            var azureFileStorage = _config.GetSection("AzureFileStorage");
            var connectionString = azureFileStorage["AzureConnectionString"];
            var fileShareName = azureFileStorage["AzureFileShareName"];
            var folderName = azureFileStorage["AzureDirectoryName"];
            var fileName = "Exception" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            ShareClient share = new ShareClient(connectionString, fileShareName);
            share.CreateIfNotExists();
            if (share.Exists())
            {
                ShareDirectoryClient directory = share.GetDirectoryClient(folderName);
                directory.CreateIfNotExists();
                ShareDirectoryClient subDirectory = directory.GetSubdirectoryClient("iLearn_Logs");
                subDirectory.CreateIfNotExists();
                if (subDirectory.Exists())
                {
                    var fileClient = subDirectory.GetFileClient(fileName);
                    ShareFileClient fileCreate = subDirectory.GetFileClient(fileName);
                    fileCreate.Create(52428);
                    var exceptionDetails = new StringBuilder();
                    exceptionDetails.AppendLine($"Exception Type: {ex.GetType().FullName}");
                    exceptionDetails.AppendLine($"Message: {ex.Message}");
                    exceptionDetails.AppendLine($"Stack Trace: {ex.StackTrace}");
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(exceptionDetails.ToString())))
                    {
                        fileClient.Upload(stream);
                    }
                }
            }
            else
            {
                Console.WriteLine($"File is not upload successfully");
            }
        }

        public void LogMessage(string message)
        {
            var azureFileStorage = _config.GetSection("AzureFileStorage");
            var connectionString = azureFileStorage["AzureConnectionString"];
            var fileShareName = azureFileStorage["AzureFileShareName"];
            var folderName = azureFileStorage["AzureDirectoryName"];
            var fileName = "Message" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            ShareClient share = new ShareClient(connectionString, fileShareName);
            share.CreateIfNotExists();
            if (share.Exists())
            {
                ShareDirectoryClient directory = share.GetDirectoryClient(folderName);
                directory.CreateIfNotExists();
                ShareDirectoryClient subDirectory = directory.GetSubdirectoryClient("iLearn_Logs");
                subDirectory.CreateIfNotExists();
                if (subDirectory.Exists())
                {
                    var fileClient = subDirectory.GetFileClient(fileName);
                    ShareFileClient fileCreate = subDirectory.GetFileClient(fileName);
                    fileCreate.Create(52428);
                    
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
                    {
                        fileClient.Upload(stream);
                    }
                }
            }
            else
            {
                Console.WriteLine($"File is not upload successfully");
            }
        }
    }
}
