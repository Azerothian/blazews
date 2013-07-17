using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public enum StatisticsTrackType
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class DtoStatistics
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime Started {get;set;}
        public DateTime Ended {get;set;}

    }
}
