using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    class Program
    {       
        static void Main(string[] args)
        {
            #region Create DataSource
            List<Person> people = new List<Person>();
            people.Add(new Person() { Name = "김철수", Id = "1" });
            people.Add(new Person() { Name = "박영희", Id = "2" });
            people.Add(new Person() { Name = "오나라", Id = "3" });
            people.Add(new Person() { Name = "박철수", Id = "4" });
            people.Add(new Person() { Name = "한영희", Id = "5" });
            people.Add(new Person() { Name = "홍길동", Id = "6" });

            List<Item> items = new List<Item>();
            items.Add(new Item() { Name = "컴퓨터", Type = "가전", Price = 1000000 });
            items.Add(new Item() { Name = "모니터", Type = "가전", Price = 300000 });
            items.Add(new Item() { Name = "냉장고", Type = "가전", Price = 700000 });
            items.Add(new Item() { Name = "흠정역성경", Type = "책", Price = 20000 });
            items.Add(new Item() { Name = "C언어 입문", Type = "책", Price = 35000 });
            items.Add(new Item() { Name = "Visual Studio", Type = "소프트웨어", Price = 500000 });

            List<Sale> sales = new List<Sale>();
            sales.Add(new Sale() { Customer = "1", ItemName = "컴퓨터" });
            sales.Add(new Sale() { Customer = "3", ItemName = "모니터" });
            sales.Add(new Sale() { Customer = "2", ItemName = "컴퓨터" });
            sales.Add(new Sale() { Customer = "4", ItemName = "흠정역성경" });
            sales.Add(new Sale() { Customer = "1", ItemName = "흠정역성경" });
            sales.Add(new Sale() { Customer = "5", ItemName = "컴퓨터" });
            sales.Add(new Sale() { Customer = "5", ItemName = "C언어 입문" });
            sales.Add(new Sale() { Customer = "5", ItemName = "Visual Studio" });
            sales.Add(new Sale() { Customer = "7", ItemName = "DAQ" });

            string[] names = people.Select(p => p.Name).ToArray();
            string[] addNames = { "박찬호", "추성훈", "김동헌", "홍길동", "홍길동", "홍길동" };

            #endregion

            #region Join(Inner Join) 
            Console.WriteLine("Join(inner)--------------------------------------");
            Console.WriteLine("Q. 각 사람들이 구매한 물품은?");
            // Query: - Left Sequence기준으로, 조건에 부합되는 데이터만 Join된다.
            var salesNamesQuery = from p in people
                                  join s in sales on p.Id equals s.Customer 
                                  select new
                                  {
                                      p.Name,
                                      s.ItemName
                                  };
            // Method
            var salesNames = people.Join(sales,
                             p => p.Id, s => s.Customer,
                             (p, s) => new
                             {
                                 Name = p.Name,
                                 ItemName = s.ItemName
                             });

            foreach (var v in salesNamesQuery)
            {
                Console.WriteLine("{0}(이)가 {1}을 구매했음", v.Name, v.ItemName);
            }

            #endregion

            #region GroupJoin
            Console.WriteLine("\r\nGroupJoin--------------------------------------");
            Console.WriteLine("Q. 각 사람들이 구매한 물품의 개수는?");
            // Query: Group Join
            var salesCountQuery = from p in people
                                  join s in sales on p.Id equals s.Customer
                                  into orders
                                  select new
                                  {
                                      p.Name,
                                      Count = orders.Count()
                                  };

            // Method
            var salesCount = people.GroupJoin(sales, //from
                             p => p.Id, s => s.Customer, //on, Equal
                             (p, orders) => new //into
                             {
                                 p.Name,
                                 Count = orders.Count()
                             });

            foreach (var v in salesCountQuery)
            {
                Console.WriteLine("{0}(이)가 {1}개의 물건을 구매하였음", v.Name, v.Count);
            }

            #endregion

            #region GroupBy
            Console.WriteLine("\r\nGroupBy--------------------------------------");
            Console.WriteLine("Q. 개별 구매한 물품의 누적 금액?");
            // Query: 이름, 아이템, 가격 얻기
            var saleGroupQuery = from p in salesNamesQuery
                          join s in items on p.ItemName equals s.Name
                          into eachPerson
                          select new
                          {
                              Name = p.Name,
                              Item = p.ItemName,
                              Price = (from man in eachPerson
                                       where man.Name == p.ItemName
                                       select man.Price).Sum()
                          };

            // 이름별 가격 Sum구하기
            var groupNames1 = (from item in saleGroupQuery
                               group item by item.Name)
                              .Select(p => new
                              {
                                  Name = p.ElementAt(0).Name,
                                  total = p.Sum(m => m.Price)
                              });

            // Method
            var saleGroupMethod = salesNames.GroupJoin(items,
                                  man => man.ItemName, item => item.Name,
                                  (man, eachPerson) => new
                                  {
                                      Name = man.Name,
                                      Item = man.ItemName,
                                      Price = eachPerson
                                              .Where(v => v.Name == man.ItemName).Sum(v => v.Price)
                                  });

            var groupNames = saleGroupMethod.
                             GroupBy(man => man.Name).
                             Select(p => new
                             {
                                 Name = p.ElementAt(0).Name,
                                 total = p.Sum(m => m.Price)
                             });

            foreach (var item in groupNames)
            {
                Console.WriteLine("{0}의 구매 누적 금액은 {1}입니다. ", item.Name.ToString(), item.total);
            }
            #endregion

            #region Join(Outer Join) 
            Console.WriteLine("Join(outter)--------------------------------------");
            Console.WriteLine("Q. 각 사람들이 구매한 물품은?");
            // Query - Join한 결과에 대해 DefaultIfEmpty 연산으로 비어 있는 조인 결과에 빈값을 채워 넣는다.
            var tempGroupQuery = from p in people
                                  join s in sales on p.Id equals s.Customer
                                  into tempGroup
                                  select new
                                  {
                                      p.Name,
                                      Items = tempGroup.Where(s => s.Customer == p.Id).Select(s => s.ItemName)
                                  };

            foreach (var item in tempGroupQuery)
                foreach (var data in item.Items)
                    Console.WriteLine("{0} / {1}", item.Name, data);
            Console.WriteLine();

            var salesNamesOutterQuery = from p in people
                                   join s in sales on p.Id equals s.Customer
                                   into tempGroup
                                   from s in tempGroup.DefaultIfEmpty(new Sale() { Customer = p.Name, ItemName = "Empty" })
                                   select new
                                   {
                                       p.Name,
                                       s.ItemName
                                   };
              
            // Method
            var salesNamesOutterMethod = people
                                         .GroupJoin(sales,
                                         p => p.Id, s => s.Customer,
                                         (p, tempGroup) => new
                                         {
                                             people = p,
                                             sales = tempGroup.DefaultIfEmpty(new Sale() { Customer = p.Name, ItemName = "Empty" })
                                         })
                                         .SelectMany(s => s.sales,
                                         (s, y) => new
                                         { 
                                            Name = s.people.Name, 
                                            ItemName = y.ItemName 
                                         });

            foreach (var v in salesNamesOutterMethod)
            {
                if(v.ItemName == "Empty")
                    Console.WriteLine("{0}(이)가 구매한 물품이 없음", v.Name);
                else
                    Console.WriteLine("{0}(이)가 {1}을 구매했음", v.Name, v.ItemName);
            }

            #endregion

            #region Union Operator
            Console.WriteLine("\r\nUnion--------------------------------------");
            var unionPeople = names.Union(addNames);
            foreach (var p in unionPeople)
            {
                Console.WriteLine("Name: {0}", p);
            }
            #endregion

            #region Intersect Operator
            Console.WriteLine("\r\nIntersect--------------------------------------");
            var intersectPeople = names.Intersect(addNames);
            foreach (var p in intersectPeople)
            {
                Console.WriteLine("Name: {0}", p);
            }

            #endregion

            #region Except Operator
            Console.WriteLine("\r\nExcept--------------------------------------");
            var exceptPeople = names.Except(addNames);
            foreach (var p in exceptPeople)
            {
                Console.WriteLine("Name: {0}", p);
            }

            #endregion

            #region Distinct Operator
            Console.WriteLine("\r\nDistinct--------------------------------------");
            var distinctList = addNames.Distinct();
            foreach (var p in distinctList)
            {
                Console.WriteLine("Name: {0}", p);
            }

            #endregion

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();     
        }


    }
}
