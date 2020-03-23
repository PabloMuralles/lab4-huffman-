using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

        [HttpPost]
        [Route("api/compress/LZW/{nombre}")]
        public async Task<IActionResult> PostCompressLZW(IFormFile file, string nombre)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
           
            LZW.Lectura.AlgoritomoCompresion Compress = new LZW.Lectura.AlgoritomoCompresion(nombre,file);
            return Ok();

        }

        [HttpPost]
        [Route("api/decompress/LZW/{nombre}")]
        public async Task<IActionResult> PostdessCompressLZW(IFormFile file, string nombre)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            LZW.Lectura.AlgoritmoDescompresion Desscompress = new LZW.Lectura.AlgoritmoDescompresion(file,nombre);
            return Ok();

        }

        [HttpGet]
        [Route("api/compression/LZW")]
        public ActionResult<string> Peliculas()
        {
            var ListCompresion = LZW.Lectura.HistorialCompresion.Instance.ArchivosComprimidosPila;
            var ListDescomresion = LZW.Lectura.HistorialCompresion.Instance.ArchivosDescomprimidosPils;
            if (ListCompresion == null && ListDescomresion == null)
            {
                return NotFound("No hay datos.");

            }
            else
            {
                LZW.Historial.Historial Nuevo = new LZW.Historial.Historial(ListCompresion, ListDescomresion);

                var Historial = Nuevo.Anallizar();
                var json = JsonConvert.SerializeObject(Historial);
                return json;
                 

            }
        }

    }
}