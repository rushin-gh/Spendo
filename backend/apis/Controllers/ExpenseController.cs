using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apis.Models;
using System.Reflection.PortableExecutable;

namespace apis.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromBody] ExpenseModel expenseDto)
        {
            var result = new Result();
            try
            {
                
            }
            catch (Exception ex)
            {

            }
            return Ok(result);
        }
    }
}
