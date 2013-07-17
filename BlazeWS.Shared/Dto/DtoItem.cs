using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoItem :DtoBase
    {
        public DtoItem()
        {
        }

        public string Name { get; set; }
        public string ObjectType { get; set; }
        public string ObjectData { get; set; }

        public Guid Application { get; set; }
        public Guid Parent { get; set; }

    }
}
