using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreUtilidades.Providers
{
    public enum Folders 
    {
        Uploads = 0, Images = 1, Documents = 2, Temp = 3
    }
    public class PathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        public PathProvider(IWebHostEnvironment hostEnvironment) 
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(String fileName, Folders folder) 
        {
            string carpeta = "";
            if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Documents) 
            {
                carpeta = "documents";
            }
            string path = Path.Combine(this.hostEnvironment.WebRootPath, carpeta, fileName);
            /*esto por si queremos guardarlo temporalmente en el ordenador*/
            if (folder == Folders.Temp) 
            {
                path = Path.Combine(Path.GetTempPath(), fileName);
            }
            return path;
        }
    }
}
