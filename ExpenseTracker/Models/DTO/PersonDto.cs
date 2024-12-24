namespace ExpenseTracker.Models.DTO
{
    public class PersonDto

    { }
    namespace ExpenseTracker.Models.DTO
    {
        public class PersonDto
        {
            public string? Name { get; set; }
            public decimal Balance { get; set; }
            public string? age { get; set; }
          

        }
        public class AddPersonDto : PersonDto
        {
            
        }

        public class UpdatePersonDto : PersonDto
        {
        }

    }
}