using Microsoft.EntityFrameworkCore;
using StudTeacher.Data;
using StudTeacher.Models;

namespace StudTeacher.StudTeachApp.Tests.Data
{
    public static class AppDbContextMockHelper
    {
        public static AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Accounts.AddRange(
                new Account { Id = 1, Name = "John", Dob = DateTime.Now, Designation = "Student",Email = "john@x.com",PasswordHash = "stud123"},
                new Account { Id = 2, Name = "Alice",Dob = DateTime.Now, Designation = "Teacher",Email = "alice@x.com", PasswordHash = "teach123"}
            );
            context.SaveChanges();
            return context;
        }
    }
}
