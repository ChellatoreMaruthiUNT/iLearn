using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface IFileStorageService
    {
        public string UploadFile(string filePath, string destinationFolderToBeCreated);
        public void LogException(Exception ex);
        public void LogMessage(string message);
    }
}
