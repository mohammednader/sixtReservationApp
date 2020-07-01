using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public static class Helper
    {
        public static string ToSentenceCase(this string str)
        {
            try
            {
                return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {m.Value[1]}");
            }
            catch
            {
                return str;
            }
        }


        public static DataTable GenerateDataTable<T>(T[] list)
        {
            var dt = new DataTable();
            var props = typeof(T).GetProperties().Where(p => p.CanRead && p.GetGetMethod(false) != null).ToArray();
            for (var i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var type = prop.PropertyType;
                if (Nullable.GetUnderlyingType(type) != null)
                {
                    type = Nullable.GetUnderlyingType(type);
                }
                var name = prop.Name.ToSentenceCase();
                var displayNameAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (displayNameAttr?.Count() > 0)
                {
                    name = ((DisplayNameAttribute)displayNameAttr[0]).DisplayName;
                }
                dt.Columns.Add(name, type);
            }

            for (int i = 0; i < list.Length; i++)
            {
                var item = list[i];
                var row = dt.NewRow();
                for (var j = 0; j < props.Length; j++)
                {
                    var prop = props[j];
                    var value = prop.GetValue(item);
                    row[j] = value ?? DBNull.Value;
                    //if (value != null
                    //    && (value.GetType() == typeof(DateTime)
                    //    || Nullable.GetUnderlyingType(value.GetType()) == typeof(DateTime))
                    //    && (DateTime)value == DateTime.MinValue)
                    //{
                    //    row[j] = DBNull.Value;
                    //}
                    //else
                    //{
                    //    row[j] = value ?? DBNull.Value;
                    //}
                }
                dt.Rows.Add(row);
            }

            return dt;
        }



        public static T CopyProperties<T>(this object source, ref T result)
        {
            var sourceProperties = source.GetType().GetProperties().ToArray();
            var resultProperties = result.GetType().GetProperties().ToArray();

            for (int i = 0; i < sourceProperties.Length; i++)
            {
                System.Reflection.PropertyInfo sourceProperty = sourceProperties[i];
                if (sourceProperty.Name.ToLower() == "id")
                {
                    continue;
                }
                if (sourceProperty.GetType() == typeof(RateSegment) ||
                     sourceProperty.GetType() == typeof(Branch) ||
                     sourceProperty.GetType() == typeof(ReservationStatus))
                {
                    continue;
                }
                var resultProperty = resultProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && sourceProperty.PropertyType == sourceProperty.PropertyType);
                if (resultProperty != null)
                {
                    resultProperty.SetValue(result, sourceProperty.GetValue(source));
                }

                //for (int j = 0; j < resultProperties.Length; j++)
                //{
                //    System.Reflection.PropertyInfo childProperty = resultProperties[j];
                //    if (sourceProperty.Name == childProperty.Name && sourceProperty.PropertyType == childProperty.PropertyType)
                //    {
                //        childProperty.SetValue(result, sourceProperty.GetValue(source));
                //        break;
                //    }
                //}
            }
            return result;
        }
        //public static T CloneObject<T>(this object source)
        //{
        //    T result = Activator.CreateInstance<T>();
        //    //// **** made things  
        //    return result;
        //}


        public static bool SaveStringToFile(string txt, string path)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    File.WriteAllText(path, txt);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;



            }
        }

        //public static GetEnumText(Enum enumType, int enumValue)
        //{
        //    return Enum.GetName(typeof(enumType), enumValue).ToSentenceCase();
        //}

    }
}
