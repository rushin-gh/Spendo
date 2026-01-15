using System.ComponentModel.DataAnnotations;

namespace apis.Models.Expense.DTOs
{
    public class ExpenseCreateDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
