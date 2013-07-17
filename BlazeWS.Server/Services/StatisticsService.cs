using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Shared.Messages.Statistics;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    public class StatisticsTracker : DtoStatistics, IDisposable
    {
        public StatisticsTracker(string name, StatisticsTrackType type)
            : base()
        {
            this.Name = name;
            this.Type = (int)type;
            this.Started = DateTime.Now;

        }

        public void Dispose()
        {
            StatisticsLogic.TrackEnd(this);
        }
    }

    public class StatisticsService : Service
    {
      
        /// <summary>
        /// Create a New Application
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DtoStatistics Any(GetStatistics s)
        {
            var result = (from v in StatisticsLogic.Statistics
                          select v).FirstOrDefault();
            if (result != null)
            {
                StatisticsLogic.Statistics.RemoveRange(0, 1);
            }
            return result;
        }

        public IEnumerable<DtoTotalStatistics> Any(GetTotalStatistics s)
        {
            foreach (var a in StatisticsLogic.TotalAccess.Keys)
            {
                yield return new DtoTotalStatistics()
                {
                    Hits = StatisticsLogic.TotalAccess[a],
                    Type = (int)a
                };
            }

        }


        internal static DtoStatistics TrackStart(string name, StatisticsTrackType type)
        {
            return new DtoStatistics()
            {
                Name = name,
                Type = (int)type,
                Started = DateTime.Now
            };
        }
    }

}