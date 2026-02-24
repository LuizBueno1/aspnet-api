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

    [HttpGet]
    public string MyFirstRoute()
    {
        return "My first route";
    }

    [HttpPost]
    public Person ManipulatePersonModel([FromBody] Person p)
    {
        return p;
    }
}