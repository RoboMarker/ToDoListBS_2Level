using Microsoft.AspNetCore.Mvc;
using ToDoListBS.ViewModels;

namespace ToDoListBS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        {
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"}
        };
        private readonly string _folder;

        public UploadController(IWebHostEnvironment env)
        {
            //Microsoft.Extensions.Hosting.IHostEnvironment
            //Microsoft.AspNetCore.Hosting.IWebHostEnvironment : IHostEnvironment
            //Microsoft.Extensions.Hosting.IHostApplicationLifetime
            //Microsoft.Extensions.Hosting.Environments
            // 把上傳目錄設為：wwwroot\UploadFolder
            // _folder = $@"{env.WebRootPath}\Upload";
            _folder = $@"{env.ContentRootPath}Upload";
           // var _folder2 = Path.Combine(env.ContentRootPath, "Upload");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("txt")]
        public async Task<IActionResult> PostText()
        {

            return Ok("ok");
        }

        [HttpPost("Excel")]
        public async Task<IActionResult> PostExcel([FromForm] UploadVM uploadVM)
        {
            //Request.Form.Files.FirstOrDefault()
            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            string sFileName = uploadVM.FFile.FileName;
            var path = Path.Combine(_folder, sFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await uploadVM.FFile.CopyToAsync(stream);
            }


            return Ok(new { result = "sunccess" });
        }
        //IFormFile 檔案上傳可用,多個用 List<IFormFile>
        [HttpPost("Image2")]
        public async Task<IActionResult> UploadImage2([FromForm] UploadVM uploadVM)
        {
            //Request.Form.Files.FirstOrDefault()
            //var size = vmFiles.FormFile.Length;
            //var file = vmFiles.FormFile;
            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            string sFileName = uploadVM.FFile.FileName;
            var path = Path.Combine(_folder, sFileName);
            
            //using (var stream = System.IO.File.Create(path))
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await uploadVM.FFile.CopyToAsync(stream);
            }


            return Ok(new { result = "sunccess" });
        }


        [HttpPost("Movice")]
        public async Task<IActionResult> PostMovice()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
