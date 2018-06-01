using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq;

namespace MongoDbConsoleApp
{
    class Program
    {
        class DbContext
        {
            private IMongoDatabase db;

            public DbContext()
            {
                var mongoCredential = MongoCredential.CreateMongoCRCredential("PeopleStore", "jajac", "jajac");
                MongoClientSettings setting = new MongoClientSettings();
                setting.Server = new MongoServerAddress("localhost", 27017);
                setting.Credential = mongoCredential;

                // preferred way so far as the MongoClientSettings were not working.
                const string connectionString = "mongodb://jajac:jajac@localhost:27017/PeopleStore";

                // if you dont specify any setting then local host is selected by default.
                MongoClient client = new MongoClient(connectionString);
                db = client.GetDatabase("PeopleStore");
            }

            public IMongoCollection<People> Peoples
            {
                get
                {
                    return db.GetCollection<People>("People");
                }
            }
        }


        static void Main(string[] args)
        {
            DbContext context = new DbContext();
            ////People person = new People { Name = "me", Age = 1 };
            ////context.Peoples.InsertOne(person);

            var peoples = context.Peoples.AsQueryable().Where(p => p.Age > 10).ToList();

            // if you want to update an item then you dont have to fetch it change it and send it to the server. You can do that in single query.
            //context.Peoples.UpdateOne() find out how.

        }

        class People {

            // When you do this, there wont be a property called UniqueId in database. 
            //[BsonId]
            //public int UniqueId { get; set; }

            public ObjectId Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }

            /// <summary>
            /// this will be stored as null if there is no value. you can make mongo ignore it if null by specifying attribute BsonIgnoreIfNull or BsonIgnoreIfDefault
            /// </summary>            
            public int? count { get; set; }
        }
    }
}
