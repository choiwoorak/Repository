using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    class profile
    {
        public string name { get; set; }
        public int age { get; set; }
        public int height { get; set; }
    }

    class petOwner
    {
        public string name { get; set; }
        public List<string> pets { get; set; }
    }
}
