using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01.ReplacingLINQ
{
    class ReplacingLoopByusingLinq
    {
        public void Run()
        {
            int[] numbers = { 14, 21, 24, 51, 131, 1, 11, 54 };
            int value = 50;

            ShowListValue(numbers.ToList(), "numbers");
            Console.WriteLine("Threshold: {0}", value);
            Console.WriteLine("Condition: n > {0}", value);

            ShowListValue(usingForLoop(numbers, value), "For");
            ShowListValue(usingForeachLoop(numbers, value), "Foreach");
            ShowListValue(usingLINQ(numbers, value), "Linq");
        }

        private void ShowListValue(List<int> numbers, string text)
        {
            Console.Write("{0} : ", text);
            numbers.ForEach(value =>
            {
                Console.Write("{0}", value);
                if (value != numbers.Last())
                    Console.Write(",");
            });

            Console.WriteLine();
        }

        private List<int> usingForLoop(int[] numbers, int threshole)
        {
            List<int> goodNumbers = new List<int>();
            for (int k = 0; k < numbers.Length; k++)
                if (numbers[k] > threshole)
                    goodNumbers.Add(numbers[k]);

            return goodNumbers;
        }

        private List<int> usingForeachLoop(int[] numbers, int threshole)
        {
            List<int> goodNumbers = new List<int>();
            foreach (var n in numbers)
                if (n > threshole)
                    goodNumbers.Add(n);

            return goodNumbers;
        }

        private List<int> usingLINQ(int[] numbers, int threshole)
        {
            var goodNumbers = numbers
                              .Where(x => x > threshole)
                              .ToList();

            //var goodNumbers = from number in numbers
            //                  where number > threshole
            //                  select number;

            return goodNumbers.ToList();
        }
    }
}
