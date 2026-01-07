using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apis.Models;
using System.Reflection.PortableExecutable;

namespace apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetExpenses([FromQuery]ExpenseDTO expenseDto)
        {
            return Ok(new[] { "Expense1", "Expense2", "Expense3" });
        }

        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromBody] ExpenseDTO expenseDto)
        {
            var result = new Result();
            try
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return Ok(result);
        }
    }
}
