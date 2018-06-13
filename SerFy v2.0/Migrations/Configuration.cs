namespace SerFy_v2._0.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SerFy_v2._0.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SerFy_v2._0.Models.ApplicationDbContext context)
        {
            var writer = new List<Models.Writer>
            {

                new Models.Writer{ ID=1, Name="Christopher Markus", Place_DB=new DateTime(1973,1,16), Photograph="~/Multimedia/Writers/0.jpg", MiniBio="Christopher Markus is a writer and producer, known for Capitão América: O Primeiro Vingador (2011), As Crónicas de Nárnia: O Leão, a Feiticeira e o Guarda-Roupa (2005) and Capitão América: Guerra Civil (2016). He has been married to Claire Saunders since October 22, 2011." },
                new Models.Writer{ ID=2, Name="Stephen McFeely", Place_DB= new DateTime(1973,12,21), Photograph="~/Multimedia/Writers/1.jpg", MiniBio="Stephen McFeely is a writer and producer, known for Capitão América: O Primeiro Vingador (2011), As Crónicas de Nárnia: O Leão, a Feiticeira e o Guarda-Roupa (2005) and Capitão América: Guerra Civil (2016)." },

                new Models.Writer{ ID=3, Place_DB= new DateTime(1964,5,23), Name="Joss Whedon", Photograph="~/Multimedia/Writers/2.jpg", MiniBio="Joss Whedon is the middle of five brothers - his younger brothers are Jed Whedon and Zack Whedon. Both his father, Tom Whedon and his grandfather, John Whedon were successful television writers. Joss' mother, Lee Stearns, was a history teacher and she also wrote novels as Lee Whedon. Whedon was raised in New York and was educated at Riverdale" },
                new Models.Writer{ ID=4, Place_DB= new DateTime(1968,9,1), Name="Zak Penn", Photograph="~/Multimedia/Writers/3.jpg", MiniBio="Zak Penn's career began as a screenwriter when he sold his first script, Last Action Hero, at the age of twenty-three. Since then, Penn has become known for his work on numerous films based on Marvel comics, including X-Men 2 and X-Men: The Last Stand, The Incredible Hulk, and The Avengers." },

            };
            writer.ForEach(aa => context.Writers.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var diretores = new List<Models.Director>
            {
                new Models.Director{ ID=1, Name="Anthony Russo", Photograph="0.jpg", Place_BD= new DateTime(1970,2,3), MiniBio="Anthony Russo was born on February 3, 1970 in Cleveland, Ohio, USA as Anthony J. Russo. He is a producer and director, known for Capitão América: O Soldado do Inverno (2014), Capitão América: Guerra Civil (2016) and Vingadores: Guerra do Infinito (2018)." },
                new Models.Director{ ID=2, Name="Joe Russo", Photograph="1.jpg", Place_BD= new DateTime(1971,7,8), MiniBio="Joe Russo was born on July 8, 1971 in Cleveland, Ohio, USA as Joseph Vincent Russo. He is a producer and director, known for Capitão América: O Soldado do Inverno (2014), Capitão América: Guerra Civil (2016) and Vingadores: Guerra do Infinito (2018)." },

                new Models.Director{ ID=3, Place_BD= new DateTime(1964,5,23), Name="Joss Whedon", Photograph="2.jpg", MiniBio="Joss Whedon is the middle of five brothers - his younger brothers are Jed Whedon and Zack Whedon. Both his father, Tom Whedon and his grandfather, John Whedon were successful television writers. Joss' mother, Lee Stearns, was a history teacher and she also wrote novels as Lee Whedon. Whedon was raised in New York and was educated at Riverdale" },

            };
            diretores.ForEach(aa => context.Directores.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var actors = new List<Models.Actors>
            {
                new Models.Actors{ ID=1, Name="Robert Downey Jr.", Photograph="0.jpg", BD= new DateTime(1965,4,4), Minibio="Robert Downey Jr. has evolved into one of the most respected actors in Hollywood. With an amazing list of credits to his name, he has managed to stay new and fresh even after over four decades in the business." },
                new Models.Actors{ ID=2, Name="Chris Hemsworth", Photograph="1.jpg", BD= new DateTime(1983,8,11), Minibio="Chris Hemsworth was born in Melbourne, Australia, to Leonie (van Os), a teacher of English, and Craig Hemsworth, a social-services counselor. " },
                new Models.Actors{ ID=3, Name="Mark Ruffalo", Photograph="3.jpg", BD= new DateTime(1967,11,22), Minibio="Mark Ruffalo was born in Kenosha, Wisconsin, to Marie Rose (Hebert), a stylist and hairdresser, and Frank Lawrence Ruffalo, a construction painter." },

            };
            actors.ForEach(aa => context.Actors.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var Characters = new List<Models.Characters>
            {
                new Models.Characters{ ID=1, Name="Tony Stark, Iron Man" , Photograph="0.jpg", actor= actors[0] },
                new Models.Characters{ ID=2, Name="Thor" , Photograph="1.jpg", actor= actors[1] },
                new Models.Characters{ ID=3, Name="Bruce Banner, Hulk" , Photograph="2.jpg", actor= actors[2] },

            };
            Characters.ForEach(aa => context.Charas.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();


            var Movie = new List<Models.Movie>
            {
                new Models.Movie{ ID=1, dataDePub= new DateTime(2018,4,25), Name="Avengers: Infinity War", CharactersList= new List<Models.Characters>{ Characters[0],Characters[1],Characters[2]  },
                                  DirectorList = new List<Models.Director>{ diretores[0],diretores[1] },Photograph="0.jpg", Trailer="https://www.youtube.com/watch?v=6ZfuNTqbHE8", Rating=3 , WriterList= new List<Models.Writer>{ writer[0],writer[1] },sinopse="The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe."
                                },
                new Models.Movie{ ID=2, Name="The Avengers", dataDePub= new DateTime(2012,4,25), CharactersList= new List<Models.Characters>{ Characters[0],Characters[1],Characters[2]},
                                  DirectorList = new List<Models.Director>{ diretores[2] },WriterList= new List<Models.Writer>{ writer[2], writer[3] },
                                   Photograph="1.jpg", Trailer="https://www.youtube.com/watch?v=eOrNdBpGMv8", Rating=3 ,sinopse="Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity."
                                },
                //---------------------------------------------------------------------------------------------------------------------
                //new Models.Movie{ ID=3, Name="Super Troopers 2", CharactersList= new List<Models.Characters>{ },
                //                  DirectorList = new List<Models.Director>{ },
                //                   Photograph="2.jpg", Trailer="https://www.youtube.com/watch?v=eEed-o8fVpM", Rating=3 , WriterList= new List<Models.Writer>{ },sinopse="When a border dispute arises between the U.S. and Canada, the Super Troopers are tasked with establishing a Highway Patrol station in the disputed area."
                //                },
                //new Models.Movie{ ID=4, Name="Pirates of the Caribbean: Dead Men Tell No Tales", CharactersList= new List<Models.Characters>{ },
                //                  DirectorList = new List<Models.Director>{ },
                //                   Photograph="3.jpg", Trailer="https://www.youtube.com/watch?v=Hgeu5rhoxxY", Rating=3 , WriterList= new List<Models.Writer>{ },sinopse="Captain Jack Sparrow searches for the trident of Poseidon while being pursued by an undead sea captain and his crew."
                //                },
                //new Models.Movie{ ID=5, Name="PI", CharactersList= new List<Models.Characters>{ },
                //                  DirectorList = new List<Models.Director>{ },
                //                   Photograph="4.jpg", Trailer="https://www.youtube.com/watch?v=jo18VIoR2xU", Rating=3 , WriterList= new List<Models.Writer>{ },sinopse="A paranoid mathematician searches for a key number that will unlock the universal patterns found in nature."
                //                },
                //new Models.Movie{ ID=6, Name="Captain America: Civil War", CharactersList= new List<Models.Characters>{ },
                //                  DirectorList = new List<Models.Director>{ },
                //                   Photograph="5.jpg", Trailer="https://www.youtube.com/watch?v=jo18VIoR2xU", Rating=3 , WriterList= new List<Models.Writer>{ },sinopse="Political involvement in the Avengers' activities causes a rift between Captain America and Iron Man."
                //                }
            };
            Movie.ForEach(aa => context.Movies.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }



    }
}
