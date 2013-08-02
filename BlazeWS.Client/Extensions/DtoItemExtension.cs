using BlazeWS.Shared.Dto;
using System;
using System.Collections.Generic;
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

            if (!string.IsNullOrEmpty(item.ObjectData))
            {
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item.ObjectData);
            }
            return default(T);
        }
        public static void SetObject<T>(this DtoItem item, T obj)
        {
            if (obj != null)
            {
                item.ObjectType = typeof(T).ToString();
                item.ObjectData = ServiceStack.Text.JsonSerializer.SerializeToString(obj);
            }
            else
            {
                item.ObjectData = "";
                item.ObjectType = "";
            }

        }
    }
}
