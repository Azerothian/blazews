using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Users;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client
{
    public class UserService
    {
        JsonServiceClient _client;
        public UserService(JsonServiceClient client)
        {
            _client = client;
        }
        public DtoUser Create(DtoUser dto)
        {
            var msg = AutoMapper.Mapper.Map<DtoUser, CreateUser>(dto);
            return _client.Send<CreateUserResponse>(msg);
        }

        public DtoUser Get(Guid id)
        {
            return _client.Send<GetUserResponse>(new GetUser() { Id = id });
        }
        public DtoUser GetByName(string name)
        {
            return _client.Send<GetUserResponse>(new GetUser() { Name = name });
        }

        public IEnumerable<DtoUser> GetAll()
        {
            return _client.Send<ListUsersResponse>(new ListUsers()).Users;
        }

        public bool Update(DtoUser dto)
        {

            var msg = AutoMapper.Mapper.Map<DtoUser, UpdateUser>(dto);
            //UserService.
            return _client.Send<UpdateUserResponse>(msg).Success;
        }
        public bool Delete(Guid id)
        {
            return _client.Send<DeleteUserResponse>(new DeleteUser() { Id = id }).Success;
        }


    }
}
