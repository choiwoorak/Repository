using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }
 
    class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
 
    class Sale
    {
        public string Customer { get; set; }
        public string ItemName { get; set; }
    }

    

}
