using AutoMapper;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Shared.Messages.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Managers
{
    public class AutoMapManager
    {
        static bool initialised = false;
        public static void Initialise()
        {
            if (!initialised)
            {
                 AutoMapper.Mapper.CreateMap<DateTime, DateTime>().ConvertUsing
                     (new DateTimeFix()); 

                AutoMapper.Mapper.CreateMap<DtoApplication, CreateApplication>();
                AutoMapper.Mapper.CreateMap<DtoApplication, UpdateApplication>();


                AutoMapper.Mapper.CreateMap<DtoItem, CreateItem>();
                AutoMapper.Mapper.CreateMap<DtoItem, UpdateItem>();


                initialised = true;
            }

        }
    }
    public class DateTimeFix : ITypeConverter<DateTime,DateTime>
    {

        public DateTime Convert(ResolutionContext context)
        {
            var baseDateTime = new DateTime(1970, 1, 1);
            if (!context.IsSourceValueNull)
            {
                var source = (DateTime)context.SourceValue;
                if (source < baseDateTime)
                {
                    return baseDateTime;
                }
                return source;
            }
            return baseDateTime;
        }
    }
}
