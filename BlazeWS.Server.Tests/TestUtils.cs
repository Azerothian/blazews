using BlazeWS.Server.Managers;
using BlazeWS.Server.Models;
using BlazeWS.Server.Services;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Tests
{
    public class TestUtils
    {
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public static void Initialise(string testApplicationName, ApplicationService appService)
        {
            Illisian.Nhibernate.Database.Context.Configure(typeof(Application).Assembly);
            new AutoMapManager().Initialise();

            //Force Init on nhibernate to get more accurate timing
            
            DtoApplication actual = TestUtils.CreateTestApplicationDto(testApplicationName, appService);
        }

        public static DtoApplication CreateTestApplicationDto(string testApplicationName, ApplicationService service)
        {
            DtoApplication _application = service.Any(new GetApplication() { ApplicationName = testApplicationName });


            if (_application == null)
            {
                CreateApplication dto = new CreateApplication()
                {
                    Name = testApplicationName
                };

                _application = service.Any(dto);
            }

            return _application;
        }
        public static void DeleteApplicationTest(string testApplicationName, ApplicationService service)
        {
            DtoApplication _application = service.Any(new GetApplication() { ApplicationName = testApplicationName });

            if (_application != null)
            {
                service.Any(new DeleteApplication() { Id = _application.Id });
            }
        }
    }
}
