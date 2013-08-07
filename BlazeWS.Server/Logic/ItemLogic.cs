using BlazeWS.Server.Enums;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Items;
using Newtonsoft.Json.Linq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Logic
{
    public class ItemLogic : LogicAbstract<Item>
    {

        /// <summary>
        /// Create a New Item
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateItemResponse.</returns>
        public static Item Create(ISession session, ITransaction tx, DtoItem dtoItem)
        {
            Item item = null;

            if (dtoItem.Application == Guid.Empty)
            {
                throw new ArgumentException("Unable to create an item without an Application id");
            }
            if (string.IsNullOrEmpty(dtoItem.Path))
            {
                throw new ArgumentException("Unable to create an item without an path being set");
                //if Path is set use it to find the parent item and 
            }

            if (dtoItem.Id != Guid.Empty)
                dtoItem.Id = Guid.Empty;
            item = AutoMapper.Mapper.Map<Item>(dtoItem);
            if (item.Parent == Guid.Empty && !string.IsNullOrEmpty(item.Path))
            {
                var parentItem = Axes.GetItemFromPath(session, item.Path, true);
                if (parentItem.Type == (int)ItemType.Generic)
                {
                    item.Parent = parentItem.Id;
                }
                else if (parentItem.Type == (int)ItemType.Datasource)
                {
                    /// forward request to custom data components
                    /// 
                    return null;
                }
            }
            else if (item.Parent == Guid.Empty)
            {
                throw new ArgumentException("To create a item you either need to supply a parent id or a path to the parent");
            }

            ProcessItemData(session, ref item);

            item.Permissions = (int)(ItemPermission.UserRead | ItemPermission.UserWrite | ItemPermission.UserModify);
            item.Active = true;
            item.DateCreated = DateTime.Now;
            item.DateModified = DateTime.Now;

            Save(item, session, tx);
            return item;

        }


        /// <summary>
        /// Gets a Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetItemResponse.</returns>
        public static Item Get(ISession session, GetItem app)
        {

            Item a = LoadBy(session, p => p.Id == app.Id
                && (p.Application != null) ?
                        p.Application.Id == app.Application
                        : false);

            return a;
        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoItem[][].</returns>
        /// <exception cref="System.ArgumentException">Application id has not been supplied</exception>
        public static IEnumerable<Item> List(ISession session, ListItemChildren app)
        {

            if (app.Application == Guid.Empty)
            {
                throw new ArgumentException("Application id has not been supplied");
            }
            IEnumerable<Item> _items;
            
            Guid parentID = Guid.Empty;


            var i = ItemLogic.Axes.GetItemFromPath(session, app.Path, true);
            if (i != null)
            {
                parentID = i.Id;
            }
            else
            {
                throw new ArgumentException("Unable to find a parent");
            }
            


            _items = LoadAllBy(session, p => (p.Application.Id == app.Application)
                && p.Parent == parentID).ToArray();

            return _items;


        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateItemResponse.</returns>
        public static bool Update(ISession session, ITransaction tx, UpdateItem app)
        {
            var result = false;

            var item = AutoMapper.Mapper.Map<Item>(app);
            if (item != null && item.Id != Guid.Empty)
            {
                Save(item, session, tx);
                result = true;
            }
            return result;// new UpdateItemResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="action">The app.</param>
        /// <returns>DeleteItemResponse.</returns>
        public static bool Delete(ISession session, ITransaction tx, DeleteItem action)
        {

            if (action.Application == Guid.Empty)
            {
                throw new ArgumentException("Application Id was not supplied. Unable to delete Item");
            }
            if (string.IsNullOrEmpty(action.Path))
            {
                throw new ArgumentException("Item Path was not supplied. Unable to delete Item");
            }
            var result = false;

            var a = LoadBy(session, p => p.Path == action.Path && p.Application == null ? false : p.Application.Id == action.Application);
            if (a != null)
            {

                Delete(a, session, tx);
                result = true;
            }
            return result;
            

        }


        private static void ProcessItemData(ISession session, ref Item item)
        {

            item.Path = Axes.GetFullPath(session, item);

            if (string.IsNullOrEmpty(item.JsonData))
            {
                item.JsonData = "{}";

            }
            JObject o = JObject.Parse(item.JsonData);//TODO: Catch exceptions here

            if (item.Type == (int)ItemType.Datasource && o["Datasource"] == null)
            {
                o.Add(new JProperty("Datasource", new JObject(new JProperty("Type", ""), new JProperty("Properties", new JObject()))));
            }
            item.JsonData = o.ToString();
        }

        public static class Axes
        {

            public static Item GetItemFromPath(ISession session, string path, bool getNearestItem = false)
            {
                path = path.TrimEnd('/');

                var item = LoadAllBy(session, p => p.Path.ToLower() == path.ToLower()).FirstOrDefault();
                if (item == null && getNearestItem)
                {
                    var arr = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = arr.Count(); i >= 0; i--)
                    {
                        var newpath = "/" + String.Join("/", arr.Take(i));
                        item = LoadAllBy(session, p => p.Path.ToLower() == newpath).FirstOrDefault();
                        if (item != null)
                            break;
                    }

                }
                return item;

            }



            public static IEnumerable<Item> GetAscendants(ISession session, Item item)
            {
                List<Item> _array = null;
                return GetAscendants(session, item, ref _array);
            }
            public static IEnumerable<Item> GetAscendants(ISession session, Item item, ref List<Item> _array)
            {
                if (_array == null)
                {
                    _array = new List<Item>();
                }

                if (item.Parent != Guid.Empty)
                {
                    var parent = LoadById(session, item.Parent);
                    session.Evict(parent);
                    GetAscendants(session, parent, ref _array);

                }
                _array.Add(item);
                return _array;
            }
            public static IEnumerable<Item> GetChildrenByParent(Item item, ISession session, ITransaction tx, params Func<Item, bool>[] filters)
            {
                ///TODO : find a cleaner way..
                var param = new Func<Item, bool>[] { i => i.Parent == item.Id }.Concat(filters.AsEnumerable()).ToArray();

                return LoadAllBy(session, param);
            }

            public static string GetFullPath(ISession session, Item item)
            {
                var parents = GetAscendants(session, item);

                StringBuilder sb = new StringBuilder();
                foreach (var p in parents)
                {
                    sb.Append("/");
                    sb.Append(p.Name);
                }
                return sb.ToString();

            }

        }

    }
}
