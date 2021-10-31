using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UploadApp.Interfaces;
using UploadApp.Models;

namespace UploadApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        IUploadService _fileUploadService;

        public FileUploadController(IUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost, Route("UploadYourFile")]
        public async Task<ErrorModel> UploadEnrolleePassport(
            IFormFile imageFile, [FromForm] FileModel image)
        {
            try
            {
                var imageupload = await _fileUploadService.UploadFile(imageFile, image);
                if (imageupload < 1)
                {
                    return new ErrorModel { ErrMessage = "Unable to upload your Image", Status = imageupload };
                }
                return new ErrorModel { ErrMessage = "Image Uploaded Successfully", Status = imageupload };
            }
            catch (Exception ex)
            {
                return new ErrorModel { ErrMessage = ex.Message, Status = 0 };
            }
        }
    }
}
