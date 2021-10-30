using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadApp.Models;

namespace UploadApp.Extension
{
    public static class StartupExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration _config) =>
            services.Configure<ReaderModel>(_config.GetSection("ConnectionStrings"));
    }
}
