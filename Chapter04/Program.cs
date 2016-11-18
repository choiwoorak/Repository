using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter04
{
    class Program
    {
        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        static void Main(string[] args)
        {
            var dataSource = Enumerable.Range(1, 1000000).ToArray(); //1000000

            var pLinqEvenNumbers1 = dataSource.
                                   AsParallel(). // Opt in to PLINQ
                                   Where(x => x % 2 == 0);

            var mEvenNumbers= dataSource.
                                   AsParallel(). // Opt in to PLINQ
                                   Where(x => IsEven(x));  

            // For문은 Loop중 가장 빠르나, 병렬 처리를 할수 없다.            
            List<int> evenNumbers = new List<int>();
            Stopwatch sw1 = Stopwatch.StartNew();
            for (int i = 0; i < dataSource.Count(); i++)
                if (dataSource[i] % 2 == 0)
                    evenNumbers.Add(dataSource[i]);

            sw1.Stop();
            Console.WriteLine("ForLoop - Elapsed Time : {0}", sw1.Elapsed);
            Console.WriteLine("==============================================================================");

            #region Introduction To PLINQ

            #region 병렬 쿼리 & Opt in Model
            // AsParallel() : 순차적 Linq를 병렬 처리하도록 한다. 
            // 병렬 쿼리: 형태 분석 => 분할 => 쿼리 수행 => 결과 병합 : 오버헤드(Overhead)            
            var pLinqEvenNumbers = dataSource.
                                   AsParallel(). // Opt in to PLINQ
                                   Where(x => x % 2 == 0);                                   

            #endregion
            #region 순서 지정
            // 병렬 처리되어 병합된 결과는 소스 Sequence의 순서가 유지 되지 않는다.
            Stopwatch sw2 = Stopwatch.StartNew();
            foreach (var n in pLinqEvenNumbers) { };
                //Console.WriteLine("Value : {0} ", n);
            sw2.Stop();
            Console.WriteLine("AsParallel - Elapsed Time : {0}", sw2.Elapsed);
            Console.WriteLine("==============================================================================");
            // Sequence가 유지된 결과를 얻고 싶을때는 AsOrder() 사용
            var pLinqOrderedEvenNumbers = dataSource.AsParallel().
                                          AsOrdered().
                                          Where(x => x % 2 == 0);                                          

            // Order된 결과           
            foreach (var n in pLinqOrderedEvenNumbers) { } 
                //Console.WriteLine("Value : {0}",  n);           
            Console.WriteLine("==============================================================================");
            #endregion
            #region 실행 모드
            // 런타임에서 소스 분석시 부담이 큰 쿼리나 속도 향상이 안되는 형태일 경우 순차적 처리
            // 병렬 처리를 강제 할 수 있다.             
            var pLinqEvenNumbers2 = dataSource.
                                   AsParallel().
                                   WithExecutionMode(ParallelExecutionMode.ForceParallelism). // 병렬 처리 되도록 강제함                                   
                                   Where(x => x % 2 == 0);

            Stopwatch swp1 = Stopwatch.StartNew();
            foreach (var n in pLinqEvenNumbers2) { };               
                    //Console.WriteLine("Value : {0}", n);           
            swp1.Stop();
            Console.WriteLine("AsParallel() ForceParallelism - Elapsed Time : {0}", swp1.Elapsed);
            Console.WriteLine("==============================================================================");
            #endregion
            #region 병렬 처리 수준
            // PLINQ에서는 가능한 모든 자원을 사용합니다. 프로세서를 제한할 수 있습니다.
            var DegreeParallelNumbers = dataSource.AsParallel().
                                        WithDegreeOfParallelism(2). // 병렬 처리 수준 지정
                                        Where(x => x % 2 == 0);
            #endregion
            #region 병렬 쿼리, 순차 쿼리
            // 병렬 처리보다 순차처리가 더 빠른 경우 후속 연산자 순차처리로 돌리기
            var evenNumbersSUM = dataSource.AsParallel().                                       
                                       Where(x => x % 2 == 0).
                                       AsSequential(). // Take는 순차 처리됨
                                       Take(5);
            #endregion
            #region ForAll 연산자
            // foreach를 사용하여 쿼리를 수행 할 수 있지만 foreach 자체는 병렬 처리 불가
            // 모든 병렬 작업은 foreach가 실행되는 스레드에 다시 병합되어야 한다. 
            // 순서를 유지할 필요가 없고 처리 결과를 병렬화 할 경우 ForAll()을 사용한다.
            // ForAll은 병합을 하지 않음            
            pLinqOrderedEvenNumbers.ForAll(n =>
            {
                //Console.WriteLine("Thread ID: {0} / Value : {1}",Thread.CurrentThread.ManagedThreadId, n);
            });
            Console.WriteLine("==============================================================================");
            #endregion
            #region PLINQ 성능 측정            
            var source = Enumerable.Range(0, 3000000);
            var queryToMeasure = from num in source.AsParallel()
                                 where num % 3 == 0
                                 select Math.Sqrt(num);

            // The query does not run until it is enumerated.
            // Therefore, start the timer here.
            Stopwatch sw = Stopwatch.StartNew();
            // For pure query cost, enumerate and do nothing else.
            foreach (var n in queryToMeasure) { }
            sw.Stop();

            long elapsed = sw.ElapsedMilliseconds; // or sw.ElapsedTicks
            Console.WriteLine("Total query time: {0} ms", elapsed);
            #endregion   
            #region 병렬 처리 했지만 왜 느릴까?
            // 병렬 쿼리를 병렬화 하는데 필요한 Overhead를 고려해야 한다. => 데이터가 적거나 쿼리의 형태가 병렬처리에 부적합한건 아닌지?
            // 성능 저하 원인 : 1. 쿼리내 ToArray, ToList,ToDictionary 경우 모든 병렬 스레드의 결과는 단일 데이터 구조에 병합되어야 하고 이에 따른 비용이 발생
            //                  2. foreach 루프를 사용 하여 결과를 반복하는 경우 작업자 스레드는 열거자 스레드에서 Serialize되어야 한다.
            #endregion

            #endregion
            Console.ReadKey();

        }
    }
}
