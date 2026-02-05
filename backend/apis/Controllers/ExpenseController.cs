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
        public ActionResult<DataResult<List<ExpenseWithIdDTO>>> GetExpenses()
        {
            DataResult<List<ExpenseWithIdDTO>> result = new(true, "No Response", []);
            {
                var dbExpenses = _appDbContext.Expenses;

                var expenses = dbExpenses
                                .Select(
                                    exp => new ExpenseWithIdDTO
                                    {
                                        Id = exp.Id,
                                        Title = exp.Title,
                                        Amount = exp.Amount,
                                        Description = exp.Description
                                    }
                                ).ToList();

                result = DataResult<List<ExpenseWithIdDTO>>.Success("Expenses fetched successfully", expenses);
            }
            return Ok(result);
        }

        [HttpGet("get/{id}")]
        public ActionResult<DataResult<ExpenseWithIdDTO>> GetSingleExpense([FromRoute(Name = "id")] int expId)
        {
            DataResult<ExpenseWithIdDTO> result = new(true, "No Response", new ExpenseWithIdDTO());
            {
                var dbExpenseWithId = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId)
                                        ?? throw new KeyNotFoundException($"Expense with id {expId} doesn't exists");

                ExpenseWithIdDTO expenseWithId = new ExpenseWithIdDTO
                {
                    Id = dbExpenseWithId.Id,
                    Amount = dbExpenseWithId.Amount,
                    Title = dbExpenseWithId.Title,
                    Description = dbExpenseWithId.Description
                };

                result = DataResult<ExpenseWithIdDTO>.Success("Expense fetched successfully", expenseWithId);
            }
            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<Result> AddExpense([FromBody] ExpenseDTO expenseDto)
        {
            Result result = new Result();
            {
                if (expenseDto == null)
                    throw new ArgumentException("Null expense not allowed");

                if (expenseDto.Title == null || expenseDto.Title == string.Empty)
                    throw new ArgumentException("Title is required field");

                if (!expenseDto.Amount.HasValue)
                    throw new ArgumentException("Amount is required field");

                if (expenseDto.Amount <= 0)
                    throw new ArgumentException("Non positive amount is not allowed");

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

            return Created("ExpenseCreated", result);
        }

        [HttpPatch("update/{id}")]
        public ActionResult<DataResult<ExpenseModel>> UpdateExpense([FromRoute(Name = "id")] int expId, [FromBody] ExpenseDTO expenseDto)
        {
            DataResult<ExpenseModel> result = new(true, "No Response", new ExpenseModel());
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



                result = DataResult<ExpenseModel>.Success(
                    $"Expense with id {expId} has been successfully updated", 
                    _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId)
                );
            }
            return Ok(result);
        }

        [HttpPost("delete/{id}")]
        public ActionResult<DataResult<ExpenseModel>> DeleteExpense([FromRoute(Name = "id")] int expId)
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