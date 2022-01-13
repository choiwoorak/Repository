using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ReturnUse
    {
        [Benchmark]
        public static void ReadFile(string filePath)
        {
            var lines = GetLines(filePath);
            foreach (var line in lines)
                Console.WriteLine(line);
        }
        [Benchmark]
        private static string[] GetLines(string filePath)
        {
            var files = new List<string>();
            return File.ReadAllLines(filePath);
        }
    }
}
