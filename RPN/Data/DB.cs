using RPN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPN.Data
{
    /// <summary>
    /// Created instead of a sql db just to gain time 
    /// </summary>
    public static class DB
    {
        public static Stack<StackModel> StackDB { get; set; }
    }

}
