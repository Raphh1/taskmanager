using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManagerRaph.Models;

namespace TaskManagerRaph
{
    public class DataSeeder
    {
        private readonly TaskContext _context;

        public DataSeeder(TaskContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Favorites.Any() && !_context.Tasks.Any())
            {
                // Création de quelques tâches
                var Tasks = new List<Tache>
                {
                    new Tache
                    {
                        task = "Apprendre Rust",
                        date = DateTime.Now.AddDays(1),
                        categorie = "Formation",
                        description = "Suivre un cours pour améliorer mes compétences en Rust"
                    },
                    new Tache
                    {
                        task = "Réunion d'équipe",
                        date = DateTime.Now.AddDays(2),
                        categorie = "Travail",
                        description = "Réunion d'équipe pour discuter des projets en cours"
                    },
                    new Tache
                    {
                        task = "Acheter des courses",
                        date = DateTime.Now.AddDays(3),
                        categorie = "Personnel",
                        description = "Acheter des légumes et des fruits pour la semaine"
                    }
                };

                // Ajout de favoris
                var favoris1 = new Favoris
                {
                    TaskId = "Tasks Perso",
                    Tasks = new List<Tache> { Tasks[2] } // Associer la tâche "Acheter des courses"
                };

                var favoris2 = new Favoris
                {
                    TaskId = "Tasks Pro",
                    Tasks = new List<Tache> { Tasks[0], Tasks[1] } // Associer les tâches "Apprendre Rust" et "Réunion d'équipe"
                };

                // Ajout des tâches et des favoris dans la base de données
                _context.Tasks.AddRange(Tasks);
                _context.Favorites.AddRange(new List<Favoris> { favoris1, favoris2 });

                // Enregistrement des changements dans la base de données
                _context.SaveChanges();
                Console.WriteLine("Les tâches et les favoris ont été ajoutés dans la base de données.");
            }
            else
            {
                Console.WriteLine("Les tâches et les favoris existent déjà dans la base de données.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Création du contexte de la base de données
            var optionsBuilder = new DbContextOptionsBuilder<TaskContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

            using (var context = new TaskContext(optionsBuilder.Options))
            {
                var seeder = new DataSeeder(context);
                seeder.SeedData();
            }
        }
    }
}
