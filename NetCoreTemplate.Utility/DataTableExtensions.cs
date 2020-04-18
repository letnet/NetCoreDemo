using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace NetCoreTemplate.Utility
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// DataTable To IList<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = row.ToEntity<T>();
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// DataRow To T
        /// </summary>
        public static T ToEntity<T>(this DataRow row)
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo property =
                objType.GetProperty(column.ColumnName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null || !property.CanWrite)
                {
                    continue;
                }
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;

                property.SetValue(obj, value, null);

            }
            return obj;
        }


        /// <summary>
        /// List To DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            try
            {
                Type objType = typeof(T);
                DataTable dataTable = new DataTable(objType.Name);
                if (list != null ? list.Count > 0 : false)
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
                    foreach (PropertyDescriptor property in properties)
                    {
                        Type propertyType = property.PropertyType;

                        //nullables must use underlying types
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        //enums also need special treatment
                        if (propertyType.IsEnum)
                            propertyType = Enum.GetUnderlyingType(propertyType); //probably Int32

                        dataTable.Columns.Add(property.Name, propertyType);
                    }

                    foreach (T li in list)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (PropertyDescriptor property1 in properties)
                        {
                            row[property1.Name] = property1.GetValue(li) ?? DBNull.Value; //can't use null
                        }
                        dataTable.Rows.Add(row);

                    }
                }
                return dataTable;
            }
            catch
            {
                return null;
            }
        }
    }

}
