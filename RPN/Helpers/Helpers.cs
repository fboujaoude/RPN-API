using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPN.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Validates if the given string opertor exists in the list of operators
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if available in operators list and false if not</returns>
        public static bool IsOperator(string str)
        {
            return (str == "-" || str == "+" || str == "*" || str == "/");
        }

        /// <summary>
        /// Validates the given string if double or not
        /// </summary>
        /// <param name="str">Entry string</param>
        /// <returns>true if double and false if not</returns>
        public static bool IsDouble(string str)
        {
            Double num = 0;
            bool isDouble = false;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            isDouble = Double.TryParse(str, out num);

            return isDouble;
        }

        /// <summary>
        /// Removes a value from stach
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="stack"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T Remove<T>(this Stack<T> stack, T element)
        {
            T obj = stack.Pop();
            if (obj.Equals(element))
            {
                return obj;
            }
            else
            {
                T toReturn = stack.Remove(element);
                stack.Push(obj);
                return toReturn;
            }
        }
    }
}
