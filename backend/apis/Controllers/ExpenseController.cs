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
        public ActionResult<List<ExpenseWithIdDTO>> GetExpenses()
        {
            List<ExpenseWithIdDTO> expensesWithIds = new List<ExpenseWithIdDTO>();
            try
            {
                var dbExpenses = _appDbContext.Expenses;
                expensesWithIds
                    .AddRange(dbExpenses.Select(
                        exp => new ExpenseWithIdDTO
                        {
                            Id = exp.Id,
                            Title = exp.Title,
                            Amount = exp.Amount,
                            Description = exp.Description
                        }
                    )
                );
            }
            catch (Exception ex)
            {
                // Exception logging
            }
            return Ok(expensesWithIds);
        }

        [HttpGet("get/{id}")]
        public ActionResult<ExpenseWithIdDTO> GetSingleExpenses(int id)
        {
            ExpenseWithIdDTO expenseWithId = new ExpenseWithIdDTO();
            try
            {
                var dbExpenseWithId = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == id);
                expenseWithId = new ExpenseWithIdDTO
                {
                    Id = dbExpenseWithId.Id,
                    Amount = dbExpenseWithId.Amount,
                    Title = dbExpenseWithId.Title,
                    Description = dbExpenseWithId.Description
                };
            }
            catch (Exception ex)
            {
                // Exception logging
            }
            return Ok(expenseWithId);
        }

        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromBody] ExpenseDTO expenseDto)
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

        [HttpPost("update")]
        public ActionResult<Result> UpdateExpense(ExpenseWithIdDTO expenseWithId)
        {
            Result result = new Result();


            return Ok(result);
        }
    }
}
