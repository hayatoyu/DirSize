using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirSize
{
    class FSSizeGetter
    {
        enum SizeUnit
        {
            Bytes, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public long getDirSize(string path)
        {
            long size = 0;

            DirectoryInfo dinfo = new DirectoryInfo(path);

            // 計算資料夾中的檔案總大小
            var files = dinfo.GetFiles();
            foreach (var f in files)
                size += f.Length;

            // 遞迴計算子資料夾中的檔案大小
            var childrenDir = dinfo.GetDirectories();
            foreach (var dir in childrenDir)
                size += getDirSize(dir.FullName);

            return size;
        }

        public long getDirSizeByLinq(string path)
        {
            return (from file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories) select new FileInfo(file).Length).Sum();
        }

        public long getDirSizeByScripting(string path)
        {
            return long.Parse((new Scripting.FileSystemObjectClass()).GetFolder(path).Size.ToString());
        }

        public long getDirSizeByReflection(string path)
        {
            Type tp = Type.GetTypeFromProgID("Scripting.FileSystemObject");
            var fso = Activator.CreateInstance(tp);
            var folder = tp.InvokeMember("GetFolder", System.Reflection.BindingFlags.InvokeMethod, null, fso, new object[] { path });
            long size = Convert.ToInt64(tp.InvokeMember("Size", System.Reflection.BindingFlags.GetProperty, null, folder, null));
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(fso);
            return size;
        }

        public string formatSize(long size)
        {
            double formatSize = 0;
            int count = 0;

            formatSize = size;
            while (formatSize > 1024 && count < 8)
            {
                formatSize /= 1024;
                count++;
            }
            return Math.Round(formatSize, 2).ToString() + " " + (SizeUnit)count;
        }
    }
}
