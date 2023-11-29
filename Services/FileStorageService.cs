using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using iLearn.Services.Interfaces;
using System.Text;

namespace iLearn.Services
{
    public class FileStorageService: IFileStorageService
    {
        private readonly IConfiguration _config;
        private IConfigurationSection azureFileStorage;
        private string connectionString;
        private string fileShareName;
        private string folderName;

        public FileStorageService(IConfiguration config)
        {;
            _config = config;
        }
        private void GetAzureDetails()
        {
            azureFileStorage = _config.GetSection("AzureFileStorage");
            connectionString = azureFileStorage["AzureConnectionString"];
            fileShareName = azureFileStorage["AzureFileShareName"];
            folderName = azureFileStorage["AzureDirectoryName"];
        }
        public string UploadFile(string filePath, string destinationFolderToStore)
        {
            GetAzureDetails();
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

        public byte[] DownloadFile(string fileName, string destinationFolder)
        {
            GetAzureDetails();

            ShareServiceClient serviceClient = new ShareServiceClient(connectionString);
            ShareClient shareClient = serviceClient.GetShareClient(fileShareName);
            ShareDirectoryClient directory = shareClient.GetDirectoryClient(folderName);
            ShareDirectoryClient subDirectory = directory.GetSubdirectoryClient(destinationFolder);
            ShareFileClient fileClient = subDirectory.GetFileClient(fileName);

            try
            {
                using (Stream fileStream = fileClient.OpenRead())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Azure Storage request failed: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO exception: {ex.Message}");
            }

            return null; // Return null if the file download was not successful.
        }

        public byte[] GetCertificate()
        {
            GetAzureDetails();
            string destinationFolder = "Certificate_Download";
            string fileName = "Certificate_iLearn.pdf";       

            ShareServiceClient serviceClient = new ShareServiceClient(connectionString);
            ShareClient shareClient = serviceClient.GetShareClient(fileShareName);
            ShareDirectoryClient directory = shareClient.GetDirectoryClient(folderName);
            ShareDirectoryClient subDirectory = directory.GetSubdirectoryClient(destinationFolder);
            ShareFileClient fileClient = subDirectory.GetFileClient(fileName);

            try
            {
                using (Stream fileStream = fileClient.OpenRead())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Azure Storage request failed: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO exception: {ex.Message}");
            }

            return null; // Return null if the file download was not successful.
        }

        public void LogException(Exception ex)
        {
            GetAzureDetails();
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
            GetAzureDetails();
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
