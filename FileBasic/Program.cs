using Files;
using System;

namespace FileBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetDriveInfomation.GetDrivesInfo();
            var directory_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            GetDriveInfomation.ListFileDirectory(directory_mydoc);
        }
    }
}
