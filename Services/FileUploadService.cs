using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadApp.Interfaces;
using UploadApp.Models;

namespace UploadApp.Services
{
    public class FileUploadService : IUploadService
    {
        private readonly IOptions<ReaderModel> _option;
        public FileUploadService(IOptions<ReaderModel> options)
        {
            _option = options;
        }
        public async Task<int> UploadFile(IFormFile imageFile, FileModel image)
        {
            string filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await imageFile.CopyToAsync(stream);
            }
                // Converts image file into byte[]
            byte[] imageData = await File.ReadAllBytesAsync(filePath);

            using (var connection = new SqlConnection(_option.Value.DefaultConnection))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("uspUpload", new {
                    filename = image.FileName, filetype = image.FileType, imageData = Convert.ToBase64String(imageData)
                }, commandType: CommandType.StoredProcedure);
                
                return (int)connection.State;
            }
        }
    }
}
