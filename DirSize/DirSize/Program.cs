using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DirSize
{
    class Program
    {
        static void Main(string[] args)
        {
            FSSizeGetter getter = new FSSizeGetter();

            var watch = Stopwatch.StartNew();
            Console.WriteLine("DirSize : " + getter.formatSize(getter.getDirSize(@"D:\01lin")));
            Console.WriteLine(watch.ElapsedMilliseconds + " ms spent.\n");

            watch.Restart();
            Console.WriteLine("DirSize : " + getter.formatSize(getter.getDirSizeByLinq(@"D:\01lin")));
            Console.WriteLine(watch.ElapsedMilliseconds + " ms spent.\n");

            watch.Restart();
            Console.WriteLine("DirSize : " + getter.formatSize(getter.getDirSizeByScripting(@"D:\01lin")));
            Console.WriteLine(watch.ElapsedMilliseconds + " ms spent.\n");

            watch.Restart();
            Console.WriteLine("DirSize : " + getter.formatSize(getter.getDirSizeByReflection(@"D:\01lin")));
            Console.WriteLine(watch.ElapsedMilliseconds + " ms spent.\n");

            watch.Stop();
        }
    }
}
