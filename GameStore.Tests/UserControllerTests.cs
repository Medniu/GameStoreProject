using System;
using Xunit;
using GameStore.Controllers;
using Moq;
using BLL.Interfaces;
using AutoMapper;
using GameStore.Interfaces;
using BLL.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameStore.ViewModels;
using GameStore.Models;
using DAL.Entities;
using System.Net;

namespace GameStore.Tests
{
    public class UserControllerTests
    {
        Mock<IUserService> userServiceMock;
        Mock<IMapper> mapperMock;
        Mock<IUserHelper> userHelperMock;
        public UserControllerTests()
        {
            userServiceMock = new Mock<IUserService>();
            mapperMock = new Mock<IMapper>();
            userHelperMock = new Mock<IUserHelper>();
        }

        [Fact]
        public async void GetUserInfo_ReturnUserProfileViewModel_WhenEverythingIsValid()
        {
            UserDTO testUser = new UserDTO { Name = "Name", Email = "Mail", PhoneNumber = "375445555555" };

            userHelperMock.Setup(rep => rep.GetUserId()).Returns("guidString");

            userServiceMock.Setup(repo => repo.GetInfo(It.IsAny<String>()).Result)
                .Returns(testUser);
            
            mapperMock.Setup(m => m.Map<UserDTO, UserProfileViewModel>(testUser))
                .Returns(new UserProfileViewModel { Name = testUser.Name, Email = testUser.Email, PhoneNumber = testUser.PhoneNumber});          

            UserController controller = new UserController(userServiceMock.Object, mapperMock.Object, userHelperMock.Object);
           
            JsonResult result = await controller.GetUserInfo() as JsonResult;
                       
            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var userViewModel = Assert.IsAssignableFrom<UserProfileViewModel>(result.Value);

            Assert.Equal(testUser.Name, userViewModel.Name);
            Assert.Equal(testUser.PhoneNumber, userViewModel.PhoneNumber);
            Assert.Equal(testUser.Email, userViewModel.Email);            
        }
        [Fact]
        public async void ChangeUserInfo_ReturnUpdatedUserModel_WhenEverythingIsValid()
        {
            var newUserModel = new UserProfileModel { Name = "Igor", Email = "Igor@mail.ru", PhoneNumber = "375445313131" };
            var oldUserModel = new User { UserName = "Vania", Email = "Vanya@mail.ru", PhoneNumber = "375445311313" };
            var changedModel = new ChangedUserDTO 
            {
                Result = true,
                UserDTO = new UserDTO
                {
                    Name = "Igor",
                    Email = "Igor@mail.ru",
                    PhoneNumber = "375445313131"
                }
            };
              
            userServiceMock.Setup(repo => repo.ChangeInfo(It.IsAny<String>(), It.IsAny<UserDTO>()).Result)
                .Returns(changedModel);
            
             mapperMock.Setup(m => m.Map<UserProfileModel,UserDTO>(newUserModel))
                .Returns(new UserDTO { Name = newUserModel.Name, Email = newUserModel.Email, PhoneNumber = newUserModel.PhoneNumber });

            mapperMock.Setup(m => m.Map<UserDTO, UserProfileViewModel>(changedModel.UserDTO))
                .Returns(new UserProfileViewModel
                {
                    Name = changedModel.UserDTO.Name,
                    Email = changedModel.UserDTO.Email,
                    PhoneNumber = changedModel.UserDTO.PhoneNumber
                });
            
            userHelperMock.Setup(rep => rep.GetUserId()).Returns("guidString");

            
            UserController controller = new UserController(userServiceMock.Object, mapperMock.Object, userHelperMock.Object);

            
            JsonResult result = await controller.ChangeUserInfo(newUserModel) as JsonResult;

            
            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var userViewModel = Assert.IsAssignableFrom<UserProfileViewModel>(result.Value);

            Assert.Equal(newUserModel.Name, userViewModel.Name);
            Assert.Equal(newUserModel.PhoneNumber, userViewModel.PhoneNumber);
            Assert.Equal(newUserModel.Email, userViewModel.Email);

            Assert.NotEqual(oldUserModel.UserName, userViewModel.Name);
            Assert.NotEqual(oldUserModel.PhoneNumber, userViewModel.PhoneNumber);
            Assert.NotEqual(oldUserModel.Email, userViewModel.Email);
        }

        [Fact]
        public async void ChangeUserInfo_ReturnBadRequest_IfNewModelIsNotValid()
        {
            var newUserModel = new UserProfileModel { Email = "Igor@mail.ru", PhoneNumber = "375445313131" };
            
            UserController controller = new UserController(userServiceMock.Object, mapperMock.Object, userHelperMock.Object);

            controller.ModelState.AddModelError("Key", "Name is Required");

            await controller.ChangeUserInfo(newUserModel);       
            
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async void ChangeUserInfo_ReturnBadRequest_IfResultOfModelUpdatingIsFalse()
        {
            var newUserModel = new UserProfileModel { Name = "Igor", Email = "Igor@mail.ru", PhoneNumber = "375445313131" };
            
            var changedModel = new ChangedUserDTO
            {
                Result = false,
                UserDTO = null
            };

            userServiceMock.Setup(repo => repo.ChangeInfo(It.IsAny<String>(), It.IsAny<UserDTO>()).Result)
                .Returns(changedModel);          

            userHelperMock.Setup(rep => rep.GetUserId()).Returns("guidString");

            UserController controller = new UserController(userServiceMock.Object, mapperMock.Object, userHelperMock.Object);

            JsonResult result = await controller.ChangeUserInfo(newUserModel) as JsonResult;

            Assert.False(changedModel.Result);
            Assert.Null(changedModel.UserDTO);
            Assert.True(result == null);                       
        }

        [Fact]
        public async void ChangePassword_ReturnStatusCode204_IfPasswordSuccesfullChanged()
        {                     
            var changePasswordModel = new ChangePasswordModel
            {
                OldPassword = "OldCorrectPassword",
                NewPassword = "NewPassword" 
            };
            var userCorrectDto = new UserDTO
            {
                Password = changePasswordModel.OldPassword,
                NewPassword = changePasswordModel.NewPassword
            }; 

            mapperMock.Setup(m => m.Map<ChangePasswordModel, UserDTO>(changePasswordModel))
                .Returns( new UserDTO { Password = changePasswordModel.OldPassword, NewPassword = changePasswordModel.NewPassword});

            userHelperMock.Setup(rep => rep.GetUserId()).Returns("guidString");

            userServiceMock.Setup(rep => rep.ChangePassword(It.IsAny<String>(),
                It.IsAny<UserDTO>()).Result)
                .Returns(true);

            UserController controller = new UserController(userServiceMock.Object, mapperMock.Object, userHelperMock.Object);

            IActionResult response = await controller.ChangePassword(changePasswordModel) ;
            StatusCodeResult objectResponse = Assert.IsType<StatusCodeResult>(response);
                                        
            Assert.Equal(204, objectResponse.StatusCode);
        }
    }
}
