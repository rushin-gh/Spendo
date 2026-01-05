using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apis.Models;

namespace apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        public IActionResult GetExpenses(ExpenseDTO expenseDto)
        {
            return Ok(new[] { "Expense1", "Expense2", "Expense3" });
        }
    }
}
