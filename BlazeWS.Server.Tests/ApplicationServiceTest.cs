using System;
using BlazeWS.Shared.Dto;
using System.Collections.Generic;
using BlazeWS.Server.Services;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Server.Managers;
using BlazeWS.Server.Models;
using System.Linq;
using NUnit.Framework;

namespace BlazeWS.Server.Tests
{
    
    
    /// <summary>
    ///This is a test class for ApplicationServiceTest and is intended
    ///to contain all ApplicationServiceTest Unit Tests
    ///</summary>
    [TestFixture]
    public class ApplicationServiceTest
    {

        const string testApplicationName = "ApplicationServiceTest";
        const string updatedApplicationName = "Updated ApplicationServiceTest";
        ApplicationService appService;

        [SetUp]
        public void Initialize()
        {
            appService = new ApplicationService();
            TestUtils.Initialise(testApplicationName, appService);
        }

        [TearDown]
        public void Cleanup()
        {
            TestUtils.DeleteApplicationTest(updatedApplicationName, appService);
            TestUtils.DeleteApplicationTest(testApplicationName, appService);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [Test]
        public void CreateApplicationTest()
        {
            DtoApplication actual = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(Guid.Empty, actual.Id);
            Assert.AreNotEqual(Guid.Empty, actual.BaseItem);
            Assert.AreEqual(testApplicationName, actual.Name);
            
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [Test]
        public void DeleteApplicationTest()
        {

            DtoApplication actual = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            var result = appService.Any(new DeleteApplication() { Id = actual.Id });
            Assert.IsTrue(result.Success);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [Test]
        public void GetApplicationTest()
        {
            var expected = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            DtoApplication actual = appService.Any(new GetApplication() { Id = expected.Id });
            Assert.AreEqual(expected.Id, actual.Id);
        }
        /// <summary>
        ///A test for Get
        ///</summary>
        [Test]
        public void GetByApplicationNameTest()
        {
            var expected = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            DtoApplication actual = appService.Any(new GetApplication() { ApplicationName = expected.Name });
            Assert.AreEqual(expected.Id, actual.Id);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [Test]
        public void GetAllApplicationsTest()
        {
            var actual = appService.Any(new ListApplications());
            Assert.IsNotNull(actual.Applications);
            Assert.IsTrue(actual.Applications.Count() > 0);
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [Test]
        public void UpdateApplicationTest()
        {
            var dto = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
            dto.Name = updatedApplicationName;

            var msg = new UpdateApplication()
            {
                Active = dto.Active,
                BaseItem = dto.BaseItem,
                DateCreated = dto.DateCreated,
                DateModified = dto.DateModified,
                Id = dto.Id,
                Name = dto.Name
            };

            var result = appService.Any(msg);

            if (!result.Success)
            {
                Assert.Fail();
            } else {
                var message = appService.Any(new GetApplication() { Id = dto.Id });

                Assert.IsTrue(message.Name == updatedApplicationName);
            }
                
        }
    }
}
