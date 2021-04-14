using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(IServiceProvider services)
        {
            using (var context = new ApplicationDbContext
                (services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var courses = new List<Course>();
                var modules = new List<Module>();

                var fake = new Faker("sv");

                for (int i = 0; i< 10;i++)
                {
                    var course = new Course
                    {
                        Title = fake.Company.CatchPhrase(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(0, 10)),
                        Modules = new List<Module>()
                    };
                    courses.Add(course);
                }

                foreach (var course in courses)
                {
                    int rnd = fake.Random.Int(1, 4);
                    for (int i = 0; i < rnd; i++)
                    {
                        var module = new Module
                        {
                            Title = fake.Company.Bs(),
                            CourseId = course.Id,
                            StartDate = course.StartDate.AddDays(fake.Random.Int(0,20))
                        };
                        course.Modules.Add(module);
                        modules.Add(module);
                    }
                }

                await context.AddRangeAsync(courses);
                await context.AddRangeAsync(modules);

                await context.SaveChangesAsync();
            }
        }
    }
}
