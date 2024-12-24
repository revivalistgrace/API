namespace ExpenseTracker.Models.DTO
{
    public class ExpenseDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }

    public class AddExpenseDto : ExpenseDto
    {
        public int PersonId { get; set; }
    }

    public class UpdateExpenseDto : ExpenseDto
    {
    }


}
