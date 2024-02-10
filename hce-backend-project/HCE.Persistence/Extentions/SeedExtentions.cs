using HCE.Domain.Constants;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Extentions
{
    public static class SeedExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = new Guid("bceb091d-6b80-4f37-ae10-8388a4172e8d"),
                Name = SystemRoles.SuperAdmin,
                CreatedDate = new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local),
                CreatedBy = "System",
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f"),
                Name = "Super Admin",
                Email = "Super.admin@absher.com",
                UserName = "super.admin",
                Gender = Gender.Male,
                Password = "98ca5703dbd694f23e853efb870c6919c5947f1c8add29c96a11bf3c13a89c07", //super.admin
                CreatedDate = new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local),
                CreatedBy = "System",
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                Id = new Guid("d867ac37-6e53-4880-89b6-fb867eb13c89"),
                RoleId = new Guid("bceb091d-6b80-4f37-ae10-8388a4172e8d"),
                UserId = new Guid("2a4e1c24-aff9-41c2-b046-0f25613a3c1f"),
                CreatedDate = new DateTime(2021, 10, 27, 0, 43, 49, 480, DateTimeKind.Local),
                CreatedBy = "System",
            });

            modelBuilder.Entity<Modules>().HasData(new Modules()
            {
                ModuleId = 1,//new Guid("2a4e1c33-aff9-41c2-b046-0f25643a3c1f"),
                ModuleCode = "Post",
                ModuleName = "Post",
                CreatedDate = new DateTime(2021, 11, 9, 0, 43, 49, 480, DateTimeKind.Local),
                CreatedBy = "2a4e1c24-aff9-41c2-b046-0f25613a3c1f",
            });

        }

    }
}
