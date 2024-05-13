using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class UserDemographicsDetailsControllerTest
    {
        [Fact]
        public async Task GetUserDemographicsDetails_ReturnsOkResult()
        {
            // arrange
            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserDemographicsDetails>());
            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.GetUserDemographicsDetails();

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserDemographicsDetails>>(okResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public async Task GetUserDemographicsDetailsById_ReturnsOkResult()
        {
            // arrange
            var userId = 1;
            var userDemographicsDetails = new UserDemographicsDetails { User_Id = userId, /* Other properties */ };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync(userDemographicsDetails);

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.GetUserDemographicsDetailsById(userId);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<UserDemographicsDetails>(okResult.Value);
            Assert.Equal(userId, model.User_Id);
        }
        [Fact]
        public async Task GetUserDemographicsDetailsById_WhenUserDoesNotExist_ReturnsNotFound()
        {
            // arrange
            int userId = 123;
            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync((UserDemographicsDetails)null);
            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.GetUserDemographicsDetailsById(userId);

            // assert
            var actionResult = Assert.IsType<ActionResult<UserDemographicsDetails>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PutUserDemographicsDetails_ValidInput_ReturnsOkResult()
        {
            // arrange
            var userId = 1;
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = userId,
                Name = "John Doe",
                Gender = 1, // Assuming 1 represents male, 0 for female, etc.
                Date_Of_fBirth = DateTime.Parse("1990-01-01"),
                Country = "USA",
                Language = "English",
                Race = "White",
                Premium_User = true
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.Update(userDemographicsDetails)).Returns(Task.CompletedTask);

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PutUserDemographicsDetails(userId, userDemographicsDetails);

            // assert
            Assert.IsType<OkObjectResult>(result);
        }
        

        [Fact]
        public async Task PutUserDemographicsDetails_ReturnsBadRequest_WhenUserIdNotMatching()
        {
            // arrange
            int userId = 1;
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = 2
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PutUserDemographicsDetails(userId, userDemographicsDetails);

            // assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task PutUserDemographicsDetails_ReturnsNoContent_WhenUpdateFails()
        {
            // arrange
            var userId = 1;
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = userId,
             
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.Update(userDemographicsDetails)).Throws<DbUpdateConcurrencyException>();

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PutUserDemographicsDetails(userId, userDemographicsDetails);

            // assert
            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task PostUserDemographicsDetails_ValidInput_ReturnsOkResult()
        {
            // arrange
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = 1,
                Name = "Jane Doe",
                Gender = 0,
                Date_Of_fBirth = DateTime.Parse("1995-02-15"),
                Country = "Canada",
                Language = "French",
                Race = "Asian",
                Premium_User = false
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.Add(userDemographicsDetails)).Returns(Task.CompletedTask);

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PostUserDemographicsDetails(userDemographicsDetails);

            // assert
            Assert.IsType<ActionResult<UserDemographicsDetails>>(result);
            Assert.IsType<OkObjectResult>((result as ActionResult<UserDemographicsDetails>).Result);
        }

        [Fact]
        public async Task PostUserDemographicsDetails_ReturnsBadRequest_WhenDataIsNull()
        {
            // arrange
            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PostUserDemographicsDetails(null);

            // assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Data is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostUserDemographicsDetails_ReturnsInternalServerError_OnException()
        {
            // arrange
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = 1,
                Name = "Jane Doe",
                Gender = 0,
                Date_Of_fBirth = DateTime.Parse("1995-02-15"),
                Country = "Canada",
                Language = "French",
                Race = "Asian",
                Premium_User = false
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.Add(userDemographicsDetails)).ThrowsAsync(new Exception("Test exception"));

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.PostUserDemographicsDetails(userDemographicsDetails);

            // assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task DeleteUserDemographicsDetails_ValidInput_ReturnsOkResult()
        {
            // arrange
            var userId = 1;
            var userDemographicsDetails = new UserDemographicsDetails
            {
                User_Id = userId,
                Name = "John Doe",
                Gender = 1,
                Date_Of_fBirth = DateTime.Parse("1990-01-01"),
                Country = "USA",
                Language = "English",
                Race = "White",
                Premium_User = true
            };

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync(userDemographicsDetails);
            mockRepository.Setup(repo => repo.Delete(userDemographicsDetails)).Returns(Task.CompletedTask);

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.DeleteUserDemographicsDetails(userId);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(userDemographicsDetails, okResult.Value);
        }

        [Fact]
        public async Task DeleteUserDemographicsDetails_ReturnsNotFound_WhenUserNotFound()
        {
            // arrange
            var userId = 1;
            UserDemographicsDetails userDemographicsDetails = null;

            var mockRepository = new Mock<IRepository<UserDemographicsDetails>>();
            mockRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync(userDemographicsDetails);

            var controller = new UserDemographicsDetailsController(mockRepository.Object);

            // act
            var result = await controller.DeleteUserDemographicsDetails(userId);

            // assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }


    }
}
