using DbCore.Configuration;
using DbCore.DbClient;
using DbTests.CustomSteps;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace DbTests.Tests
{
    public class ExampleTests
    {
        private IConfiguration? configuration;
        private DbClient? dbClient;
        private DbSteps? dbSteps;

        [SetUp]
        public void Setup()
        {
            configuration = ConfigFile.GetConfiguration();
            dbClient = new DbClient(configuration, "PostgresSettings");
            dbClient!.Create();
            dbSteps = new DbSteps(dbClient);
        }

        [TearDown]
        public void TearDown()
        {
            dbClient!.Dispose();
        }

        [Test]
        public void CheckFirstSelect()
        {
            var query = @"select * from processes p 
                join requests r on r.process_id = p.id 
                where r.process_id = 2";

            var (processId, description, requestId, count) = dbSteps!.GetListAndCount(query);
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{processId[i]}\t{description[i]}\t\t{requestId[i]}");
            }
            count.Should().Be(3);
        }
    }
}