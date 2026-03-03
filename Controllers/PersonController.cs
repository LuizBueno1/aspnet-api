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
    public Person Register([FromBody] Person p)
    {
        return _personRepository.RegisterPerson(p);
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