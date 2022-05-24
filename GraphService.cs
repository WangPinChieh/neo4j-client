using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;

namespace neo4j_client;

public class GraphService
{
    private GraphClient _client;

    public GraphService(string uri, string user, string password)
    {
        _client = new GraphClient(new Uri(uri), user, password);
    }

    public async Task GetPersons()
    {
        await _client.ConnectAsync();
        var result = await _client
            .Cypher
            .Match("(p:Person)")
            .Return(p => p.As<Person>())
            .ResultsAsync;
    }

    public async Task CreatePerson()
    {
        await _client.ConnectAsync();
        var person = new Person {Name = "George Chen"};
        await _client
            .Cypher
            .Create("(p:Person $newUser)")
            .WithParam("newUser", person)
            .ExecuteWithoutResultsAsync();
    }    
    public async Task CreatePersonAndActedIn()
    {
        await _client.ConnectAsync();
        var person = new Person {Name = "Joey Chen"};
        await _client
            .Cypher
            .Match("(movie:Movie)")
            .Where((Movie movie) => movie.Title == "The Matrix Revolutions")
            .Create("(person:Person $newUser)")
            .WithParam("newUser", person)
            .Create("(person)-[:ACTED_IN]->(movie)")
            .ExecuteWithoutResultsAsync();
    }

    public async Task CreatePersonsInBatch()
    {
        await _client.ConnectAsync();
        var persons = new List<Person>
        {
            new Person
            {
                Name = "ABC"
            },
            new Person
            {
                Name = "DEF"
            },
        };
        await _client
            .Cypher
            .Unwind(persons, "map")
            .Create("(person:Person)")
            .Set("person = map")
            .ExecuteWithoutResultsAsync();
    }
}

public class Movie
{
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }
    [JsonProperty(PropertyName = "released")]
    public DateTime Released { get; set; }
    [JsonProperty(PropertyName = "tagline")]
    public string Tagline { get; set; }

}

public class Person
{
    [JsonProperty(PropertyName = "name")] 
    public string Name { get; set; }
}
