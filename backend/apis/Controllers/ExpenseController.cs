using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apis.Models;
using System.Reflection.PortableExecutable;
using apis.Database;
using apis.Models.Expense;
using apis.Models.Expense.DTOs;
using Microsoft.EntityFrameworkCore;

namespace apis.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet("get")]
        public ActionResult<List<ExpenseWithIdDTO>> GetExpenses()
        {
            List<ExpenseWithIdDTO> expensesWithIds = [];
            {
                var dbExpenses = _appDbContext.Expenses;
                
                if (dbExpenses == null)
                    throw new Exception("Error while loading expenses");

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
            return Ok(expensesWithIds);
        }

        [HttpGet("get/{id}")]
        public ActionResult<ExpenseWithIdDTO> GetSingleExpense([FromRoute(Name = "id")] int expId)
        {
            ExpenseWithIdDTO expenseWithId = new ExpenseWithIdDTO();
            {
                var dbExpenseWithId = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId)
                                        ?? throw new KeyNotFoundException($"Expense with id {expId} doesn't exists");

                expenseWithId = new ExpenseWithIdDTO
                {
                    Id = dbExpenseWithId.Id,
                    Amount = dbExpenseWithId.Amount,
                    Title = dbExpenseWithId.Title,
                    Description = dbExpenseWithId.Description
                };
            }
            return Ok(expenseWithId);
        }

        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromBody] ExpenseDTO expenseDto)
        {
            // ExpenseDTO validation
            if (expenseDto == null)
                throw new ArgumentException("Null expense not allowed");

            if (expenseDto.Title == null || expenseDto.Title == string.Empty)
                throw new ArgumentException("Title is required field");

            if (!expenseDto.Amount.HasValue)
                throw new ArgumentException("Amount is required field");

            if (expenseDto.Amount <= 0)
                throw new ArgumentException("Non positive amount is not allowed");

            var result = new Result();
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

                result = Result.Success(
                    $"Expense has been successfully created with id {expenseModel.Id}."
                );
            }
            return Ok(result);
        }

        [HttpPatch("update/{id}")]
        public ActionResult<Result> UpdateExpense([FromRoute(Name = "id")] int expId, [FromBody] ExpenseDTO expenseDto)
        {
            Result result = new();
            {
                var expense = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId)
                                ?? throw new KeyNotFoundException($"Expense with id {expId} does not exists in database.");

                if (expenseDto.Title != null)
                    expense.Title = expenseDto.Title;

                if (expenseDto.Description != null)
                    expense.Description = expenseDto.Description;

                if (expenseDto.Amount.HasValue)
                    expense.Amount = expenseDto.Amount;

                _appDbContext.SaveChanges();

                result.IsSuccess = true;
                result.Message = $"Expense with id {expId} has been successfully updated.";
            }

            return Ok(result);
        }

        // TODO - Work on response of Delete and Update as well
        [HttpPost("delete/{id}")]
        public ActionResult<DataResult<ExpenseDTO>> DeleteExpense([FromRoute(Name = "id")] int expId)
        {
            DataResult<ExpenseModel> result = new();
            {
                if (!_appDbContext.Expenses.Any(exp => exp.Id == expId))
                    throw new KeyNotFoundException($"Expense with id {expId} does not exists in database.");

                result.Data = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId);

                _appDbContext.Expenses.Where(exp => exp.Id == expId).ExecuteDelete();
                _appDbContext.SaveChanges();

                result.IsSuccess = true;
                result.Message = $"Attached expense is successfully deleted";
            }
            return Ok(result);
        }
    }
}