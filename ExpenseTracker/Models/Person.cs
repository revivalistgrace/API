namespace ExpenseTracker.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Balance { get; set; }
        public string? age { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }          

    }
}

