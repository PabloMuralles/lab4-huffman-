using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Lab4_Compresion.Controllers
{
    public class CompesionController : Controller
    {
        [HttpPost]
        [Route("api/compress/{nombre}")]
        public async Task<IActionResult> Post(IFormFile file, string nombre)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
                     Compresion.Compresion.Instance.RutaArchivos += nombre;
            Lectura.Lectura NuevoArchivo = new Lectura.Lectura(nombre,filePath);   
            return Ok();
        }

        [HttpPost]
        [Route("api/decompress/{nombre}")]
        public async Task<IActionResult> PostDescompresion(IFormFile file, string nombre)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            Descomprecion.Descompresion descompresion = new Descomprecion.Descompresion(nombre,filePath);      
            return Ok();

        }
    }
}