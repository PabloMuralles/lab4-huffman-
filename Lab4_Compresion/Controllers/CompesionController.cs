using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_Compresion.Controllers
{
    public class CompesionController : Controller
    {
        [HttpPost]
        [Route("api/archivo")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            Lectura.Lectura.Instance.Ingresar(filePath);
            return Ok(new { count = 1, path = filePath });
        }
    }
}