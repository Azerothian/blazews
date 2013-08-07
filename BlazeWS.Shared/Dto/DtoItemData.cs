using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoItemData : DtoBase
    {
        public DtoItemData()
        {
        }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual Guid Item { get; set; } 

    }
}
