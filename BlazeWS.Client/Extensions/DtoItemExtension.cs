using BlazeWS.Shared.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Extensions
{
    public static class DtoItemExtension
    {

        public static void AddChild(this DtoItem item, DtoItem child)
        {
            child.Parent = item.Id;
        }
        public static IEnumerable<DtoItem> GetChildren(this DtoItem item, ItemService service)
        {

            return service.GetChildren(item.Application, item.Id);
        }

        public static T GetObject<T>(this DtoItem item)
        {
            //if (typeof(T).ToString() != item.ObjectType)
            //{
            // throw new ArgumentException("Type requested to cast to does not match stored content");
            //}
            if (item.ObjectType.StartsWith("application/json;"))
            {
                if (item.ObjectData != null)
                {
                    return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item.ObjectData);
                }
            }
            else
            {
                throw new ArgumentException("Unable to deserialize, stored object type does not start with 'application/json;'");
            }
            return default(T);
        }
        public static void SetObject<T>(this DtoItem item, T obj)
        {
            if (obj != null)
            {
                item.ObjectType = "application/json;type:" + typeof(T).ToString();


                item.ObjectData = ServiceStack.Text.JsonSerializer.SerializeToString(obj);

            }
            else
            {
                item.ObjectData = null;
                item.ObjectType = "";
            }

        }
    }
}
