using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPN.Models
{
    public class StackModel
    {
        public int id { get; set; }

        public Stack<double> stack { get; set; }
    }
}
