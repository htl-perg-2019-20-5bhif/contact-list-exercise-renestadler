using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactList.Controllers
{
    [ApiController]
    [Route("/contacts")]
    public class ContactsController : ControllerBase
    {

        private static int ID = 0;
        private static readonly List<Person> People = new List<Person>();

        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return Ok(People);
        }

        [HttpPost]
        public virtual IActionResult AddPerson([FromBody]Person body)
        {
            IEnumerable<Person> deleteQuery = from person in People where person.id == body.id select person;
            if (deleteQuery.Count() != 0)
            {
                return BadRequest("ID already choosen");

            }
            People.Add(body);
            return Created("", body);
        }

        [HttpDelete]
        [Route("/contacts/{personId}")]
        public virtual IActionResult DeletePerson([FromRoute]int personId)
        {
            if (personId < 0)
            {
                return BadRequest("Invalid ID supplied");
            }

            IEnumerable<Person> deleteQuery = from person in People where person.id == personId select person;
            if (deleteQuery.Count() == 0)
            {
                return NotFound("Person not found");
            }
            People.Remove(deleteQuery.Single());
            return NoContent();
        }

        [HttpGet]
        [Route("/contacts/findByName")]
        public virtual IActionResult FindPersonByName([FromQuery]string nameFilter)
        {
            if (nameFilter == null)
            {
                return BadRequest("Invalid or missing name");
            }
            IEnumerable<Person> deleteQuery = from person in People where (person.firstName != null && person.firstName.Contains(nameFilter)) || (person.lastName != null && person.lastName.Contains(nameFilter)) select person;
            return Ok(deleteQuery);
        }
    }
}
