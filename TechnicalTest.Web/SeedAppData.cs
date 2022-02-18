using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.DAL;
using TechnicalTest.DAL.Entities;

namespace TechnicalTest.Web
{
    public static class SeedAppData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<TechTestContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

           
           
            if (!context.Patients.Any())
            {
                context.Patients.AddRange(

                    new Patient
                    {
                        Fullname = "John Doe",
                        CardNo = "TTH 001",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    },

                    new Patient
                    {
                        Fullname = "Mary Doe",
                        CardNo = "TTH 002",
                        Created = DateTime.Now,
                        Updated = DateTime.Now

                    },

                    new Patient
                    {
                        Fullname = "Harry Lance",
                        CardNo = "TTH 003",
                        Created = DateTime.Now,
                        Updated = DateTime.Now

                    }

                );
            }

            if (!context.Visits.Any())
            {
                context.Visits.AddRange(

                    new Visit
                    {
                        PatientId = 1,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-9),
                        SignOut = DateTime.Now.AddMinutes(9)
                    },

                    new Visit
                    {
                        PatientId = 2,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-8),
                        SignOut = DateTime.Now.AddMinutes(8)
                    },

                    new Visit
                    {
                        PatientId = 3,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-7),
                        SignOut = DateTime.Now.AddMinutes(7)
                    },

                    new Visit
                    {
                        PatientId = 1,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-6),
                        SignOut = DateTime.Now.AddMinutes(6)
                    },

                    new Visit
                    {
                        PatientId = 2,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-5),
                        SignOut = DateTime.Now.AddMinutes(5)
                    },
                    new Visit
                    {
                        PatientId = 3,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-4),
                        SignOut = DateTime.Now.AddMinutes(4)
                    },

                    new Visit
                    {
                        PatientId = 1,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-3),
                        SignOut = DateTime.Now.AddMinutes(3)
                    },

                    new Visit
                    {
                        PatientId = 2,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-2),
                        SignOut = DateTime.Now.AddMinutes(2)
                    },

                    new Visit
                    {
                        PatientId = 3,
                        Reason = "Some Reason",
                        CameToSee = "Somebody",
                        VisitedAt = DateTime.Now.AddMinutes(-1),
                        SignOut = DateTime.Now.AddMinutes(1)
                    }

                );
            }

            context.SaveChanges();

        }

    }
}