using Microsoft.AspNetCore.Identity;

namespace HomeWork.Data {
    public class SeedDataIdentity {

        private const string adminStudentsEmail = "adminStudents@gmail.com";
        private const string adminStudentsPassword = "secretStudents";

        private const string adminFacultyEmail = "adminFaculty@gmail.com";
        private const string adminFacultyPassword = "secretFaculty";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app) {
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user1 = await userManager.FindByEmailAsync(adminStudentsEmail);
            IdentityUser user2 = await userManager.FindByEmailAsync(adminFacultyEmail);



            // Roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleName1 = "StudentsAdmin";
            string roleName2 = "FacultiesAdmin";

            if (!await roleManager.RoleExistsAsync(roleName1)) {
                await roleManager.CreateAsync(new IdentityRole(roleName1));
            }

            if (!await roleManager.RoleExistsAsync(roleName2)) {
                await roleManager.CreateAsync(new IdentityRole(roleName2));
            }


            if (user1 == null) {
                user1 = new IdentityUser { UserName = adminStudentsEmail, Email = adminStudentsEmail };
                await userManager.CreateAsync(user1, adminStudentsPassword);
                await userManager.AddToRoleAsync(user1, roleName1);
            }

            if (user2 == null) {
                user2 = new IdentityUser { UserName = adminFacultyEmail, Email = adminFacultyEmail };
                await userManager.CreateAsync(user2, adminFacultyPassword);
                await userManager.AddToRoleAsync(user2, roleName2);
            }              
        
        }
    }
}
