using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Storage
{
    public interface IStorage
    {
        byte[] DownloadFile(string containerName, string fileName);

        string UploadFile(string containerName, string fileName, string mimeType, byte[] file);
        string UploadFile(string containerName, string fileName, string mimeType, System.IO.Stream file);

        void DeleteFile(string containerName, string fileName);

        UriInfo GetUriInfo(string storagePath);

        string CopyFile(string containerNameFrom, string fileNameFrom, string containerNameTo, string fileNameTo);

        string GetSharedAccess(string containerName, string fileName);
    }
}
