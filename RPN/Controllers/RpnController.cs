using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RPN.Data;
using RPN.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPN.Helpers;

namespace RPN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RpnController : ControllerBase
    {

        private readonly IRepository repository;
        List<string> Operands = new List<string>() { "+", "-", "*", "/" };

        public RpnController(IRepository repository)
        {
            this.repository = repository;
            ////added for testing purposes

            //DB.StackDB = new Stack<StackModel>();
            //Stack<double> st1 = new Stack<double>();
            //st1.Push(10);
            //st1.Push(5);
            //st1.Push(6);
            ////Stack<string> st2 = new Stack<string>();
            ////st2.Push("5");
            ////Stack<string> st3 = new Stack<string>();
            ////st3.Push("6");
            ////DB.StackDB.Push( new StackModel() { id = 1, stack = st1 });
            ////DB.StackDB.Push(new StackModel() { id = 2, stack = st2 });
            //DB.StackDB.Push(new StackModel() { id = 1, stack = st1 });
        }

        /// <summary>
        /// List all the Operands
        /// </summary>
        /// <returns>List of operands</returns>
        [HttpGet]
        [Route("op")]
        public IActionResult GetOp()
        {
            return new JsonResult(Operands);
        }

        /// <summary>
        /// List the available stacks
        /// </summary>
        /// <returns>List of available stack</returns>
        [HttpGet]
        [Route("stack")]
        public IActionResult GetAvailableStacks()
        {
            return new JsonResult(repository.GetStacks());
        }

        /// <summary>
        /// Get a stack
        /// </summary>
        /// <param name="stack_id">stack id</param>
        /// <returns>returns the found stack or null if not found</returns>
        [HttpGet]
        [Route("stack/{stack_id}")]
        public IActionResult GetStack(int stack_id)
        {
            StackModel stack = repository.GetStackById(stack_id);
            if (stack == null)
                return NotFound();
            else
                return new JsonResult(stack);
        }

        /// <summary>
        /// Create a new stack
        /// </summary>
        /// <returns>Ok if successfully added and Bad Request if not</returns>
        [HttpPost]
        [Route("stack")]
        public IActionResult PostStack()
        {
            try
            {
                repository.CreateStack();
                return Ok(repository.GetStacks().Peek());
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create stack");
            }
        }

        /// <summary>
        /// Push a new value to a stack
        /// </summary>
        /// <param name="stack_id">stack id to push into</param>
        /// <param name="val">the value to be pushed</param>
        /// <returns>Ok if successful and bad request with message if not</returns>
        [HttpPost]
        [Route("stack/{stack_id}")]
        public IActionResult PostAddToStack(int stack_id, [FromBody]string val)
        {
            if (!(Helper.IsDouble(val)))
                return BadRequest("Value should be a double or a valid operation");

            StackModel stack = repository.AddStackValue(stack_id, val);
            return Ok(stack);
        }


        /// <summary>
        /// Delete a stack
        /// </summary>
        /// <param name="stack_id">stack id to be deleted</param>
        /// <returns>Ok if successful and bad request with message if not</returns>
        [HttpDelete]
        [Route("stack/{stack_id}")]
        public IActionResult DeleteStack(int stack_id)
        {
            StackModel stack = repository.RemoveStack(stack_id);
            if (stack == null)
                return NotFound();
            else
            {
                return Ok(stack);
            }
        }

        /// <summary>
        /// Apply an operand to a stack
        /// </summary>
        /// <param name="op">operand to be applied</param>
        /// <param name="stack_id">stack id where to apply the operand</param>
        /// <returns>Ok if successful and bad request with message if not</returns>
        [HttpPost]
        [Route("stack/op/{op}/stack/{stack_id}")]
        public IActionResult PostApplyOperand(string op, int stack_id)
        {
            // (Could be wrong but let me explain what i did)
            // This method as i understood the requirements will add the operand to
            // a specific stach in the list of stacksa dn will apply this operand
            // to the last 2 consecutive values as rpn algorithm indicates

            StackModel st = repository.GetStackById(stack_id);
            if (st == null)
                return BadRequest("Wrong stack id");

            if (!Helper.IsOperator(op))
                return BadRequest("Wrong operator");

            repository.ApplyOperandToStack(op, st);
            return Ok(repository.GetStacks());

            //double result = 0;
            //StackModel st = DB.StackDB.Where(s => s.id == stack_id).FirstOrDefault();
            //if (st == null)
            //    return BadRequest("Wrong stack id");

            //if (!Helper.IsOperator(op))
            //    return BadRequest("Wrong operator");

            //if (DB.StackDB.Count == 0)
            //    return Ok(0);
            //else
            //{
            //    switch (op)
            //    {
            //        case "+":
            //            result = Convert.ToDouble(st.stack.Pop());
            //            double popedAdd = 0;
            //            st.stack.Push((st.stack.TryPop(out popedAdd) ? popedAdd : 0) + result);
            //            break;
            //        case "*":
            //            result = Convert.ToDouble(st.stack.Pop());
            //            double popedTimes = 0;
            //            st.stack.Push((st.stack.TryPop(out popedTimes) ? popedTimes : 0) * result);
            //            break;
            //        case "/":
            //            result = Convert.ToDouble(st.stack.Pop());
            //            double popedDivided = 0;
            //            st.stack.Push((st.stack.TryPop(out popedDivided) ? popedDivided : 0) / result);
            //            break;
            //        case "-":
            //            result = Convert.ToDouble(st.stack.Pop());
            //            double popedMinus = 0;
            //            st.stack.Push((st.stack.TryPop(out popedMinus) ? popedMinus : 0) - result);
            //            break;
            //        default:
            //            break;
            //    }
            //    return Ok(DB.StackDB);
            //}

            // Commented out as I wasn't really sure if i got the point of the requirement for this method
            // even the other methods were done in different comprehensive way but have choosen this one at the end

            //if (EntryIsValid(stack_id))
            //    return BadRequest("Wrong entry");

            //double result = 0;

            //if (DB.StackDB2.Count == 0)
            //    return Ok(0);
            //else if (DB.StackDB2.Count == 1)
            //    return Ok(DB.StackDB2.Peek());
            //else
            //{
            //    switch (op)
            //    {
            //        case "+":
            //            result = Convert.ToDouble(DB.StackDB2.Pop());
            //            DB.StackDB2.Push(Convert.ToString(Convert.ToDouble(DB.StackDB2.Pop()) + result));
            //            break;
            //        case "*":
            //            result = Convert.ToDouble(DB.StackDB2.Pop());
            //            DB.StackDB2.Push(Convert.ToString(Convert.ToDouble(DB.StackDB2.Pop()) * result));
            //            break;
            //        case "/":
            //            result = Convert.ToDouble(DB.StackDB2.Pop());
            //            DB.StackDB2.Push(Convert.ToString(Convert.ToDouble(DB.StackDB2.Pop()) / result));
            //            break;
            //        case "-":
            //            result = Convert.ToDouble(DB.StackDB2.Pop());
            //            DB.StackDB2.Push(Convert.ToString(Convert.ToDouble(DB.StackDB2.Pop()) - result));
            //            break;
            //        default:
            //            break;
            //    }
            //    return Ok(DB.StackDB2);
            //}

        }
    }
}
