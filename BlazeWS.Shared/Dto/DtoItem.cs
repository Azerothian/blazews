using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoItem : DtoBase
    {
        public DtoItem()
        {
        }

        public Guid Parent { get; set; }

        public string Path { get; set; } 
        public string Name { get; set; }
        public int Type { get; set; }
        public string JsonDataType { get; set; }
        public string JsonData { get; set; }
        public int SystemPermissions { get; set; }
        public Guid ItemData { get; set; }
        public Guid Application { get; set; }
        public Guid ModifiedBy { get; set; }

    }
}
