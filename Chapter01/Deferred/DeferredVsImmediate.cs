using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class DeferredVsImmediate
    {
        List<Employee> empList = new List<Employee>(new Employee[]
            {
                new Employee{ ID = 1, Name = "Jack", Age = 32 },
                new Employee{ ID = 2, Name = "Rahul", Age = 35 },
                new Employee{ ID = 3, Name = "Sheela", Age = 28 },
                new Employee{ ID = 4, Name = "Mary", Age = 25 }
            });

        public void ImmediateRun()
        {
            //.To**로 시작되는 Method, 단일값을 Return하는 Method는 즉시 처리
            var lst = (from e in empList
                      where e.Age > 28
                      select new { e.Name }).ToList();

            foreach (var emp in lst)
                Console.WriteLine(emp.Name);

        }

        public void deferredRun()
        {
            // Return Type이 IEnumable<T>인 경우 지연 처리
            var lst = from e in empList
                      where e.Age > 28 //Query Variable
                      select new { e.Name };

            empList.Add(new Employee { ID = 4, Name = "choi", Age = 32 }); //Add New employee 

            foreach (var emp in lst)
                Console.WriteLine(emp.Name);

        }

    }
}
