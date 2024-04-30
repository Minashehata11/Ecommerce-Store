using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository
{
    public  class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName="Mina",
                    Email="Mina@gmail.com",
                    UserName="Mina",
                    Address=new Address
                    {
                        FirstName="Mina",
                        LastName="Shehata",
                        City="Alex",
                        State="Borg",
                        ZipCode=545,
                        Street="Asan"

                    }

                };
                await userManager.CreateAsync(user,"Mina_1452002");
            }
        }
    }
}
