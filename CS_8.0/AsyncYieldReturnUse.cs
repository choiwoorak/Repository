using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AsyncYieldReturnUse
    {
        [Benchmark]
        public static async void YieldReadFileAsync(string filePath)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            IAsyncEnumerable<Person> perons = GetPersonAsync(1000, token.Token);

            
            await foreach (var user in perons.WithCancellation(token.Token))
                Console.WriteLine($"Name:{user.Name}");


            var result = perons.WhereAwait(x => ValueTask.FromResult(x.Name == "20"));
            await foreach (var user in result.WithCancellation(token.Token))
                Console.WriteLine($"Name(20):{user.Name}");


            //var lines = GetLinesAsync(filePath);
            //await foreach (var line in lines)
            //    Console.WriteLine(line);
        }
        [Benchmark]
        private static async IAsyncEnumerable<string> GetLinesAsync(string filePath)
        {
            string line;
            var fileStream = new StreamReader(filePath);
            while ((line = await fileStream.ReadLineAsync()) != null)
            {
                //await Task.Delay(100);
                yield return line;
            }

        }
        private static async IAsyncEnumerable<Person> GetPersonAsync(int count, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(100);
                if (cancellationToken.IsCancellationRequested)
                    break;

                yield return new Person(i.ToString(), i);
            }
                
        }
    }
}
