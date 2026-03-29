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
    public IActionResult Update(int id, [FromBody] Person p)
    {
        if (!_personRepository.PersonExists(id))
        {
            return NotFound(new {message = "Person does not exist!"});
        }
        else if(string.IsNullOrWhiteSpace(p.Name))
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
        
        p.Id = id;

        _personRepository.UpdatePerson(p);

        return Ok(p);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if(_personRepository.PersonExists(id))
        {
           _personRepository.DeletePerson(id);
           return Ok(new {message = "Person successfully deleted."}); 
        }
        else
        {
            return NotFound(new {message = "Person does not exist!"});
        }
    }

}