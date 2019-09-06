using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Data
{
    public static class DbSeeder
    {

        public static void SeedDb(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            SeedTable1(context);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            
        }

        private static void SeedTable1(ApplicationDbContext context)
        {
            
        }
    }
}
