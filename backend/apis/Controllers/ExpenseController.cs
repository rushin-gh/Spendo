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
        public ActionResult<ExpenseWithIdDTO> GetSingleExpense([FromRoute(Name = "id")]int expId)
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
            try
            {
                ExpenseModel expenseModel = new ExpenseModel
                {
                    Id = expenseWithId.Id,
                    Amount = expenseWithId.Amount,
                    Description = expenseWithId.Description,
                    Title = expenseWithId.Title
                };

                if (_appDbContext.Expenses.Any(exp => exp.Id == expenseModel.Id))
                {
                    _appDbContext.Expenses.Update(expenseModel);
                    _appDbContext.SaveChanges();

                    result.IsSuccess = true;
                    result.Message = $"Expense with id {expenseModel.Id} has been successfully updated.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"Expense with id {expenseModel.Id} does not exists in database.";
                }
            }
            catch(Exception ex)
            {
                // Exception logging
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        [HttpPost("delete/{id}")]
        public ActionResult<Result> DeleteExpense([FromRoute(Name = "id")]int expId)
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