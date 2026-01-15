namespace apis.Models.Expense.DTOs
{
    public class ExpenseResponseDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
