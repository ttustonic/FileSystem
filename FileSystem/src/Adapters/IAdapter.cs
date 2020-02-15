using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SharpGrip.FileSystem.Models;

namespace SharpGrip.FileSystem.Adapters
{
    public interface IAdapter : IDisposable
    {
        public void Connect();
        string Prefix { get; }
        string RootPath { get; }
        IFile GetFile(string path);
        IDirectory GetDirectory(string path);
        IEnumerable<IFile> GetFiles(string path = "");
        IEnumerable<IDirectory> GetDirectories(string path = "");
        bool FileExists(string path);
        bool DirectoryExists(string path);
        Stream CreateFile(string path);
        DirectoryInfo CreateDirectory(string path);
        Task DeleteDirectory(string path, bool recursive);
        Task DeleteFile(string path);
        Task<byte[]> ReadFile(string path);
        Task<string> ReadTextFile(string path);
        Task WriteFile(string path, byte[] contents, bool overwrite = false);
        Task WriteFile(string path, string contents, bool overwrite = false);
        Task AppendFile(string sourcePath, byte[] contents);
        Task AppendFile(string sourcePath, string contents);
    }
}