using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class MultipleFileDownload
    {
        public static List<FileInfo> GetFile(string FolderName, List<FileInfo> listF)
        {
            List<FileInfo> listFiles = new List<FileInfo>();
            string fileSavePath = System.Web.Hosting.HostingEnvironment.MapPath(FolderName);
            DirectoryInfo dirInfo = new DirectoryInfo(fileSavePath);
            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                if (listF.Any(x => x.FileName == item.Name))
                {
                    listFiles.Add(new FileInfo()
                    {
                        FileId = i + 1,
                        FileName = item.Name,
                        FilePath = dirInfo.FullName + @"\" + item.Name
                    });
                    i = i + 1;
                }
            }
            return listFiles;
        }
    }

    public class FileInfo
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}