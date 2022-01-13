using Chapter01.ReplacingLINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instance;
using extension;

namespace Chapter01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Language INtergrated Query : 데이터 질의 기능.
            // 기존 Loop와 Linq Code비교.
            ReplacingLoopByusingLinq replaceLoop = new ReplacingLoopByusingLinq();
            replaceLoop.Run();

            int luckyNumber = 7;
            int unluckyNumber = 4;
            int value = 0;
            int sum = 0;

            int multipleLuckyNumber1 = luckyNumber.multiplyByTwo(); //Extension
            int multipleLuckyNumber2 = InstanceMethods.multiplyByTwo(luckyNumber); //Instance
                                   
            Func<int,int,int> Plus = (a, b) =>  a+b; //Return 존재
            Action<int, int> Minus = (a, b) =>  value = a-b; // Void
            // (a,b ) = a+b ;
            // Lamda Expression : (input parameters) => expression
            // Linq Query식을 만드는데 유용하게 사용.                        
            Console.WriteLine(sum);    
            sum = Plus(luckyNumber,unluckyNumber);
            Console.WriteLine(sum);

            Minus(luckyNumber,unluckyNumber);
            Console.WriteLine(value);

            DeferredVsImmediate obj = new DeferredVsImmediate();
            obj.ImmediateRun();
            obj.DeferredRun();
            

            Console.ReadKey();
       
        }

    }
}
