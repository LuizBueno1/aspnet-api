using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase  
{
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