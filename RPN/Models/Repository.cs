using RPN.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPN.Models
{
    public interface IRepository
    {
        Stack<double> GetOperands();
        Stack<StackModel> GetStacks();
        StackModel GetStackById(int id);
        StackModel AddStackValue(int id,string op);
        StackModel RemoveStack(int id);
        void CreateStack();
        void ApplyOperandToStack(string op,StackModel sm);
    }
    public class Repository : IRepository
    {
        private Stack<StackModel> Stacks;

        public Stack<StackModel> GetStacks()
        {
            //StackModel sm = new StackModel();
            //sm.id = 1;
            //sm.stack = new Stack<double>();
            //sm.stack.Push(10);
            //sm.stack.Push(5);
            //sm.stack.Push(6);

            //Stacks.Push(sm);
            return Stacks;
        }

        public StackModel GetStackById(int id)
        {
            StackModel sm = Stacks.Where(st => st.Id == id).FirstOrDefault();
            if (sm != null)
                return sm;
            else
                return null;
        }


        public StackModel AddStackValue(int id, string op)
        {
            StackModel sm = Stacks.Where(st => st.Id == id).FirstOrDefault();
            if (sm != null)
            {
                sm.Operand.Push(op);
            }
            return sm;
        }

        public StackModel RemoveStack(int id)
        {
            StackModel sm = Stacks.Where(st => st.Id == id).FirstOrDefault();
            if (sm != null)
            {
                Stacks.Remove(sm);
            }
            return sm;
        }

        public void CreateStack()
        {
            if (Stacks == null)
            {
                Stacks = new Stack<StackModel>();
            }

            StackModel stack = new StackModel();
            stack.Id = Stacks.Count + 1;
            stack.Operand = new Stack<string>();
            Stacks.Push(stack);
        }

        public void ApplyOperandToStack(string op, StackModel sm)
        {
            double result = 0;

            if (Stacks.Count == 0 || Stacks.Count == 1)
                return;
            else
            {
                switch (op)
                {
                    case "+":
                        result = Convert.ToDouble(sm.Operand.Pop());
                        string popedAdd = "0";
                        sm.Operand.Push(Convert.ToString((sm.Operand.TryPop(out popedAdd) ? Convert.ToDouble(popedAdd) : 0) + result));
                        break;
                    case "*":
                        result = Convert.ToDouble(sm.Operand.Pop());
                        string popedTimes = "0";
                        sm.Operand.Push(Convert.ToString((sm.Operand.TryPop(out popedTimes) ? Convert.ToDouble(popedTimes) : 0) * result));
                        break;
                    case "/":
                        result = Convert.ToDouble(sm.Operand.Pop());
                        string popedDivided = "0";
                        sm.Operand.Push(Convert.ToString((sm.Operand.TryPop(out popedDivided) ? Convert.ToDouble(popedDivided) : 0) / result));
                        break;
                    case "-":
                        result = Convert.ToDouble(sm.Operand.Pop());
                        string popedMinus = "0";
                        sm.Operand.Push(Convert.ToString((sm.Operand.TryPop(out popedMinus) ? Convert.ToDouble(popedMinus) : 0) - result));
                        break;
                    default:
                        break;
                }
            }
        }

        public Stack<double> GetOperands()
        {
            return new Stack<double>();
        }

        
    }
}
