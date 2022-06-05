using System.Data.SQLite;

namespace SpaceExploration.Data;

public interface IStorageProvider
{
    public SQLiteConnection GetConnection();
}