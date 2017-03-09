using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Storage
{
    public class UriInfo
    {
        public string StorageUri { get; protected set; }

        public string ContainerName { get; protected set; }

        public string FileName { get; private set; }

        public UriInfo(string uri)
        {
            System.Uri fileUri = new System.Uri(uri);

            StorageUri = string.Concat(fileUri.Scheme, "://", fileUri.Authority);
            ContainerName = fileUri.Segments[1].Replace("/", "");
            FileName = fileUri.Segments[2];
        }
    }
}
