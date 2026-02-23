public class PersonRepository
{
    private readonly string? _connectionString;
    
    public PersonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
}