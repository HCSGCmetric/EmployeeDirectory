using EmployeeDirectory.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeDirectory.Helper
{
    public static class HelperCls
    {
        /// <summary>
        /// Datas the row to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static T DataRowToObject<T>(this DataRow row) where T : class, new()
        {
            try
            {
                T obj = new T();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    }
                    catch
                    {
                        continue;
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Datas the table to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            //propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);


                            var property = obj.GetType().GetProperty(prop.Name);
                            if (property != null)
                            {
                                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                var value = row[prop.Name];
                                if (property.PropertyType.FullName == "System.Boolean") 
                                {
                                    if (value.ToString().ToLower() == "true") 
                                    {
                                        value = 1;
                                    }
                                    if (value.ToString().ToLower() == "false")
                                    {
                                        value = 0;
                                    }
                                    value = int.Parse(value.ToString());
                                }
                                
                                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                property.SetValue(obj, safeValue, null);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Datas the table to identifier name list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static List<T> DataTableToIdNameList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                //table.Columns[1].ColumnName = "Name";
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return null;
            }
        }

        public static DataTable ToDataTable<TSource>(this IList<TSource> data)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                    prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static int[] GetDisplayRecordCount(int pageNo, int pageSize)
        {
            int[] arr = new int[2];
            if (pageNo > 0)
            {
                pageNo = pageNo - 1;
                arr[0] = pageSize * pageNo + 1;
                arr[1] = arr[0] + pageSize - 1;
                return arr;
            }
            else
            {
                arr[0] = 0;
                arr[1] = int.MaxValue;
            }

            return arr;
        }

        public static System.Web.Mvc.MvcHtmlString CreateDynamicMenuItem(this HtmlHelper helper, string ControllerName)
        {
            StringBuilder sb = new StringBuilder("");
            List<FormPermissionEntity> listFormPermissionsEntity = CacheHelper.Get<List<FormPermissionEntity>>(CacheKeys.FormPermissions);
            if (listFormPermissionsEntity != null)
            {
                var ApplicationName = listFormPermissionsEntity.Where(i => i.FormControllerName == ControllerName).Select(i => i.FormApplicationName).FirstOrDefault();
                var Liveurl = System.Configuration.ConfigurationManager.AppSettings["LivesubURL"];
                    //Liveurl = "/" + Liveurl + "HsgUser/GetDivRegDistList";

                foreach (var item in listFormPermissionsEntity.Where(i => i.FormApplicationName == ApplicationName).Where(i => i.IsView == true).GroupBy(x => new { x.FormName, x.FormURL }).Select(x => new { x.Key.FormName, x.Key.FormURL }).ToList())
                {
                    sb.Append("<li><a aria-expanded='false' aria-haspopup='true' role='button' href=/" + Liveurl + item.FormURL + ">" + item.FormName + "</a></li>");

                }
            }

            return System.Web.Mvc.MvcHtmlString.Create(sb.ToString());
        }
    }
}