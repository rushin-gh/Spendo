using System.ComponentModel.DataAnnotations;

namespace apis.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required, MaxLength(200)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
