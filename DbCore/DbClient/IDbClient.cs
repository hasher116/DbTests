namespace DbCore.DbClient
{
    public interface IDbClient : IDisposable
    {
        public bool Create();
    }
}
