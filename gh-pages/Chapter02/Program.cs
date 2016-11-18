using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DataSource
            profile[] datasource = {
                                       new profile() { name = "CHOI", age = 10, height = 121},
                                       new profile() { name = "KIM", age = 12, height = 130},
                                       new profile() { name = "GO", age = 13, height = 151},
                                       new profile() { name = "PARK", age = 25, height = 170},
                                       new profile() { name = "LEE", age = 30, height = 178},
                                       new profile() { name = "HONG", age = 33, height = 190},
                                       new profile() { name = "MIN", age = 20, height = 157}
                                   };

            petOwner[] owner =  {
                                    new petOwner{name = "JO", pets = new List<string>() {"Dog", "Cat"}},
                                    new petOwner{name = "SEO", pets = new List<string>() {"Pig", "Horse"}},
                                    new petOwner{name = "YOON", pets = new List<string>() {"Dog"}}
                                };
            #endregion

            // Q. 성인은 누구?
            #region Select, Where
            var adults = datasource
                         .Where(person => person.age >= 20)
                         .Select(person => person.name);

            Console.WriteLine("Q. 성인은 누구?");
            foreach (var value in adults)
                Console.WriteLine(value);
            Console.WriteLine("-------------------------------------------------");
            #endregion

            // Q. 성인중 키가 175 넘는 사람은?
            #region Select, Where, 익명 객체
            var adultsTaller1 = datasource
                               .Where(person => person.age >= 20 && person.height >= 175)
                               .Select(adult => new 
                               {
                                   Name = adult.name,
                                   Height = adult.height 
                               });                               

            Console.WriteLine("Q. 성인중 키가 175 넘는 사람은?");
            foreach (var value in adultsTaller1)
                Console.WriteLine(value);
            Console.WriteLine("-------------------------------------------------");
            #endregion

            // Q. 가장 나이가 어린 사람순으로 정렬
            #region OrderBy, Range, ElementAt
            var orderYoungList = datasource.OrderBy(person => person.age);
            var adultYoungTaller = Enumerable.Range(0, datasource.Count()).Select(num => new
            {
                No = num+1,
                Name = orderYoungList
                       .ElementAt(num).name
            });

            Console.WriteLine("Q. 가장 나이가 어린 사람순으로 정렬");
            foreach (var value in adultYoungTaller)
                Console.WriteLine(value);
            Console.WriteLine("-------------------------------------------------");
            #endregion

            // Q. 가장 나이가 많은 사람순으로 정렬
            #region OrderbyDecending, Range, ElementAt
            var orderOldList = datasource.OrderByDescending(person => person.age);
            var adultOldTaller = Enumerable.Range(0, datasource.Count()).Select(num => new
            {
                No = num + 1,
                Name = orderOldList
                       .ElementAt(num).name
            });

            Console.WriteLine("Q. 가장 나이가 많은 사람순으로 정렬");
            foreach (var value in adultOldTaller)
                Console.WriteLine(value);
            Console.WriteLine("-------------------------------------------------");
            #endregion

            // Q. List of List
            #region SelectMany Vs. Select
            Console.WriteLine("Using Select #1");
            var selectResult = owner.Select(person => person.pets);
            foreach (var data in selectResult)
                Console.WriteLine(data);

            Console.WriteLine("Using Select #1");            
            foreach (var data in selectResult)
                foreach (var value in data)
                    Console.WriteLine(value);
            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Using SelectMany");
            var selectManyResult = owner.SelectMany(pet => pet.pets);
            foreach (var data in selectManyResult)
                Console.WriteLine(data);

            #endregion

            Console.WriteLine("-------------------------------------------------");
            Console.ReadKey();
        }
    }
}
