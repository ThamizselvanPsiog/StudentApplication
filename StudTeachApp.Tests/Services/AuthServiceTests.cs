using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudTeacher.DTO;
using StudTeacher.Models;
using StudTeacher.Services;
using StudTeacher.StudTeachApp.Tests.Data;

namespace StuTeacher.StudTeachApp.Tests.Services
{
    public class AuthServiceTests
    {
        private IConfiguration GetFakeConfig()
        {
            var dict = new Dictionary<string, string?>
            {
                {"Jwt:Key", "MySuperSecretJwtKey1234567890123456"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"}
            };
            return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
        }

        [Fact]
        public async Task Register_NewUser_ReturnsToken()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var config = GetFakeConfig();
            var service = new AuthService(context, config);

            var dto = new RegisterDto
            {
                Name = "Test",
                Email = "test@x.com",
                Password = "123",
                Designation = "Student",
                Dob = DateTime.Now
            };

            var result = await service.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.Equal("test@x.com", result.Email);
        }

        [Fact]
        public async Task Register_DuplicateEmail_ReturnsNull()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var config = GetFakeConfig();
            var service = new AuthService(context, config);

            var dto = new RegisterDto
            {
                Name = "John",
                Email = "john@x.com",
                Password = "123",
                Designation = "Student",
                Dob = DateTime.Now
            };

            var result = await service.RegisterAsync(dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ValidUser_ReturnsToken()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var config = GetFakeConfig();
            var service = new AuthService(context, config);

            var newUser = new Account
            {
                Name = "LogUser",
                Email = "log@x.com",
                Designation = "Teacher",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
            };
            context.Accounts.Add(newUser);
            context.SaveChanges();

            var dto = new LoginDto { Email = "log@x.com", Password = "pass" };
            var result = await service.LoginAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("log@x.com", result.Email);
        }

        [Fact]
        public async Task Login_InvalidPassword_ReturnsNull()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var config = GetFakeConfig();
            var service = new AuthService(context, config);

            var newUser = new Account
            {
                Name = "LogUser",
                Email = "wrong@x.com",
                Designation = "Teacher",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
            };
            context.Accounts.Add(newUser);
            context.SaveChanges();

            var dto = new LoginDto { Email = "wrong@x.com", Password = "badpass" };
            var result = await service.LoginAsync(dto);

            Assert.Null(result);
        }
    }
}
