using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadApp.Models;

namespace UploadApp.Interfaces
{
    public interface IUploadService
    {
        Task<int> UploadFile(IFormFile imageFile, FileModel image);
    }
}
