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

                new Models.Writer{ ID=1, Name="Christopher Markus", Place_DB=new DateTime(1973,1,16), Photograph="0.jpg", MiniBio="Christopher Markus is a writer and producer, known for Capitão América: O Primeiro Vingador (2011), As Crónicas de Nárnia: O Leão, a Feiticeira e o Guarda-Roupa (2005) and Capitão América: Guerra Civil (2016). He has been married to Claire Saunders since October 22, 2011." },
                new Models.Writer{ ID=2, Name="Stephen McFeely", Place_DB= new DateTime(1973,12,21), Photograph="1.jpg", MiniBio="Stephen McFeely is a writer and producer, known for Capitão América: O Primeiro Vingador (2011), As Crónicas de Nárnia: O Leão, a Feiticeira e o Guarda-Roupa (2005) and Capitão América: Guerra Civil (2016)." },

                new Models.Writer{ ID=3, Place_DB= new DateTime(1964,5,23), Name="Joss Whedon", Photograph="2.jpg", MiniBio="Joss Whedon is the middle of five brothers - his younger brothers are Jed Whedon and Zack Whedon. Both his father, Tom Whedon and his grandfather, John Whedon were successful television writers. Joss' mother, Lee Stearns, was a history teacher and she also wrote novels as Lee Whedon. Whedon was raised in New York and was educated at Riverdale" },
                new Models.Writer{ ID=4, Place_DB= new DateTime(1968,9,1), Name="Zak Penn", Photograph="3.jpg", MiniBio="Zak Penn's career began as a screenwriter when he sold his first script, Last Action Hero, at the age of twenty-three. Since then, Penn has become known for his work on numerous films based on Marvel comics, including X-Men 2 and X-Men: The Last Stand, The Incredible Hulk, and The Avengers." },

                new Models.Writer{ ID=5, Place_DB= new DateTime(1968,4,9), Name="Jay Chandrasekhar", Photograph="4.jpg", MiniBio="Jay Chandrasekhar was born on April 9, 1968 in Chicago, Illinois, USA as Jayanth Jambulingam Chandrasekhar. He is a director and actor, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004). He has been married to Susan Clarke since September 18, 2005. They have three children." },
                new Models.Writer{ ID=6, Place_DB= new DateTime(1968,5,25), Name="Kevin Heffernan", Photograph="5.jpg", MiniBio="Kevin Heffernan was born on May 25, 1968 in West Haven, Connecticut, USA. He is an actor and writer, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004)." },

                new Models.Writer{ ID=7, Place_DB= new DateTime(1965,10,12), Name="Jeff Nathanson", Photograph="6.jpg", MiniBio="Jeff Nathanson was born on October 12, 1965 in Los Angeles, California, USA. He is known for his work on Speed 2: Perigo a Bordo (1997), Apanha-me Se Puderes (2002) and Piratas das Caraíbas: Homens Mortos Não Contam Histórias (2017)." },

                new Models.Writer{ ID=8, Place_DB= new DateTime(1969,2,12), Name="Darren Aronofsky", Photograph="7.jpg", MiniBio="Darren Aronofsky was born February 12, 1969, in Brooklyn, New York. Growing up, Darren was always artistic: he loved classic movies and, as a teenager, he even spent time doing graffiti art. After high school, Darren went to Harvard University to study film (both live-action and animation). He won several film awards after completing his senior ..." },

            };
            writer.ForEach(aa => context.Writers.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var diretores = new List<Models.Director>
            {
                new Models.Director{ ID=1, Name="Anthony Russo", Photograph="0.jpg", Place_BD= new DateTime(1970,2,3), MiniBio="Anthony Russo was born on February 3, 1970 in Cleveland, Ohio, USA as Anthony J. Russo. He is a producer and director, known for Capitão América: O Soldado do Inverno (2014), Capitão América: Guerra Civil (2016) and Vingadores: Guerra do Infinito (2018)." },
                new Models.Director{ ID=2, Name="Joe Russo", Photograph="1.jpg", Place_BD= new DateTime(1971,7,8), MiniBio="Joe Russo was born on July 8, 1971 in Cleveland, Ohio, USA as Joseph Vincent Russo. He is a producer and director, known for Capitão América: O Soldado do Inverno (2014), Capitão América: Guerra Civil (2016) and Vingadores: Guerra do Infinito (2018)." },

                new Models.Director{ ID=3, Place_BD= new DateTime(1964,5,23), Name="Joss Whedon", Photograph="2.jpg", MiniBio="Joss Whedon is the middle of five brothers - his younger brothers are Jed Whedon and Zack Whedon. Both his father, Tom Whedon and his grandfather, John Whedon were successful television writers. Joss' mother, Lee Stearns, was a history teacher and she also wrote novels as Lee Whedon. Whedon was raised in New York and was educated at Riverdale" },

                new Models.Director{ ID=4, Place_BD= new DateTime(1968,4,9), Name="Jay Chandrasekhar", Photograph="3.jpg", MiniBio="Jay Chandrasekhar was born on April 9, 1968 in Chicago, Illinois, USA as Jayanth Jambulingam Chandrasekhar. He is a director and actor, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004). He has been married to Susan Clarke since September 18, 2005. They have three children." },

                new Models.Director{ ID=5, Place_BD= new DateTime(1972,5,30), Name="Joachim Rønning", Photograph="4.jpg", MiniBio="Growing up in the eighties in Sandefjord, a small town south of Oslo, Norway, Joachim Rønning spent his free time making short films with his dad's 30 pounds home video camera - one of the few burdens for being the first of the video generation. In 1992 he attended Stockholm Film School in Sweden - graduating in 1994. Later that year Rønning served." },
                new Models.Director{ ID=6, Place_BD= new DateTime(1971,6,17), Name="Espen Sandberg", Photograph="5.jpg", MiniBio="Espen Sandberg was born on June 17, 1971 in Sandefjord, Norway. He is a director and producer, known for Kon Tiki - A Viagem Impossível (2012), Piratas das Caraíbas: Homens Mortos Não Contam Histórias (2017) and Bandidas (2006). " },

                new Models.Director{ ID=7, Place_BD= new DateTime(1969,2,12), Name="Darren Aronofsky", Photograph="6.jpg", MiniBio="Darren Aronofsky was born February 12, 1969, in Brooklyn, New York. Growing up, Darren was always artistic: he loved classic movies and, as a teenager, he even spent time doing graffiti art. After high school, Darren went to Harvard University to study film (both live-action and animation). He won several film awards after completing his senior ..." },


            };
            diretores.ForEach(aa => context.Directores.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var actors = new List<Models.Actors>
            {
                new Models.Actors{ ID=1, Name="Robert Downey Jr.", Photograph="0.jpg", BD= new DateTime(1965,4,4), Minibio="Robert Downey Jr. has evolved into one of the most respected actors in Hollywood. With an amazing list of credits to his name, he has managed to stay new and fresh even after over four decades in the business." },
                new Models.Actors{ ID=2, Name="Chris Hemsworth", Photograph="1.jpg", BD= new DateTime(1983,8,11), Minibio="Chris Hemsworth was born in Melbourne, Australia, to Leonie (van Os), a teacher of English, and Craig Hemsworth, a social-services counselor. " },
                new Models.Actors{ ID=3, Name="Mark Ruffalo", Photograph="2.jpg", BD= new DateTime(1967,11,22), Minibio="Mark Ruffalo was born in Kenosha, Wisconsin, to Marie Rose (Hebert), a stylist and hairdresser, and Frank Lawrence Ruffalo, a construction painter." },

                new Models.Actors{ ID=4, BD= new DateTime(1968,4,9), Name="Jay Chandrasekhar", Photograph="3.jpg", Minibio="Jay Chandrasekhar was born on April 9, 1968 in Chicago, Illinois, USA as Jayanth Jambulingam Chandrasekhar. He is a director and actor, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004). He has been married to Susan Clarke since September 18, 2005. They have three children." },
                new Models.Actors{ ID=5, BD= new DateTime(1968,5,25), Name="Kevin Heffernan", Photograph="4.jpg", Minibio="Kevin Heffernan was born on May 25, 1968 in West Haven, Connecticut, USA. He is an actor and writer, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004)." },
                new Models.Actors{ ID=6, BD= new DateTime(1968,11,13), Name="Steve Lemme", Photograph="5.jpg", Minibio="Steve Lemme was born on November 13, 1968 in New York City, New York, USA as Stephen Carlos Lemme. He is an actor and writer, known for Super Troopers (2001), Beerfest (2006) and Clube Dread - Morre a Rir, Meu (2004). He has been married to Tiffany Chadderton since April 15, 2010. He was previously married to Sandra." },

                new Models.Actors{ ID=7, BD= new DateTime(1963,6,9), Name="Johnny Depp ", Photograph="6.jpg", Minibio="Johnny Depp is perhaps one of the most versatile actors of his day and age in Hollywood. He was born John Christopher Depp II in Owensboro, Kentucky, on June 9, 1963, to Betty Sue (Wells), who worked as a waitress, and John Christopher Depp, a civil engineer. Depp was raised in Florida. He dropped out of school when he was 15, and fronted a series " },
                new Models.Actors{ ID=8, BD= new DateTime(1951,6,6), Name="Geoffrey Rush", Photograph="7.jpg", Minibio="Geoffrey Roy Rush was born on July 6, 1951, in Toowoomba, Queensland, Australia, to Merle (Bischof), a department store sales assistant, and Roy Baden Rush, an accountant for the Royal Australian Air Force. His mother was of German descent and his father had English, Irish, and Scottish ancestry. He was raised in Brisbane, Queensland, after his ..." },
                new Models.Actors{ ID=9, BD= new DateTime(1969,3,1), Name="Javier Bardem", Photograph="8.jpg", Minibio="Javier Bardem is the youngest member of a family of actors that has been making films since the early days of Spanish cinema. He was born in Las Palmas de Gran Canaria, Spain, to actress Pilar Bardem (María del Pilar Bardem Muñoz) and businessman José Carlos Encinas Doussinague. His maternal grandparents were actors Rafael Bardem and Matilde Muñoz ..." },

                new Models.Actors{ ID=10, BD= new DateTime(1968,6,4), Name="Sean Gullette", Photograph="9.jpg", Minibio="Gullette co-wrote and played the lead role in the award-winning Pi, directed by longtime collaborator Darren Aronofsky. He has since played principal and supporting roles in some twenty films. His first feature film as writer-director, Traitors, made its world premiere at the 2013 Venice Days festival, where it won a Special Mention, and screened ..." },
                new Models.Actors{ ID=11, BD= new DateTime(1939,11,26), Name="Mark Margolis", Photograph="10.jpg", Minibio="He was born in Philadelphia, Pennsylvania, in 1939 and attended Temple University briefly before moving to New York where he studied drama with Stella Adler and at Actors Studio." },
                new Models.Actors{ ID=12, BD= new DateTime(1968,9,26), Name="Ben Shenkman", Photograph="11.jpg", Minibio="Ben Shenkman was born on September 26, 1968 in New York City, New York, USA. He is an actor, known for Pi(1998), Blue Valentine - Só Tu e Eu(2010) and Anjos na América(2003).He has been married to Lauren Greilsheimer since September 17, 2005." },

                new Models.Actors{ ID=13, BD= new DateTime(1981,6,13), Name="Chris Evans", Photograph="12.jpg", Minibio="Christopher Robert Evans began his acting career in typical fashion: performing in school productions and community theatre. He was born in Boston, Massachusetts, the son of Lisa (Capuano), who worked at the Concord Youth Theatre, and G. Robert Evans III, a dentist. His uncle is congressman Mike Capuano. Chris's father is of half German and half ..." },

            };
            actors.ForEach(aa => context.Actors.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();

            var Characters = new List<Models.Characters>
            {
                new Models.Characters{ ID=1, Name="Tony Stark, Iron Man" , Photograph="0.jpg", actor= actors[0] },
                new Models.Characters{ ID=2, Name="Thor" , Photograph="1.jpg", actor= actors[1] },
                new Models.Characters{ ID=3, Name="Bruce Banner, Hulk" , Photograph="2.jpg", actor= actors[2] },

                new Models.Characters{ ID=4, Name="Mac" , Photograph="3.jpg", actor= actors[5] },
                new Models.Characters{ ID=5, Name="Thorny" , Photograph="4.jpg", actor= actors[3] },
                new Models.Characters{ ID=6, Name="Farva" , Photograph="5.jpg", actor= actors[4] },

                new Models.Characters{ ID=7, Name="Captain Jack Sparrow" , Photograph="6.jpg", actor= actors[6] },
                new Models.Characters{ ID=8, Name="Captain Salazar" , Photograph="7.jpg", actor= actors[7] },
                new Models.Characters{ ID=9, Name="Captain Hector Barbossa" , Photograph="8.jpg", actor= actors[8] },

                new Models.Characters{ ID=10, Name="Maximillian Cohen" , Photograph="9.jpg", actor= actors[9] },
                new Models.Characters{ ID=11, Name="Sol Robeson" , Photograph="10.jpg", actor= actors[10] },
                new Models.Characters{ ID=12, Name="Lenny Meyer" , Photograph="11.jpg", actor= actors[11] },

                new Models.Characters{ ID=13, Name="Steve Rogers, Captain America" , Photograph="11.jpg", actor= actors[12] },




            };
            Characters.ForEach(aa => context.Charas.AddOrUpdate(a => a.Name, aa));
            context.SaveChanges();


            var Movie = new List<Models.Movie>
            {
                new Models.Movie{ ID=1, dataDePub= new DateTime(2018,4,25), Name="Avengers: Infinity War", CharactersList= new List<Models.Characters>{ Characters[0],Characters[1],Characters[2]  },
                                  DirectorList = new List<Models.Director>{ diretores[0],diretores[1] },Photograph="0.jpg", Trailer="https://www.youtube.com/embed/6ZfuNTqbHE8", Rating=3 , WriterList= new List<Models.Writer>{ writer[0],writer[1] },sinopse="The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe."
                                },
                new Models.Movie{ ID=2, Name="The Avengers", dataDePub= new DateTime(2012,4,25), CharactersList= new List<Models.Characters>{ Characters[0],Characters[1],Characters[2]},
                                  DirectorList = new List<Models.Director>{ diretores[2] },WriterList= new List<Models.Writer>{ writer[2], writer[3] },
                                   Photograph="1.jpg", Trailer="https://www.youtube.com/embed/eOrNdBpGMv8", Rating=3 ,sinopse="Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity."
                                },

                new Models.Movie{ ID=3, Name="Super Troopers 2", dataDePub= new DateTime(2018,4,20), CharactersList= new List<Models.Characters>{ Characters[3],Characters[4],Characters[5] },
                                  DirectorList = new List<Models.Director>{diretores[3]},
                                   Photograph="2.jpg", Trailer="https://www.youtube.com/embed/eEed-o8fVpM", Rating=3 , WriterList= new List<Models.Writer>{ writer[4],writer[5]},sinopse="When a border dispute arises between the U.S. and Canada, the Super Troopers are tasked with establishing a Highway Patrol station in the disputed area."
                                },
                new Models.Movie{ ID=4, Name="Dead Men Tell No Tales", dataDePub=new DateTime(2017,5,25), CharactersList= new List<Models.Characters>{Characters[6],Characters[7],Characters[8]},
                                  DirectorList = new List<Models.Director>{ diretores[4],diretores[5] },
                                   Photograph="3.jpg", Trailer="https://ww w.youtube.com/embed/Hgeu5rhoxxY", Rating=3 , WriterList= new List<Models.Writer>{ writer[6] },sinopse="Captain Jack Sparrow searches for the trident of Poseidon while being pursued by an undead sea captain and his crew."
                                },
                new Models.Movie{ ID=5, Name="PI", dataDePub= new DateTime(1998,6,10), CharactersList= new List<Models.Characters>{Characters[9],Characters[10],Characters[11] },
                                  DirectorList = new List<Models.Director>{diretores[6]},
                                   Photograph="4.jpg",
                                   Trailer ="https://www.youtube.com/embed/jo18VIoR2xU",
                                   Rating =3 ,
                                   WriterList = new List<Models.Writer>{ writer[7]},
                                   sinopse ="A paranoid mathematician searches for a key number that will unlock the universal patterns found in nature."
                                },
                
                new Models.Movie{ ID=6, Name="Captain America: Civil War",dataDePub= new DateTime(2016,4,28),CharactersList= new List<Models.Characters>{ Characters[12],Characters[0] },
                                  DirectorList = new List<Models.Director>{ diretores[0], diretores[1] },
                                   Photograph="5.jpg", Trailer="https://www.youtube.com/embed/dKrVegVI0Us", Rating=3 , WriterList= new List<Models.Writer>{writer[0],writer[1] },sinopse="Political involvement in the Avengers' activities causes a rift between Captain America and Iron Man."
                                }
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
