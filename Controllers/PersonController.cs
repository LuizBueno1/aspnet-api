using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase  
{

    private readonly PersonRepository _personRepository;

    public PersonController(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpPost]
    public IActionResult Register([FromBody] Person p)
    {
        if(string.IsNullOrWhiteSpace(p.Name))
        {
            return BadRequest(new {message = "Name is mandatory!"});
        }
        else if(string.IsNullOrWhiteSpace(p.City))
        {
            return BadRequest(new{message = "City is mandatory!"});
        }
        else if(p.Age < 0 || p.Age > 120)
        {
            return BadRequest(new{message = "The age must be between 0 and 120!"});
        }
 
            var registeredPerson = _personRepository.RegisterPerson(p);
            return Created(string.Empty , registeredPerson);
    }

    [HttpGet]
    public List<Person> Select()
    {
        return _personRepository.SelectPeople();
    }

    [HttpPut("{id}")]
    public Person Update(int id, [FromBody] Person p)
    {
        p.Id = id;

        _personRepository.UpdatePerson(p);

        return p;
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _personRepository.DeletePerson(id);
    }

}