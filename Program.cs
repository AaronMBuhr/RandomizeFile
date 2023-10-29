using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RandomizeFile
{
    class Program
    {

        static void RandomizeFile(string filename, long filesize)
        {
            var rand = new Random();
            byte[] random_bytes = new byte[filesize];
            rand.NextBytes(random_bytes);
            try
            {
                using (var stream = File.Open(filename, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.ASCII))
                    {
                        writer.Write(random_bytes);
                    }
                }
                Console.WriteLine(filename + " randomized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(filename + "error: " + ex.Message);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("RandomizeFile - fills a file with random values");
                Console.WriteLine("Usage: randomizefile <filespec>");
                Environment.Exit(0);
            }
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            var arg_dir = (args[0].Contains(Path.DirectorySeparatorChar) ? Path.GetDirectoryName(args[0]) : "." + Path.DirectorySeparatorChar);
            var arg_filespec = Path.GetFileName(args[0]);
            Console.WriteLine(arg_dir);
            Console.WriteLine(arg_filespec);
            DirectoryInfo d_info = new DirectoryInfo(arg_dir);
            FileInfo[] files = d_info.GetFiles(arg_filespec);

            foreach (var file in files)
            {
                RandomizeFile(file.FullName, file.Length);
            }

        }
    }
}
