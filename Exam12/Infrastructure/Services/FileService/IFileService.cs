﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.FileService
{
    public interface IFileService
    {
        Task<string> CreateFile(IFormFile file);
        bool DeleteFile(string file);
    }
}
