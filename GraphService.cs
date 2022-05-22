using Neo4j.Driver;

namespace neo4j_client;

public class GraphService
{
    private IDriver _driver;

    public GraphService(string uri, string user, string password)
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }

    public async Task GetPersons()
    {
        using (var session = _driver.AsyncSession())
        {
            var result = await session.ReadTransactionAsync(async tx =>
            {
                var persons = await tx.RunAsync("match(p:Person) return p.name as name");
                return (await persons.ToListAsync()).As<List<IRecord>>();
            });
            foreach (var person in result)
            {
                Console.WriteLine(person["name"]);
            }
        }
        
        
    }
}
