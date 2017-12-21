using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Utilities
{
    public static class JObjectHelper
    {
        public static string GetStringValue(JObject jObject, string propertyName)
        {
            return GetStringValue(jObject, propertyName, string.Empty);
        }

        public static string GetStringValue(JObject jObject, string propertyName, string defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            return jObject[propertyName] != null ? Convert.ToString(jObject[propertyName]) : defaultValue;
        }

        public static int GetInt32Value(JObject jObject, string propertyName)
        {
            return GetInt32Value(jObject, propertyName, default(int));
        }

        public static int GetInt32Value(JObject jObject, string propertyName, int defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            int value = defaultValue;

            if (jObject[propertyName] != null)
                int.TryParse(Convert.ToString(jObject[propertyName]), out value);

            return value;
        }

        public static double GetDoubleValue(JObject jObject, string propertyName)
        {
            return GetDoubleValue(jObject, propertyName, default(double));
        }

        public static double GetDoubleValue(JObject jObject, string propertyName, double defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            if (jObject[propertyName] != null)
            {
                try
                {
                    string text = Convert.ToString(jObject[propertyName]);
                    text = text.Replace(",", ".");
                    return jObject[propertyName] != null ? double.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture) : defaultValue;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
            else
                return defaultValue;
        }

        public static decimal GetDecimalValue(JObject jObject, string propertyName)
        {
            return GetDecimalValue(jObject, propertyName, default(decimal));
        }

        public static decimal GetDecimalValue(JObject jObject, string propertyName, decimal defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            if (jObject[propertyName] != null)
            {
                try
                {
                    string text = Convert.ToString(jObject[propertyName]);
                    text = text.Replace(",", ".");
                    return jObject[propertyName] != null ? decimal.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture) : defaultValue;
                }
                catch (FormatException ex)
                {
                    return defaultValue;
                }
            }
            else
                return defaultValue;
        }

        public static bool GetBoolValue(JObject jObject, string propertyName)
        {
            return GetBoolValue(jObject, propertyName, default(bool));
        }

        public static bool GetBoolValue(JObject jObject, string propertyName, bool defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            return jObject[propertyName] != null ? Convert.ToBoolean(jObject[propertyName]) : defaultValue;
        }

        public static Guid GetGuidValue(JObject jObject, string propertyName)
        {
            return GetGuidValue(jObject, propertyName, default(Guid));
        }

        public static Guid GetGuidValue(JObject jObject, string propertyName, Guid defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            return jObject[propertyName] != null ? new Guid(Convert.ToString(jObject[propertyName])) : defaultValue;
        }

        public static T GetEnumValue<T>(JObject jObject, string propertyName)
        {
            return GetEnumValue<T>(jObject, propertyName, default(T));
        }

        public static T GetEnumValue<T>(JObject jObject, string propertyName, T defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            return jObject[propertyName] != null ? (T)System.Enum.Parse(typeof(T), Convert.ToString(jObject[propertyName])) : defaultValue;
        }

        public static T GetEnumValueIncaseSensitive<T>(JObject jObject, string propertyName, T defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            string[] enumNames = System.Enum.GetNames(typeof(T));

            string text = GetStringValue(jObject, propertyName);

            if (!text.Equals(string.Empty))
                foreach (string enumName in enumNames)
                    if (text.ToLowerInvariant().Equals(enumName.ToLowerInvariant()))
                        return (T)System.Enum.Parse(typeof(T), enumName);

            return default(T);
        }

        public static Color GetColorValue(JObject jObject, string propertyName)
        {
            return GetColorValue(jObject, propertyName, default(Color));
        }

        public static Color GetColorValue(JObject jObject, string propertyName, Color defaultValue)
        {
            if (jObject == null)
                return defaultValue;

            if (jObject[propertyName] != null)
            {
                string colorText = Convert.ToString(jObject[propertyName]);
                return ColorTranslator.FromHtml(colorText);
            }
            else
                return defaultValue;
        }

        public static void SetValue(JObject jObject, string propertyName, object value)
        {
            if (value is string)
                jObject[propertyName] = Convert.ToString(value);
            else if (value is int)
                jObject[propertyName] = Convert.ToInt32(value);
            else if (value is float || value is double)
                jObject[propertyName] = Convert.ToDouble(value);
            else if (value is decimal)
                jObject[propertyName] = Convert.ToDecimal(value);
            else if (value is bool)
                jObject[propertyName] = Convert.ToBoolean(value);
            else
                throw new Exception("Value type not supported");
        }
    }
}
