using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apis.Models;
using System.Reflection.PortableExecutable;
using apis.Database;
using apis.Models.Expense;
using apis.Models.Expense.DTOs;

namespace apis.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("get")]
        public ActionResult<List<ExpenseResponseDTO>> GetExpenses()
        {
            List<ExpenseResponseDTO> expensesDto = new List<ExpenseResponseDTO>();
            try
            {
                var expenses = _appDbContext.Expenses.ToList();
                expenses.ForEach(exp =>
                {
                    expensesDto.Add(
                        new ExpenseResponseDTO
                        {
                            Id = exp.Id,
                            Title = exp.Title,
                            Amount = exp.Amount,
                            Description = exp.Description
                        }
                    );
                });
            }
            catch(Exception ex)
            {
                // Exception logging
            }
            return Ok(expensesDto);
        }

        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromQuery] ExpenseCreateDTO expenseDto)
        {
            var result = new Result();
            try
            {
                ExpenseModel expenseModel = new ExpenseModel
                {
                    Amount = expenseDto.Amount,
                    Date = DateTime.Now,
                    Description = expenseDto.Description,
                    Title = expenseDto.Title
                };

                _appDbContext.Expenses.Add(expenseModel);
                _appDbContext.SaveChanges();

                result = new Result
                {
                    IsSuccess = true,
                    Message = $"Expense with id {expenseModel.Id} has been successfully created."
                };
            }
            catch (Exception ex)
            {
                // Exception Logging
            }
            return Ok(result);
        }
    }
}
