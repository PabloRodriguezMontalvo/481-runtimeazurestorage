using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RuntimeAzureStorage
{
    public sealed class AlmacenAzure
    {

        public IAsyncOperation<String> SubirFicheroAsync(StorageFile fichero,
            String recurso)
        {

            return SubirFicheroHelper(fichero,recurso).
                AsAsyncOperation();
        }



        private async Task<String> SubirFicheroHelper(StorageFile fichero,
            String recurso)
        {
            using (var stream=await  fichero.OpenStreamForReadAsync())
            {

                var credenciales = new StorageCredentials("wenotepadalu",
      "ovd0FYdLrR9th19GwZqCdUR8MBq0o31DVNydbbIq4o5+iqItJFw2emDrDblVjrWupvLqnf8dJefi4eOsWYx+xQ==");
              var cuenta = new CloudStorageAccount(credenciales, true);
              var cliente = cuenta.CreateCloudBlobClient();
              var container = cliente.GetContainerReference("fotos");
               
                var blob = container.GetBlockBlobReference(recurso);

              await blob.UploadFromStreamAsync(stream.AsInputStream());

                return blob.Uri.ToString();
            }

            

        }

    }
}
