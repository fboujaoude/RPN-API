using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPN.Models
{
    public class StackModel
    {
        public int Id { get; set; }

        public Stack<string> Operand { get; set; }
    }
}
