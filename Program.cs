// See https://aka.ms/new-console-template for more information

using neo4j_client;

var graphService = new GraphService("http://localhost:7474", "neo4j", "Passw0rd");
// await graphService.GetPersons();
// await graphService.CreatePerson();
// await graphService.CreatePersonAndActedIn();
await graphService.CreatePersonsInBatch();
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
