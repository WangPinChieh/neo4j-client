// See https://aka.ms/new-console-template for more information

using neo4j_client;

var graphService = new GraphService("bolt://localhost:7687", "neo4j", "Passw0rd");
await graphService.GetPersons();
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
