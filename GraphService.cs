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
        var person = new Person {Name = "George Chen"};
        await _client
            .Cypher
            .Create("(p:Person $newUser)")
            .WithParam("newUser", person)
            .ExecuteWithoutResultsAsync();
    }
}

public class Person
{
    [JsonProperty(PropertyName = "name")] 
    public string Name { get; set; }
}
