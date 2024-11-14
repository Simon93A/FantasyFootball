using fantacyfotball_api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace fantacyfotball_api
{
    public class DbSeeder
    {
        public static async void Seed(IServiceProvider service)
        {

            using var scope = service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FantasyFootballDbContext>();


            if (!context.Players.Any())
            {
                var players = new List<Player>()
                {

                    new Player { _id = 1, Name = "Zlatan Ibrahimovic", Rating = 8, Price = 75 },
                    new Player { _id = 2, Name = "Lionel Messi", Rating = 9, Price = 90 },
                    new Player { _id = 3, Name = "Cristiano Ronaldo", Rating = 9, Price = 85 },
                    new Player { _id = 4, Name = "Neymar Jr", Rating = 8, Price = 80 },
                    new Player { _id = 5, Name = "Kevin De Bruyne", Rating = 8, Price = 78 },
                    new Player { _id = 6, Name = "Kylian Mbappé", Rating = 9, Price = 95 },
                    new Player { _id = 7, Name = "Mohamed Salah", Rating = 8, Price = 85 },
                    new Player { _id = 8, Name = "Virgil van Dijk", Rating = 8, Price = 70 },
                    new Player { _id = 9, Name = "Harry Kane", Rating = 8, Price = 80 },
                    new Player { _id = 10, Name = "Sadio Mané", Rating = 8, Price = 75 },
                    new Player { _id = 11, Name = "Gareth Bale", Rating = 7, Price = 60 },
                    new Player { _id = 12, Name = "Luka Modric", Rating = 8, Price = 75 },
                    new Player { _id = 13, Name = "Karim Benzema", Rating = 8, Price = 80 },
                    new Player { _id = 14, Name = "Eden Hazard", Rating = 7, Price = 65 },
                    new Player { _id = 15, Name = "Raheem Sterling", Rating = 8, Price = 70 },
                    new Player { _id = 16, Name = "Paul Pogba", Rating = 7, Price = 65 },
                    new Player { _id = 17, Name = "Antoine Griezmann", Rating = 8, Price = 75 },
                    new Player { _id = 18, Name = "Gianluigi Donnarumma", Rating = 8, Price = 70 },
                    new Player { _id = 19, Name = "Son Heung-min", Rating = 8, Price = 78 },
                    new Player { _id = 20, Name = "Romelu Lukaku", Rating = 8, Price = 80 },
                    new Player { _id = 21, Name = "Marcus Berg", Rating = 6, Price = 35 },
new Player { _id = 22, Name = "Jimmy Durmaz", Rating = 5, Price = 30 },
new Player { _id = 23, Name = "Sebastian Larsson", Rating = 6, Price = 32 },
new Player { _id = 24, Name = "Andreas Granqvist", Rating = 5, Price = 28 },
new Player { _id = 25, Name = "Jordan Larsson", Rating = 6, Price = 40 },
new Player { _id = 26, Name = "John Guidetti", Rating = 5, Price = 30 },
new Player { _id = 27, Name = "Albin Ekdal", Rating = 6, Price = 38 },
new Player { _id = 28, Name = "Emil Forsberg", Rating = 7, Price = 50 },
new Player { _id = 29, Name = "Victor Nilsson Lindelöf", Rating = 7, Price = 55 },
new Player { _id = 30, Name = "Robin Quaison", Rating = 6, Price = 36 }

                };
                var client = new MongoClient("mongodb://host.docker.internal:27017");
                var database = client.GetDatabase("FantasyFootballDB");
                var playersCollection = database.GetCollection<Player>("Players");
                await playersCollection.InsertManyAsync(players);

            }
            
        }
    }
}

