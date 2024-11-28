using DbCore.DbClient;
using System.Data;

namespace DbTests.CustomSteps
{
    public class DbSteps
    {
        private readonly DbClient _dbClient;

        public DbSteps(DbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public (List<int> processId, List<string> description, List<int> requestId, int count) GetListAndCount(string query)
        {
            var processId = new List<int>();
            var description = new List<string>();
            var requestId = new List<int>();

            var dt = _dbClient.SelectQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                processId.Add(Convert.ToInt32(row[0]));
                description.Add(Convert.ToString(row[1])!);
                requestId.Add(Convert.ToInt32(row[2]));
            }
            
            var count = dt.Rows.Count;

            return (processId, description, requestId, count);
        }
    }
}