using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Candidate.Models
{
    public class ProfileData
    {
        //Singleton
        private static ProfileData instance = new ProfileData();
        public static ProfileData Instance
        {
            get
            {
                return instance;
            }
        }

        //Memory Access
        public ObservableCollection<Profile> Profiles { get; set; }
        public ObservableCollection<ProfileQuestion> ProfileQuestions { get; set; }
        public ObservableCollection<Question> Questions { get; set; }

        private ProfileData()
        {
            

            //Create Tables in sqlite database
            CreateDatabase();

            //Debug -- Populate from memory
            //Populate to memory
            HardCodeToMemory();
            //Populate from memory
            MemoryToSqlite();            
        }      


        public float[][] GetTrainData()
        {
            //List<Profile> profiles = Profile.GetAll();
            int profileCount = Profiles.Count();
            int questionCount = Questions.Count();
            //using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    profiles = connection.Table<Profile>().ToList();
            //    Profiles = new ObservableCollection<Profile>(profiles as List<Profile>);
            //}

            //1 sample by candidate
            float[][] ret = new float[profileCount][];
            for(int i = 0; i< profileCount; i++)
            {
                //number of features equal to number of questions
                //+1 for the class attributed to this train sample
                ret[i] = new float[questionCount + 1];
                for(int j = 0; j< questionCount; j++)
                {
                    //get the choice of the i candidate for the j question (+1 as Primary key starts from 1)
                    ret[i][j] = ProfileQuestions.SingleOrDefault(pq => pq.Profile.Id == i+1 && pq.Question.Id == j+1).Choice;
                    //ProfileQuestion pq = ProfileQuestion.GetProfileQuestion(i + 1, j + 1);
                    //ret[i][j] = pq.Choice;
                }
                //last column is the attributed class
                ret[i][questionCount] = i;
            }            

            return ret;
        }

        public void CreateDatabase()
        {

            SQLiteAsyncConnection connection = new SQLiteAsyncConnection(App.DatabaseLocation);
            
                connection.DropTableAsync<Profile>();
                connection.DropTableAsync<Question>();
                connection.DropTableAsync<Assertive>();
                connection.DropTableAsync<ProfileQuestion>();

                connection.CreateTableAsync<Profile>();
                connection.CreateTableAsync<Assertive>();
                connection.CreateTableAsync<Question>();                
                connection.CreateTableAsync<ProfileQuestion>();
            connection.GetConnection().Close();
            connection.GetConnection().Dispose();
            connection = null;
        }

        void MemoryToSqlite()
        {                        
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {               
                connection.InsertAllWithChildren(ProfileQuestions, recursive: true);             
            }
        }

        public async void SqliteToMemory()
        {
            ProfileQuestions = new ObservableCollection<ProfileQuestion>(ProfileQuestion.GetAllWithChildren());
            Profiles = new ObservableCollection<Profile>(Profile.GetAll());
            Questions = new ObservableCollection<Question>(Question.GetAllWithChildren());
        }

        void HardCodeToMemory()
        {
            Profiles = new ObservableCollection<Profile>()
            {
                new Profile()
                {
                    Id = 1,
                    Name = "Jair Bolsonaro",
                    Bio = "Político",
                    ImageFile = "bolsonaro_round.png",
                    Score = 0
                },
                new Profile()
                {
                    Id = 2,
                    Name = "Marina Silva",
                    Bio = "Político",
                    ImageFile = "marina_round.png",
                    Score = 0
                },
                new Profile()
                {
                    Id = 3,
                    Name = "Geraldo Alckmin",
                    Bio = "Político",
                    ImageFile = "alckmin_round.png",
                    Score = 0
                }
            };


            Questions = new ObservableCollection<Question>
            {
               new Question
                {
                   Id = 1,
                    Title = "Pergunta sobre um Tema A, relevante para as eleições e futuro do Brasil.",
                    Info = "Informações sobre o Tema A",
                    Assertives = new List<Assertive>()
                    {
                        new Assertive
                        {
                            Id = 1,                           
                            Score = 0,
                            Text = "Nada"
                        },
                        new Assertive
                        {
                            Id = 2,                            
                            Score = 1,
                            Text = "Pouco"
                        },
                        new Assertive
                        {
                            Id = 3,                           
                            Score = 2,
                            Text = "Normal"
                        },
                        new Assertive
                        {
                            Id = 4,                            
                            Score = 3,
                            Text = "Muito"
                        },
                        new Assertive
                        {
                            Id = 5,                            
                            Score = 4,
                            Text = "Tudo"
                        }
                    }
                },
                new Question
                {
                    Id = 2,
                    Title = "Pergunta sobre um Tema B, relevante para as eleições e futuro do Brasil.",
                    Info = "Informações sobre o Tema B",
                     Assertives = new List<Assertive>()
                    {
                         new Assertive
                        {
                             Id = 6,                             
                             Score = 0,
                            Text = "Nada"
                        },
                        new Assertive
                        {
                            Id = 7,                            
                            Score = 1,
                            Text = "Pouco"
                        },
                        new Assertive
                        {
                            Id = 8,                            
                            Score = 2,
                            Text = "Normal"
                        },
                        new Assertive
                        {
                            Id = 9,                            
                            Score = 3,
                            Text = "Muito"
                        },
                        new Assertive
                        {
                            Id = 10,                            
                            Score = 4,
                            Text = "Tudo"
                        }
                    }
                }
            };

            ProfileQuestions = new ObservableCollection<ProfileQuestion>()
            {
                new ProfileQuestion()
                {
                    Id = 1,
                    Profile = Profiles.SingleOrDefault(p => p.Id == 1),
                    Question = Questions.SingleOrDefault(q => q.Id == 1),
                    Opinion = "O que o Candidato A pensa sobre o tema A",
                    Choice = 0
                },

                new ProfileQuestion()
                {
                    Id = 2,
                    Profile = Profiles.SingleOrDefault(p => p.Id == 1),
                    Question = Questions.SingleOrDefault(q => q.Id == 2),
                    Opinion = "O que o Candidato A pensa sobre o tema B",
                    Choice = 0
                },

                new ProfileQuestion()
                {
                    Id = 3,
                    Profile = Profiles.SingleOrDefault(p => p.Id == 2),
                    Question = Questions.SingleOrDefault(q => q.Id == 1),
                    Opinion = "O que o Candidato B pensa sobre o tema A",
                    Choice = 2
                },

                new ProfileQuestion()
                {
                    Id = 4,
                    Profile = Profiles.SingleOrDefault(p => p.Id == 2),
                    Question = Questions.SingleOrDefault(q => q.Id == 2),
                    Opinion = "O que o Candidato B pensa sobre o tema B",
                    Choice = 2
                },

                new ProfileQuestion()
                {
                    Id = 5,
                    Profile = Profiles.SingleOrDefault(p => p.Id == 3),
                    Question = Questions.SingleOrDefault(q => q.Id == 1),
                    Opinion = "O que o Candidato C pensa sobre o tema A",
                    Choice = 4
                },

                new ProfileQuestion()
                {
                    Id = 6,
                   Profile = Profiles.SingleOrDefault(p => p.Id == 3),
                    Question = Questions.SingleOrDefault(q => q.Id == 2),
                    Opinion = "O que o Candidato C pensa sobre o tema B",
                    Choice = 4
                }
            };
        }
    }
}
