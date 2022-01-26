using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCoreUtilidades.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail; /*IMPORTANTE PARA ENVIAR CORREOS*/
using System.Net;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UtilidadesController : Controller
    {
        private HelperMail helperMail;
        private HelperUploadFiles helperUpload;
        public UtilidadesController(HelperMail helperMail, HelperUploadFiles helperUpload) 
        {
            this.helperMail = helperMail;
            this.helperUpload = helperUpload;
        }
        public IActionResult UploadFiles()
        {
            return View();
        }

        /*IMPORTANTE PARA SUBIR FICHEROS AL SERVIDOR*/
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile fichero)
        {

            string path = await this.helperUpload.UploadFileAsync(fichero, Folders.Uploads);
            ViewBag.Mensaje = "Fichero subido a " + path;
            ViewBag.FileName = "aq";
            return View();
        }
        public IActionResult SendMail() 
        {
            return View();
        }
        /*recibimos por parametro los datos del formulario para despues enviarlos al correo*/
        [HttpPost]
        public async Task<IActionResult> SendMail
            (string destinatario,string asunto,string mensaje,IFormFile fichero)
        {
            if (fichero != null)
            {
                //UTILIZAMOS UPLOADFILES
                string path = await this.helperUpload.UploadFileAsync(fichero, Folders.Temp);
                this.helperMail.SendMail(destinatario, asunto, mensaje, path);
            }
            else 
            {
                this.helperMail.SendMail(destinatario, asunto, mensaje);
            }
            ViewData["MENSAJE"] = "Mail enviado correctamente";
            return View();
        }
    }
}
