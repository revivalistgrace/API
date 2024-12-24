using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.DTO.ExpenseTracker.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PersonController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllPerson")]
        public IActionResult GetAllPerson()
        {
            var allPerson = dbContext.Person.ToList();
            return Ok(allPerson);
        }


        [HttpPost]
        [Route("CreatePerson")]
        public IActionResult AddPerson(AddPersonDto addPersonDto)
        {
            var Person = new Person()
            {
                Name = addPersonDto.Name,
                Balance = addPersonDto.Balance,
                age= addPersonDto.age,
            };
            dbContext.Person.Add(Person);
            dbContext.SaveChanges();

            return Ok(Person);
        }

        [HttpPut]
        [Route("UpdatePerson/{id:int}")]
        public IActionResult UpdatePerson(int id, UpdatePersonDto updatePersonDto)
        {
            var person = dbContext.Person.Find(id);
            if (person is null)
            {
                return NotFound();
            }

            person.Name= updatePersonDto.Name;
            person.Balance = updatePersonDto.Balance;
            person.age = updatePersonDto.age;
           

            dbContext.SaveChanges();

            return Ok(person);
        }

        [HttpDelete]
        [Route("DeletePerson")]
        public IActionResult DeletePerson(int id)
        {
            var Person = dbContext.Person.Find(id);
            if (Person is null)
            {
                return NotFound();
            }
            dbContext.Person.Remove(Person);
            dbContext.SaveChanges();


            return Ok(Person);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPersonById(int id)
        {
            var Person =dbContext.Person.Find(id);
            if (Person is null)
            {
                return NotFound();
            }

            UpdateBalance(id);
            return Ok(Person);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public void UpdateBalance(int personId)
        {
            var person = dbContext.Person.Find(personId);

            if(person != null)
            {
                var totalIncome = dbContext.Income.Where(i => i.PersonId == personId).Sum(i => i.Amount);
                var totalExpense = dbContext.Expense.Where(i => i.PersonId == personId).Sum(i => i.Amount);

                person.Balance = totalIncome - totalExpense;
                dbContext.SaveChanges();
            }         

           
        }
        

    }
}
