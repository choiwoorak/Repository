using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace instance
{
    public static class InstanceMethods
    {
        public static int multiplyByTwo(int value)
        {
            return value * 2;
        }

        public static string UppercaseFirstLetter(string value)
        {
            if (value.Length > 0)
            {
                char[] array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);
                return new string(array);
            }
            return value;
        }

        public static IEnumerable<T> Log<T>(IEnumerable<T> value)
        {
            if (value.Count() > 0 && value != null)
            {
                Console.WriteLine("---Start Log---");
                value.ToList().ForEach(data => Console.WriteLine(data));
                Console.WriteLine("---End Log---");
            }

            return value;
        }
    }
}