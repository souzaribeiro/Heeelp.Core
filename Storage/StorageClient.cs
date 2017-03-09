using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace Heeelp.Core.Storage
{
    public class StorageClient   : IStorage
    {
        private readonly CloudBlobClient _blobClient;
        public StorageClient(string storageAddress)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAddress);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public void DeleteFile(string containerName, string fileName)
        {
            CloudBlobContainer container = GetContainerReference(containerName);

            CloudBlockBlob fileReference = container.GetBlockBlobReference(fileName);

            fileReference.DeleteIfExists();

        }

        public byte[] DownloadFile(string containerName, string fileName)
        {
            CloudBlobContainer container = GetContainerReference(containerName);

            CloudBlockBlob fileReference = container.GetBlockBlobReference(fileName);

            if (!fileReference.Exists())
            {
                throw new System.ArgumentException("Arquivo não encontrado");
            }

            System.IO.MemoryStream fileStream = new System.IO.MemoryStream();
            fileReference.DownloadToStream(fileStream);
            return fileStream.ToArray();
        }

        public UriInfo GetUriInfo(string storagePath)
        {
            return new UriInfo(storagePath);
        }

        public string UploadFile(string containerName, string fileName, string mimeType, byte[] file)
        {
            CloudBlobContainer container = GetContainerReference(containerName);
            CloudBlockBlob fileReference = container.GetBlockBlobReference(fileName);

            fileReference.Properties.ContentType = mimeType;
            fileReference.UploadFromByteArray(file, 0, file.Length);
            return fileReference.Uri.ToString();
        }

        public string UploadFile(string containerName, string fileName, string mimeType, System.IO.Stream file)
        {
            CloudBlobContainer container = GetContainerReference(containerName);
            CloudBlockBlob fileReference = container.GetBlockBlobReference(fileName);

            fileReference.Properties.ContentType = mimeType;
            fileReference.UploadFromStream(file);
            return fileReference.Uri.ToString();
        }

        private CloudBlobContainer GetContainerReference(string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            return container;
        }

        public string CopyFile(string containerNameFrom, string fileNameFrom, string containerNameTo, string fileNameTo)
        {
            CloudBlobContainer containerFrom = GetContainerReference(containerNameFrom);
            CloudBlockBlob fileReferenceFrom = containerFrom.GetBlockBlobReference(fileNameFrom);

            byte[] fileBytes = DownloadFile(containerNameFrom, fileNameFrom);

            return UploadFile(containerNameTo, fileNameTo, fileReferenceFrom.Properties.ContentType, fileBytes);
        }

        public string GetSharedAccess(string containerName, string fileName)
        {
            CloudBlobContainer container = GetContainerReference(containerName);
            CloudBlockBlob fileReference = container.GetBlockBlobReference(fileName);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(3);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            string sasBlobToken = fileReference.GetSharedAccessSignature(sasConstraints);

            return fileReference.Uri + sasBlobToken;
        }
    }
}
