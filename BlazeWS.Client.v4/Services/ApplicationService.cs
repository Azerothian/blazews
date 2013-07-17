using BlazeWS.Client.Managers;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Service
{
    public class ApplicationService
    {
        JsonServiceClient _client;
        public ApplicationService(string url = "http://blazews.localhost.com/")
        {
            AutoMapManager.Initialise();
            _client = new JsonServiceClient(url);
        }


        public DtoApplication Create(DtoApplication dto)
        {
            var msg = AutoMapper.Mapper.Map<DtoApplication, CreateApplication>(dto);
           return _client.Send<CreateApplicationResponse>(msg);
        }
        
        public DtoApplication Get(Guid id)
        {
            return _client.Send<GetApplicationResponse>(new GetApplication() { Id = id });
        }
        public DtoApplication GetByApplicationName(string name)
        {
            return _client.Send<GetApplicationResponse>(new GetApplication() { ApplicationName = name });
        }

        public IEnumerable<DtoApplication> GetAll()
        {
            return _client.Send<IEnumerable<DtoApplication>>(new ListApplications());
        }

        public bool Update(DtoApplication dto)
        {

            var msg = AutoMapper.Mapper.Map<DtoApplication, UpdateApplication>(dto);
            //ApplicationService.
            return _client.Send<UpdateApplicationResponse>(msg).Success;
        }
        public bool Delete(Guid id)
        {
            return _client.Send<DeleteApplicationResponse>(new DeleteApplication() { Id = id }).Success;
        }

    }
}
