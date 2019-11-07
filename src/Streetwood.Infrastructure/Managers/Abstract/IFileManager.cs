﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Streetwood.Infrastructure.Managers.Abstract
{
    public interface IFileManager
    {
        Task MoveFileAsync(IFormFile file, string directoryPath, string uniqueFileName);

        void RemoveFile(string path);
    }
}
