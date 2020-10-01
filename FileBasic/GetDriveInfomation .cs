using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Files
{
    public class GetDriveInfomation
    {
        public static void GetDrivesInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
                    Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
                    Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }

        public static void GetAllFolder()
        {
            var directory_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            String[] files = System.IO.Directory.GetFiles(directory_mydoc);
            String[] directories = System.IO.Directory.GetDirectories(directory_mydoc);

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
            }
        }

        //liet ke all file, thu muc trong tung thu muc
        public static void ListFileDirectory(string path)
        {
            String[] directories = System.IO.Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);

            foreach(var file in files)
            {
                Console.WriteLine(file);
            }
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);     
                ListFileDirectory(directory); // Đệ quy
            }

        }
    }
}
