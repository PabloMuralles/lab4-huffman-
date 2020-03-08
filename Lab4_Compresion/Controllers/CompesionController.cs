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
        [Route("api/archivo/{nombre}")]
        public async Task<IActionResult> Post(IFormFile file, string nombre)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            Lectura.Lectura NuevoArchivo = new Lectura.Lectura(nombre,filePath);
            
            return Ok(new { count = 1, path = filePath });
        }
    }
}