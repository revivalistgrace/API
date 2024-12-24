using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ExpenseController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllExpense")]
        public IActionResult GetAllExpense()
        {
            var allExpense = dbContext.Expense.ToList();
            return Ok(allExpense);
        }

        [HttpPost]
        [Route("CreateExpense")]
        public IActionResult AddExpense(AddExpenseDto addExpenseDto)

        {
            var Expense = new Expense()
            {
                Date = addExpenseDto.Date,
                Amount = addExpenseDto.Amount,
                Category = addExpenseDto.Category,
                Description = addExpenseDto.Description,
                PersonId = addExpenseDto.PersonId,
            };
            dbContext.Expense.Add(Expense);
            dbContext.SaveChanges();

            UpdateBalance(addExpenseDto.PersonId);

            return Ok("Success");
        }

        [HttpGet]
        [Route("GetByExpenseId")]
        public IActionResult GetExpenseById(int id)
        {
            var Expense = dbContext.Expense.Find(id);
            
            if (Expense is null)
            {
                return NotFound();
            }
            return Ok(Expense);
        }

        [HttpPut]
        [Route("UpdateExpense/{id:int}")]         
        public IActionResult UpdateExpense(int id,UpdateExpenseDto updateExpense)
        {
            var Expense = dbContext.Expense.Find(id);
            if (Expense is null)
            {
                return NotFound("Not Found");
            }

            Expense.Date = updateExpense.Date;
            Expense.Amount = updateExpense.Amount;
            Expense.Category = updateExpense.Category;
            Expense.Description = updateExpense.Description;

            dbContext.SaveChanges();

            return Ok("Success");
        }

        [HttpDelete]
        [Route("DeleteExpense/{id:int}")]
        public IActionResult DeleteExpense(int id)
        {
            var expense = dbContext.Expense.Find(id);
            if(expense is null)
            {
                return NotFound();
            }
            dbContext.Expense.Remove(expense);
            dbContext.SaveChanges();


            return Ok();
        }

        [HttpGet]
        [Route("GetByDate")]
        public IActionResult GetExpensesByMonthAndYear([FromQuery] int month, [FromQuery] int year)
        {
            var filteredExpenses = dbContext.Expense.Where(expense => expense.Date.Month == month && expense.Date.Year == year).ToList();

            if (!filteredExpenses.Any())
            {
                return NotFound($"No expenses found for {month}/{year}.");
            }

            return Ok(filteredExpenses);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public void UpdateBalance(int personId)
        {
            var person = dbContext.Person.Find(personId);

            if (person != null)
            {
                var totalIncome = dbContext.Income.Where(i => i.PersonId == personId).Sum(i => i.Amount);
                var totalExpense = dbContext.Expense.Where(i => i.PersonId == personId).Sum(i => i.Amount);

                person.Balance = totalIncome - totalExpense;
                dbContext.SaveChanges();
            }


        }

        [HttpGet]
        [Route("GetExpenseByPersonId")]
        public IActionResult GetExpenseByPerson(int id)
        {
            var expenses = dbContext.Expense.Where(e => e.PersonId == id).ToList();
            return Ok(expenses);
        }
    }

}


