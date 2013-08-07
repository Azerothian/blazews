using Illisian.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Models
{
    public class DataSource : Entity<DataSource>
    {
        public virtual string Name { get; set; }
        public virtual string Path { get; set; }
        public virtual string Type { get; set; }
        public virtual Application Application { get; set; }
        public virtual bool IsPrimarySource { get; set; }
        public virtual string JsonData { get; set; }
    }
}
