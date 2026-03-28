using MySql.Data.MySqlClient;

public class PersonRepository
{
    private readonly string? _connectionString;
    
    public PersonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Person RegisterPerson(Person p)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        string sqlCommand = "INSERT INTO people(name, city, age) VALUES (@name, @city, @age);";
        sqlCommand+="SELECT LAST_INSERT_ID();";

        using var command = new MySqlCommand(sqlCommand, connection);

        command.Parameters.AddWithValue("@name", p.Name);
        command.Parameters.AddWithValue("@city", p.City);
        command.Parameters.AddWithValue("@age", p.Age);

        int generatedId = Convert.ToInt32(command.ExecuteScalar()); 

        p.Id = generatedId;

        return p;
    }

    public List<Person> SelectPeople()
    {
        List<Person> people = [];

        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        using var sqlCommand = new MySqlCommand("SELECT * FROM people", connection);

        using var registers = sqlCommand.ExecuteReader();

        while (registers.Read())
        {
            people.Add(new Person
            {
                Id = registers.GetInt32("id"),
                Name = registers.GetString("name"),
                City = registers.GetString("city"),
                Age = registers.GetInt32("age")
            });
        } 

        return people;
    }

    public void UpdatePerson(Person p)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        using var sqlCommand = new MySqlCommand("UPDATE people SET name = @name, city = @city, age = @age WHERE id = @id", connection);

        sqlCommand.Parameters.AddWithValue("@id", p.Id);
        sqlCommand.Parameters.AddWithValue("@name", p.Name);
        sqlCommand.Parameters.AddWithValue("@city", p.City);
        sqlCommand.Parameters.AddWithValue("@age", p.Age);

        sqlCommand.ExecuteNonQuery();
        
    }

    public void DeletePerson(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        using var sqlCommand = new MySqlCommand("DELETE FROM people WHERE id = @id", connection);

        sqlCommand.Parameters.AddWithValue("@id", id);

        sqlCommand.ExecuteNonQuery();
    }

    public bool PersonExists(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        using var sqlCommand = new MySqlCommand("SELECT COUNT(*) FROM people WHERE id = @id", connection);

        sqlCommand.Parameters.AddWithValue("@id", id);

        int counter = Convert.ToInt32(sqlCommand.ExecuteScalar());

        return counter > 0;
    }
}