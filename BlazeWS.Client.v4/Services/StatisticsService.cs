using BlazeWS.Client.Managers;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Statistics;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Services
{
    public class StatisticsService
    {
        JsonServiceClient _client;
        public StatisticsService(string url = "http://blazews.localhost.com/")
        {
            AutoMapManager.Initialise();
            _client = new JsonServiceClient(url);
        }

        public DtoStatistics GetStatistics()
        {
            return _client.Send<DtoStatistics>(new GetStatistics() {  });
        }
        public IEnumerable<DtoTotalStatistics> GetTotatlStatistics()
        {
            return _client.Send<IEnumerable<DtoTotalStatistics>>(new GetTotalStatistics() {  });
        }
    }
}
