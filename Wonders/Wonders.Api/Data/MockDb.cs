using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonders.Api.Models;

namespace Wonders.Api.Data
{
    public class MockDb : IDb
    {
        public List<Wonder> Wonders { get; set; }

        public MockDb()
        {
            Wonders = new List<Wonder>
            {
                new Wonder
            {
                Id = 1,
                Title = "Great pyramid of Giza",
                Description =
                    "The oldest and largest of the three pyramids in the Giza pyramid complex bordering what is now El Giza, Egypt",
                Country = "Egypt"
            },
            new Wonder
            {
                Id = 2,
                Title = "Great wall of China",
                Description =
                    "Series of fortifications made of stone, brick, tamped earth, wood, and other materials",
                Country = "China"
            },
            new Wonder
            {
                Id = 3,
                Title = "Petra",
                Description = "Historical and archaeological city in southern Jordan",
                Country = "Jordan"
            },
            new Wonder
            {
                Id = 4,
                Title = "The Colosseum",
                Description = "An oval amphitheatre in the centre of the city of Rome, Italy",
                Country = "Italy"
            },
            new Wonder
            {
                Id = 5,
                Title = "Chichen Itza",
                Description = "Large pre-Columbian city built by the Maya people of the Terminal Classic period",
                Country = "Mexico"
            },
            new Wonder
            {
                Id = 6,
                Title = "Machu Picchu",
                Description =
                    "15th-century Inca citadel situated on a mountain ridge 2,430 metres (7,970 ft) above sea level",
                Country = "Peru"
            },
            new Wonder
            {
                Id = 7,
                Title = "Taj Mahal",
                Description =
                    "Ivory-white marble mausoleum on the south bank of the Yamuna river in the Indian city of Agra",
                Country = "India"
            },
            new Wonder
            {
                Id = 8,
                Title = "Christ the Redeemer",
                Description = "Art Deco statue of Jesus Christ in Rio de Janeiro, Brazil",
                Country = "Brazil"
            }
            };
        }
    }
}
