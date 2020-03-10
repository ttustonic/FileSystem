using System;

namespace SharpGrip.FileSystem.Exceptions
{
    public class DirectoryNotFoundException : Exception
    {
        public DirectoryNotFoundException(string path, string prefix) : base(GetMessage(path, prefix))
        {
        }

        private static string GetMessage(string path, string prefix)
        {
            return $"Directory '{path}' not found in adapter with '{prefix}'.";
        }
    }
}