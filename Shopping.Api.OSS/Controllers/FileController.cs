using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Web;

namespace Shopping.Api.OSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private IWebHostEnvironment _webhostEnv;
        public FileController(ILogger<FileController> logger, IWebHostEnvironment webhostEnv)
        {
            _logger = logger;
            _webhostEnv = webhostEnv;
        }
        private static string UploadDirectory = "/upload";
        [HttpPost]
        [AllowAnonymous]
        public async Task<List<FileResponse>> Post()
        {
            List<FileResponse> resp = new List<FileResponse>();
            //获取boundary
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            //得到reader
            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();

            //读取 section  每个formData算一个 section  多文件上传时每个文件算一个 section
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    if (contentDisposition!.IsFileDisposition())
                    {
                        string[] fileNames = contentDisposition!.FileName.Value.Split('.');
                        string fileName = Guid.NewGuid().ToString();
                        if (fileNames.Length>=2)
                        {
                            fileName += $".{fileNames[fileNames.Length - 1]}";
                        }
                        //相对路径
                        string saveKey = $"{UploadDirectory}/{DateTime.Now.ToString("yyyyMMdd")}/" + fileName;

                        //完整路径
                        string path = _webhostEnv.WebRootPath + saveKey;

                        string directoryPath = Path.GetDirectoryName(path)!;
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        using (var stream = System.IO.File.Create(path))
                        {
                            await section.Body.CopyToAsync(stream);
                        }
                        resp.Add(new FileResponse()
                        {
                            FileName = contentDisposition.FileName.Value,
                            PathUrl = "/file" + saveKey.Replace(UploadDirectory,""),//去掉总文件夹路径
                        });
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }

            return resp;
        }
        [HttpGet("{*path}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string path)
        {
            //完整路径，加上总文件夹路径
            string filePath = _webhostEnv.WebRootPath + UploadDirectory+"/" +path;
            if (System.IO.File.Exists(filePath))
            {
                string fileName = Path.GetFileName(filePath).Split('.')[1];
                if (fileName == "jpg"|| fileName == "jpeg")
                {
                    return PhysicalFile(filePath, "image/jpeg");
                }
                return PhysicalFile(filePath, "application/octet-stream");
            }
            return BadRequest();
        }
    }
    public class FileResponse
    { 
        public string PathUrl { get; set; }
        public string FileName { get; set; }
    }
}