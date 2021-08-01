using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any() && !context.Activities.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser{DisplayName= "Bob", UserName="bob",Email="bob@test.com"},
                    new AppUser{DisplayName= "Tom", UserName="tom",Email="tom@test.com"},
                    new AppUser{DisplayName= "Jane", UserName="jane",Email="jane@test.com"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user,"Pa$$w0rd");
                }

 
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Beer Competition",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Drink as much beer you can in 1 hour for 4 dollars",
                        Category = "drinks",
                        City = "London",
                        Venue = "12 Hay Hill Pubs",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Treasure Hunt",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "culture",
                        City = "London",
                        Venue = "Natural History Museum",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Sunburn music festival",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "one of the biggest music fest in goa",
                        Category = "music",
                        City = "Goa",
                        Venue = "Mandrem beach",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Fast and Furious 9",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "In the memory of Paul Walker",
                        Category = "film",
                        City = "New York",
                        Venue = "New York Central",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            }
                        }
                    },
                    
                };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}