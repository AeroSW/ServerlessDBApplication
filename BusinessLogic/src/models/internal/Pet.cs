using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.models
{
    internal interface Pet
    {
        string name { get; set; }
        string address { get; set; }
        string owner { get; set; }
    }
}
