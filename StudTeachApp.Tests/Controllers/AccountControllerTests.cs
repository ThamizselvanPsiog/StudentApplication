using Microsoft.AspNetCore.Mvc;
using StudTeacher.Controllers;
using StudTeacher.Models;
using StudTeacher.StudTeachApp.Tests.Data;

namespace StudTeacher.StudTeachApp.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsAllAccounts()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var controller = new AccountController(context);

            var result = await controller.GetAll() as OkObjectResult;

            Assert.NotNull(result);
            var accounts = Assert.IsAssignableFrom<IEnumerable<object>>(result.Value);
            Assert.True(accounts.Count() >= 2);
        }

        [Fact]
        public async Task Delete_ExistingAccount_ReturnsOk()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var controller = new AccountController(context);

            var result = await controller.Delete(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Deleted", result.Value);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var controller = new AccountController(context);

            var result = await controller.Delete(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ValidAccount_ReturnsOk()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var controller = new AccountController(context);

            var updated = new UpdateAccountDTO {Designation = "Student", Email = "u@x.com" };
            var result = await controller.Update(1, updated) as OkObjectResult;

            Assert.NotNull(result);
            var account = Assert.IsType<Account>(result.Value);
            Assert.Equal(updated.Designation, account.Designation);
            Assert.Equal(updated.Email, account.Email);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFound()
        {
            var context = AppDbContextMockHelper.GetDbContext();
            var controller = new AccountController(context);

            var updated = new UpdateAccountDTO {Designation = "Student", Email = "i@x.com" };
            var result = await controller.Update(999, updated);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
