namespace ExpenseTracker.Models.DTO
{
    public class IncomeDto
    {       
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
    public class AddIncomeDto : IncomeDto
    {
        public int PersonId { get; set; }
    }

    public class UpdateIncomeDto : IncomeDto
    { 
    }
}
