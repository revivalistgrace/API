 using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public IncomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllIncome")]
        public IActionResult GetAllIncome()
        {
            var allIncome = dbContext.Income.ToList();

            var totalAmount = allIncome.Sum(x => x.Amount);

            return Ok(allIncome);
        }


        [HttpPost]
        [Route("CreateIncome")]
        public IActionResult AddIncome(AddIncomeDto addIncomeDto)

        {
            var Income = new Income()
            {
                Date = addIncomeDto.Date,
                Amount = addIncomeDto.Amount,
                Category = addIncomeDto.Category,
                Description = addIncomeDto.Description,
                PersonId = addIncomeDto.PersonId,
            };
            dbContext.Income.Add(Income);
            dbContext.SaveChanges();

            UpdateBalance(addIncomeDto.PersonId);

            return Ok("Success");
        }

        [HttpGet]
        [Route("GetByIncomeId")]
        public IActionResult GetIncomeById(int id)
        {
            var Income = dbContext.Income.Find(id);
            if (Income is null)
            {
                return NotFound();
            }
            return Ok(Income);
        }

        [HttpPut]
        [Route("UpdateIncome/{id:int}")]
        public IActionResult UpdateIncome(int id, UpdateIncome updateIncome)
        {
            var Income = dbContext.Income.Find(id);
            if (Income is null)
            {
                return NotFound();
            }

            Income.Date = updateIncome.Date;
            Income.Amount = updateIncome.Amount;
            Income.Category = updateIncome.Category;
            Income.Description = updateIncome.Description;

            dbContext.SaveChanges();

            return Ok("Success");
        }

        [HttpDelete]
        [Route("DeleteIncome")]
        public IActionResult DeleteIncome(int id)
        {
            var Income = dbContext.Income.Find(id);
            if (Income is null)
            {
                return NotFound();
            }
            dbContext.Income.Remove(Income);
            dbContext.SaveChanges();


            return Ok("Success");
        }

        [HttpGet]
        [Route("GetByDate")]
        public IActionResult GetIncomeByMonthAndYear([FromQuery] int month, [FromQuery] int year)
        {
            var filteredIncome = dbContext.Income.Where(income => income.Date.Month == month && income.Date.Year == year).ToList();

            if (!filteredIncome.Any())
            {
                return NotFound($"No Income found for {month}/{year}.");
            }

            return Ok(filteredIncome);

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
        [Route("GetIncomeByPersonId")]
        public IActionResult GetIncomeByPerson(int id)
        {
            var incomes = dbContext.Income.Where(e => e.PersonId == id).ToList();
            return Ok(incomes);
        }
    }
}
