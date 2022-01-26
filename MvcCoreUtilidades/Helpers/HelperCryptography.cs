using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;/*importante para criptar las contraseñas*/
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public static class HelperCryptography
    {
        //VAMOS A REALIZAR UN CIFRADO BASICO 
        public static string EncriptarTextoBasico(string contenido) 
        {
            //NECESITAMOS TRABAJAR A NIVEL DE BYTE 
            //DEBEMOS CONVERTIR EL CONTENIDO A BYTE []
            byte[] entrada;
            //UNA VEZ QUE APLIQUEMOS EL CIFRADO, NOS DEVOLVERA UN BYTE[]
            //DE SALIDA CON LOS ELEMENTOS CIFRADOS
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR BYTE[] A STRING 
            // Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS EL OBJETO PARA EL CIFRADO
            SHA1Managed sha = new SHA1Managed();
            //CONVERTIMOS EL CONTENIDO A BYTE[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO SHA1 CONTIENE UN METODO PARA REALIZAR EL CIFRADO
            // Y DEVOLVER EL CONTENIDO A BYTE[]
            salida = sha.ComputeHash(entrada);//esto iria a la bbdd
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        public static string Salt { get; set; }
        private static string GenerateSalt() 
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++) 
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }
        public static string EncriptarContenido(string contenido, bool comparar) 
        {
            if (comparar == false) 
            {
                Salt = GenerateSalt();
            }
            string contenidosalt = contenido + Salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidosalt);
            //CIFRAMOS N VECES 
            for (int i= 1; i <= 55; i++) 
            {
                //REALIZAMOS EL CIFRADO SOBRE LA SALIDA
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();//limpiar el sha, ya que utiliza mucha memoria
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
