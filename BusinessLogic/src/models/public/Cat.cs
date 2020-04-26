using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.models
{
    public class Cat : Pet
    {
        public string name { get; set; }
        public string address { get; set; }
        public string owner { get; set; }
        public string breed { get; set; }
        public string breeder { get; set; }
        public bool outdoor { get; set; }
        public int weight { get; set; }
    }
}
