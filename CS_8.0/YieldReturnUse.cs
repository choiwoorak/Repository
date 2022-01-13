using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class YieldReturnUse
    {
        [Benchmark]
        public static void YieldReadFile(string filePath)
        {
            var lines = GetLinesSync(filePath);
            foreach (var line in lines)
                Console.WriteLine(line);
        }
        [Benchmark]
        private static IEnumerable<string> GetLinesSync(string filePath)
        {
            var fileStream = new StreamReader(filePath);
            string line;
            while ((line = fileStream.ReadLine()) != null)
            {
                //Thread.Sleep(100);
                yield return line;
            }
        }
    }
}
