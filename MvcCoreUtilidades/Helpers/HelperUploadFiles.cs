using Microsoft.AspNetCore.Http;
using MvcCoreUtilidades.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFiles
    {
        private PathProvider pahtProvider;
        public HelperUploadFiles(PathProvider pathProvider) 
        {
            this.pahtProvider = pathProvider;
        }

        //esto es para subir los ficheros 
        public async Task<string> UploadFileAsync
            (IFormFile formFile, Folders folder) 
        {
            string fileName = formFile.FileName;
            string path = this.pahtProvider.MapPath(fileName, folder);
            using (Stream stream = new FileStream(path, FileMode.Create)) 
            {
                await formFile.CopyToAsync(stream);
            }
            return path;
        }

    }
}
