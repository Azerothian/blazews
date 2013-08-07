using System;
using BlazeWS.Server.Services;
using BlazeWS.Shared.Messages.Items;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Server.Models;
using BlazeWS.Server.Managers;
using System.Linq;
using BlazeWS.Shared.Dto;
using NUnit.Framework;
namespace BlazeWS.Server.Tests
{
    [TestFixture]
    public class ItemServiceTest
    {
        ApplicationService appService;
        ItemService itemService;
        const string testApplicationName = "ItemServiceTest";
       // DtoApplication application;
        [SetUp]
        public void Initialize()
        {
            appService = new ApplicationService();
            itemService = new ItemService();
            TestUtils.Initialise(testApplicationName, appService);
        }

        [TearDown]
        public void Cleanup()
        {
            TestUtils.DeleteApplicationTest(testApplicationName, appService);
        }



        [Test]
        public void CreateItemTest()
        {
            DtoApplication _application = appService.Any(new GetApplication() { ApplicationName = testApplicationName });
            DtoItem i = itemService.Any(new CreateItem()
            {
                Name = "New Item",
                Path = "/root/New Item",
                Application = _application.Id

            });
            Assert.IsTrue(i.Name == "New Item");
        }
        [Test]
        public void ItemHasChildren()
        {
            DtoApplication _application = appService.Any(new GetApplication() { ApplicationName = testApplicationName });

            var i = itemService.Any(new CreateItem()
            {
                Name = "New Child Item",
                Path = "/root/New Child Item",
                Application = _application.Id,
            });
          //  var item = itemService.Any(new GetItem() { Application = application.Id, Id = application.BaseItem });
            var children = itemService.Any(new ListItemChildren() {  Path = "/root", Application = _application.Id });


            Assert.IsTrue(children.Items.Count() > 0);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [Test]
        public void DeleteItemTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var userName = "DeleteItemTest";
            var newitem = new CreateItem()
            {
                Application = application.Id,
                Name = userName,
                JsonDataType = "application/json;",
                JsonData = " { 'Password':'1234567890', 'GoogleAuth': '123123' }"
            };

            var actual = itemService.Any(newitem);

            var result = itemService.Any(new DeleteItem() { Path = newitem.Path, Application = actual.Application });
            Assert.IsTrue(result.Success);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [Test]
        public void GetItemTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);

            var name = "GetItemTest";
            CreateItem newitem = new CreateItem()
            {
                Application = application.Id,
                Name = name,
                JsonDataType = "application/json;",
                JsonData = " { Password:'1234567890', GoogleAuth: '123123' }"
            };
            DtoItem expected = itemService.Any(newitem);
            DtoItem actual = itemService.Any(new GetItem() { Id = expected.Id, Application = application.Id });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);

        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [Test]
        public void UpdateItemTest()
        {
            DtoApplication application = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
           
            var name = "UpdateItemTest";
            var updatedName = "New UpdateItemTest";
            CreateItem newitem = new CreateItem()
            {
                Application = application.Id,
                Name = name,
                JsonDataType = "application/json;",
                JsonData = "{'Password':'1234567890', 'GoogleAuth': '123123' }"
            };

            DtoItem newentry = itemService.Any(newitem);

            var update = new UpdateItem()
            {
                Active = newentry.Active,
                Application = newentry.Application,
                DateCreated = newentry.DateCreated,
                DateModified = newentry.DateModified,
                Id = newentry.Id,
                Name = updatedName,
                JsonData = newentry.JsonData
            };

            var updated = itemService.Any(update);

            Assert.IsTrue(updated.Success);
            DtoItem actual = itemService.Any(new GetItem() { Id = newentry.Id, Application = application.Id });
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Name, updatedName);
        }

    }
}
