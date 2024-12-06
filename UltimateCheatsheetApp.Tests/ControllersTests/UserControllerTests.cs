using ApplicationDTO.MSSQL.Users;
using Common.Helpers;
using Common.QueryParameters;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UltimateCheatsheetApp.Controllers;

namespace UltimateCheatsheetApp.Tests.ControllersTests
{

    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<HttpResponse> _mockHttpResponse;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockHttpResponse = new Mock<HttpResponse>();
            _mockHttpContext.Setup(ctx => ctx.Response).Returns(_mockHttpResponse.Object);

            _controller = new UserController(_mockUserService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }


        [Fact]
        public async Task GetUser_ReturnsOkResult_WithUser()
        {
            var userId = 1;
            var user = new UserDTO
            {
                Id = userId,
                Name = "Test User"
            };

            _mockUserService.Setup(service => service.GetById(userId)).ReturnsAsync(user);

            var result = await _controller.GetUser(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userId, returnValue.Id); Assert.Equal("Test User", returnValue.Name);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserNotExists()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.GetById(userId)).ReturnsAsync((UserDTO)null);

            var result = await _controller.GetUser(userId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithUserList()
        {
            // Arrange
            var userParams = new UserParams();
            var users = new PagedList<UserDTO>(
                new List<UserDTO> {
                    new UserDTO { Id = 1, Name = "User1" },
                    new UserDTO { Id = 2, Name = "User2" } },
                pageSize: 10,
                currentPage: 1,
                totalCount: 2);

            _mockUserService.Setup(service => service.GetAll(userParams)).ReturnsAsync(users);

            _mockUserService.Setup(service => service.GetAll(userParams)).ReturnsAsync(users);
            _mockHttpResponse.Setup(response => response.Headers).Returns(new HeaderDictionary());

            var result = await _controller.GetAllUsers(userParams);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedList<UserDTO>>(okResult.Value);

            Assert.Equal(2, returnValue.Count);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(1, returnValue.CurrentPage);
            Assert.Equal(2, returnValue.TotalCount);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsNotFound_WhenNoUsersExist()
        {
            var userParams = new UserParams();

            _mockUserService.Setup(service => service.GetAll(userParams)).ReturnsAsync((PagedList<UserDTO>)null);
            _mockHttpResponse.Setup(response => response.Headers).Returns(new HeaderDictionary());

            var result = await _controller.GetAllUsers(userParams);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WhenUserUpdated()
        {
            var user = new UpdateUserDTO { Id = 1, Name = "UpdatedUser" };
            _mockUserService.Setup(service => service.Update(user)).ReturnsAsync(true);

            var result = await _controller.UpdateUser(user);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User was updated", okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenUserNotUpdated()
        {
            var user = new UpdateUserDTO { Id = 1, Name = "UpdatedUser" };
            _mockUserService.Setup(service => service.Update(user)).ReturnsAsync(false);

            var result = await _controller.UpdateUser(user);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOkResult_WhenUserDeleted()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.Delete(userId)).ReturnsAsync(true);

            var result = await _controller.DeleteUser(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User was deleted", okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenUserNotDeleted()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.Delete(userId)).ReturnsAsync(false);

            var result = await _controller.DeleteUser(userId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

}

