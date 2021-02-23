﻿/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * RESSOURCES
 */
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rhino.Controllers.Domain.Interfaces
{
    public interface ILogsRepository
    {
        IEnumerable<string> Get(string logPath);
        Task<(int StatusCode, Stream Stream)> GetAsMemoryStreamAsync(string logPath, string id);
        Task<(int StatusCode, string LogData)> GetAsync(string logPath, string id);
        Task<(int StatusCode, string LogData)> GetAsync(string logPath, string id, int numberOfLines);
    }
}