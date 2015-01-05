using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Blogrum.Core.Services
{
    public class StringValue : Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            // Check first in our cached results...
            // Look for our 'StringValueAttribute'
            // in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
                output = attrs[0].Value;

            return output;
        }

        public static IEnumerable<SelectListItem> GetDropdown<TEnum>() where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var list = new List<SelectListItem>();

            foreach (var value in Enum.GetValues(enumType))
            {
                string name = Enum.GetName(enumType, value);
                string stringValue = StringEnum.GetStringValue((Enum)value);

                list.Add(new SelectListItem()
                {
                    Text = name,
                    Value = stringValue,
                });
            }

            list.Insert(0, new SelectListItem()
            {
                Text = "Please select",
                Value = ""
            });

            return list;
        }
    }

    public static class StringHelper
    {
        public static string ToSeoSlug(this string url)
        {
            // lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
    }
}
