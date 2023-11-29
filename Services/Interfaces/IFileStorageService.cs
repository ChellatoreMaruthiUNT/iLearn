using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface IFileStorageService
    {
        public string UploadFile(string filePath, string destinationFolderToBeCreated);
        public byte[] DownloadFile(string fileName, string destinationFolder);
        public void LogException(Exception ex);
        public void LogMessage(string message);
        public byte[] GetCertificate();
    }
}
