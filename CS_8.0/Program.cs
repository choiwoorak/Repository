
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ConsoleApp1;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        string filePath = @"F:\WRCHOI.txt";
        [STAThread]
        public static void Main(string[] args)
        {
            //var summary1 = BenchmarkRunner.Run(typeof(ReturnUse));
            //var summary2 = BenchmarkRunner.Run(typeof(YieldReturnUse));
            //var summary3 = BenchmarkRunner.Run(typeof(AsyncYieldReturnUse));
            //var summary4 = BenchmarkRunner.Run(typeof(Program));

            //Console.WriteLine("Using return");

            //ReturnUse.ReadFile(filePath);
            ////Console.ReadKey();

            //Console.WriteLine("Using yield return");
            //YieldReturnUse.YieldReadFile(filePath);
            ////Console.ReadKey();

            Console.WriteLine("Using async yield return");
            AsyncYieldReturnUse.YieldReadFileAsync("");

            Console.ReadKey();
        }

        [Benchmark]
        public void Run1() => ReturnUse.ReadFile(filePath);
        [Benchmark]
        public void Run2() => YieldReturnUse.YieldReadFile(filePath);
        [Benchmark]
        public void Run3() => AsyncYieldReturnUse.YieldReadFileAsync(filePath);
    }
}
