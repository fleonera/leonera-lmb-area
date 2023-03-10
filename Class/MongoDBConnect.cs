using MongoDB.Driver;
using AwsDotnetCsharp.Models.Domain;
using System.Linq;

namespace AwsDotnetCsharp.Connection
{
    public class MongoDBConnection
    {
        private const string DBName = "sql_migration";
        private const string EmpresaCollectionName = "area";
        public  string ConnectionString = null;
        private IMongoDatabase db = null;
        public MongoDBConnection()
        {                    
        }

        public void OpenConnection(){
            var client = new MongoClient(ConnectionString);
            db = client.GetDatabase(DBName);
        }
        
        public IMongoCollection<Area> getCollection()
        {            
            if(db == null) return null;
            return db.GetCollection<Area>(EmpresaCollectionName);
        }
       
    }
}