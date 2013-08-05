using System;
using BlazeWS.Shared.Dto;
using System.Collections.Generic;
using BlazeWS.Server.Services;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Server.Managers;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Messages.Users;
using NUnit.Framework;
using System.Linq;
namespace BlazeWS.Server.Tests
{
    
    
    /// <summary>
    ///This is a test class for ApplicationServiceTest and is intended
    ///to contain all ApplicationServiceTest Unit Tests
    ///</summary>
    [TestFixture]
    public class UserServiceTest
    {

        const string testApplicationName = "ServerTests Users";
        ApplicationService appService;
        UserService userService;

        [SetUp]
        public void Initialize()
        {
            appService = new ApplicationService();
            userService = new UserService();
            TestUtils.Initialise(testApplicationName, appService);
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [TearDown]
        public void Cleanup()
        {
            TestUtils.DeleteApplicationTest(testApplicationName, appService);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [Test]
        public void CreateUserTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            var userName = "CreateUserTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };

            var actual = userService.Any(user);

            Assert.IsNotNull(actual);
            Assert.AreNotEqual(Guid.Empty, actual.Id);
            Assert.AreEqual(userName, actual.Name);

            //CLEANUP
            userService.Any(new DeleteUser() { Id = actual.Id, Application=application.Id });
            
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [Test]
        public void DeleteUserTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "DeleteUserTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };

            var actual = userService.Any(user);
            var result = userService.Any(new DeleteUser() { Id = actual.Id, Application = application.Id });
            Assert.IsTrue(result.Success);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [Test]
        public void GetUserTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "GetUserTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };
            DtoUser expected = userService.Any(user);
            DtoUser actual = userService.Any(new GetUser() { Id = expected.Id, ApplicationId = application.Id });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);

        }
        /// <summary>
        ///A test for Get
        ///</summary>
        [Test]
        public void GetByUserNameTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "GetByUserNameTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };
            DtoUser newentry = userService.Any(user);
            DtoUser actual = userService.Any(new GetUser() { Name = userName, ApplicationId = application.Id });
            Assert.IsNotNull(actual);
            Assert.AreEqual(newentry.Id, actual.Id);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [Test]
        public void GetAllUsersTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "GetAllUsersTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };
            DtoUser newentry = userService.Any(user);


            var actual = userService.Any(new ListUsers() { Application = application.Id });
            var users = actual.Users;
            Assert.IsTrue(users.Count() > 0);
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [Test]
        public void UpdateUserTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "UpdateUserTest";
            var updatedUserName = "New UpdateUserTest";
            CreateUser user = new CreateUser()
            {
                Application = application.Id,
                Name = userName,
                ObjectData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };

            DtoUser newentry = userService.Any(user);

            var update = new UpdateUser()
            {
                Active = newentry.Active,
                Application = newentry.Application,
                DateCreated = newentry.DateCreated,
                DateModified = newentry.DateModified,
                Id = newentry.Id,
                Name = updatedUserName,
                ObjectData = newentry.ObjectData
            };

            var updated = userService.Any(update);

            Assert.IsTrue(updated.Success);
            DtoUser actual = userService.Any(new GetUser() { Id = newentry.Id, ApplicationId = application.Id });
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Name, updatedUserName);
        }
    }
}
