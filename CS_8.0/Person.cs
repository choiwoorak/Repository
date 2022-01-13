using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Person
    {
        public Person(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }
        public string Name { get; set; }    
        public int Id { get; set; } 
        
    }
}
