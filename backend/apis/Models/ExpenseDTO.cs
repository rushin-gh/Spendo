namespace apis.Models
{
    public class ExpenseDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
