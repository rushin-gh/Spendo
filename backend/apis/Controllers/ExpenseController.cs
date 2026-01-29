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
        public ActionResult<ExpenseWithIdDTO> GetSingleExpense([FromRoute(Name = "id")] int expId)
        {
            ExpenseWithIdDTO expenseWithId = new ExpenseWithIdDTO();
            try
            {
                var dbExpenseWithId = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId);
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
        public ActionResult<Result<string>> AddExpense([FromBody] ExpenseDTO expenseDto)
        {
            // TODO : Validate input if the necessary fields are null if not send bad request
            var result = new Result<string>();
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

                result = Result<string>.Success(
                    $"Expense with id {expenseModel.Id} has been successfully created.",
                    "No response body"
                );
            }
            catch (Exception ex)
            {
                result = Result<string>.Failure(ex.Message);
                return Problem(ex.Message);
            }
            return Ok(result);
        }

        [HttpPatch("update/{id}")]
        public ActionResult<Result> UpdateExpense([FromRoute(Name = "id")] int expId, [FromBody] ExpenseDTO expenseDto)
        {
            Result result = new Result();
            try
            {
                var expense = _appDbContext.Expenses.FirstOrDefault(exp => exp.Id == expId);

                if (expense == null)
                {
                    result.IsSuccess = false;
                    result.Message = $"Expense with id {expId} does not exists in database.";
                    return BadRequest(result);
                }

                if (expenseDto.Amount.HasValue)
                {
                    expense.Amount = expenseDto.Amount;
                }

                if (expenseDto.Title != null)
                {
                    expense.Title = expenseDto.Title;
                }

                if (expenseDto.Description != null)
                {
                    expense.Description = expenseDto.Description;
                }

                _appDbContext.SaveChanges();

                result.IsSuccess = true;
                result.Message = $"Expense with id {expId} has been successfully updated.";

            }
            catch (Exception ex)
            {
                // Exception logging
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        [HttpPost("delete/{id}")]
        public ActionResult<Result> DeleteExpense([FromRoute(Name = "id")] int expId)
        {
            Result result = new Result();
            try
            {
                if (_appDbContext.Expenses.Any(exp => exp.Id == expId))
                {
                    _appDbContext.Expenses.Where(exp => exp.Id == expId).ExecuteDelete();
                    _appDbContext.SaveChanges();

                    result.IsSuccess = true;
                    result.Message = $"Expense with id {expId} has been successfully deleted.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"Expense with id {expId} does not exists in database.";
                }
            }
            catch (Exception ex)
            {
                // Exception logging
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return Ok(result);
        }
    }
}