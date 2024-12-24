﻿namespace ExpenseTracker.Models
{
    public class AddIncome
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int PersonId { get; set; }
    }
}
