using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class WithNull
    {
        internal Employee employee;
        public WithNull()
        {
            //employee = new Employee();

            // null 체크하는 코드(employee = employess ?? new Employee(); 와 같은 코드)
            employee ??= new Employee();


        }

        public int CalculatorFee(object vehicle)
            => vehicle switch
            {
                Car car when car.Fee < 10 => 10,
                Car car when car.Fee >= 10 => 100,
                Bus _ => 0,
                MotorCycle _ => 0,
                null => -1,
                _ => throw new NotImplementedException(),
            };

        /// <summary>
        /// C# 8.0 부터 가능한 재귀 패턴식으로 Null체크
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal int GetEmployeeNameLength(Employee employee)
            => employee?.Name is { Length : var length } userNameLength ? length : 0;
    }

    internal class Employee
    {
        #nullable disable
        public string Name { get; set; }    
        public string FirstName { get; set; }
        
        public int Id { get; set; }
    }

    class Car 
    { 
        public int Fee { get; set; }
    }
    class Bus 
    {
        public int Fee { get; set; }
    }
    class MotorCycle 
    {
        public int Fee { get; set; }
    }
}
