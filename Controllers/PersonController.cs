using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase  
{
    [HttpGet]
    public string MyFirstRoute()
    {
        return "Hello World!";
    }
}