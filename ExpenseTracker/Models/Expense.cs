namespace ExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
    }
}
