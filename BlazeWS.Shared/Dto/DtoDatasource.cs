using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoDataSource : DtoBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public Guid Application { get; set; }
        public bool IsPrimarySource { get; set; }
        public string JsonData { get; set; }
    }
}
