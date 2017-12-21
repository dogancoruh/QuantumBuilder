using Newtonsoft.Json.Linq;
using Quantum.Framework.GenericProperties.Enum;
using Quantum.Framework.GenericProperties.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Data
{
    public class GenericPropertySerializer
    {

        public static JObject SerializePropertiesToObject(GenericPropertyCollection propertyCollection)
        {
            var jObject = new JObject();

            foreach (var property in propertyCollection)
            {
                var propertyName = property.Name;

                switch (property.Type)
                {
                    case GenericPropertyType.String:
                    case GenericPropertyType.Path:
                        jObject[propertyName] = Convert.ToString(property.Value);
                        break;
                    case GenericPropertyType.Integer:
                        jObject[propertyName] = Convert.ToInt32(property.Value);
                        break;
                    case GenericPropertyType.Decimal:
                        jObject[propertyName] = Convert.ToDouble(property.Value);
                        break;
                    case GenericPropertyType.Boolean:
                        jObject[propertyName] = Convert.ToBoolean(property.Value);
                        break;
                    case GenericPropertyType.Enumeration:
                        jObject[propertyName] = Convert.ToString(property.Value);
                        break;
                    case GenericPropertyType.Guid:
                        jObject[propertyName] = Convert.ToString(property.Value);
                        break;
                    case GenericPropertyType.Color:
                        jObject[propertyName] = ColorHelper.ToHtml((Color)property.Value, true);
                        break;
                    case GenericPropertyType.Point:
                        var point = (Point)property.Value;
                        jObject[propertyName] = new JObject()
                        {
                            ["x"] = point.X,
                            ["y"] = point.Y
                        };
                        break;
                    case GenericPropertyType.Size:
                        var size = (Size)property.Value;
                        jObject[propertyName] = new JObject()
                        {
                            ["width"] = size.Width,
                            ["height"] = size.Height
                        };
                        break;
                    case GenericPropertyType.Rectangle:
                        var rectangle = (Rectangle)property.Value;
                        jObject[propertyName] = new JObject()
                        {
                            ["x"] = rectangle.X,
                            ["y"] = rectangle.Y,
                            ["width"] = rectangle.Width,
                            ["height"] = rectangle.Height
                        };
                        break;
                    case GenericPropertyType.DateTime:
                        var dateTime = (DateTime)property.Value;
                        var dateTimeUtc = dateTime.ToUniversalTime();
                        jObject[propertyName] = dateTimeUtc.ToString("o");
                        break;
                    case GenericPropertyType.Version:
                        var version = (Version)property.Value;
                        jObject[propertyName] = version.ToString(3);
                        break;
                    default:
                        break;
                }
            }

            return jObject;
        }

        public static JArray SerializePropertiesToArray(GenericPropertyCollection propertyCollection)
        {
            var jArray = new JArray();

            foreach (var property in propertyCollection)
            {
                var jObject = new JObject();

                jObject["name"] = property.Name;

                switch (property.Type)
                {
                    case Enum.GenericPropertyType.String:
                    case Enum.GenericPropertyType.Path:
                        jObject["value"] = Convert.ToString(property.Value);
                        break;
                    case Enum.GenericPropertyType.Integer:
                        jObject["value"] = Convert.ToInt32(property.Value);
                        break;
                    case Enum.GenericPropertyType.Decimal:
                        jObject["value"] = Convert.ToDouble(property.Value);
                        break;
                    case Enum.GenericPropertyType.Boolean:
                        jObject["value"] = Convert.ToBoolean(property.Value);
                        break;
                    case Enum.GenericPropertyType.Enumeration:
                        jObject["value"] = Convert.ToString(property.Value);
                        break;
                    case Enum.GenericPropertyType.Guid:
                        jObject["value"] = Convert.ToString(property.Value);
                        break;
                    case Enum.GenericPropertyType.Color:
                        jObject["value"] = ColorHelper.ToHtml((Color)property.Value, true);
                        break;
                    case Enum.GenericPropertyType.Point:
                        var point = (Point)property.Value;
                        jObject["value"] = new JObject()
                        {
                            ["x"] = point.X,
                            ["y"] = point.Y
                        };
                        break;
                    case Enum.GenericPropertyType.Size:
                        var size = (Size)property.Value;
                        jObject["value"] = new JObject()
                        {
                            ["width"] = size.Width,
                            ["height"] = size.Height
                        };
                        break;
                    case Enum.GenericPropertyType.Rectangle:
                        var rectangle = (Rectangle)property.Value;
                        jObject["value"] = new JObject()
                        {
                            ["x"] = rectangle.X,
                            ["y"] = rectangle.Y,
                            ["width"] = rectangle.Width,
                            ["height"] = rectangle.Height
                        };
                        break;
                    case Enum.GenericPropertyType.DateTime:
                        var dateTime = (DateTime)property.Value;
                        var dateTimeUtc = dateTime.ToUniversalTime();
                        jObject["value"] = dateTimeUtc.ToString("o");
                        break;
                    case Enum.GenericPropertyType.Version:
                        var version = (Version)property.Value;
                        jObject["value"] = version.ToString(3);
                        break;
                    default:
                        break;
                }

                jArray.Add(jObject);
            }

            return jArray;
        }

        public static void DeserializePropertiesFromArray(GenericPropertyCollection propertyCollection, JArray jArray)
        {
            foreach (var property in propertyCollection)
            {
                foreach (JObject jObjectProperty in jArray)
                {
                    var propertyName = JObjectHelper.GetString(jObjectProperty, "name");
                    if (!string.IsNullOrEmpty(propertyName) && propertyName == property.Name)
                    {
                        switch (property.Type)
                        {
                            case Enum.GenericPropertyType.String:
                            case Enum.GenericPropertyType.Path:
                                property.Value = JObjectHelper.GetString(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Integer:
                                property.Value = JObjectHelper.GetInt32(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Decimal:
                                property.Value = JObjectHelper.GetDouble(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Boolean:
                                property.Value = JObjectHelper.GetBoolean(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Enumeration:
                                property.Value = JObjectHelper.GetString(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Guid:
                                property.Value = JObjectHelper.GetGuid(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Color:
                                property.Value = JObjectHelper.GetColor(jObjectProperty, "value");
                                break;
                            case Enum.GenericPropertyType.Point:
                                var x = JObjectHelper.GetInt32(jObjectProperty, "x");
                                var y = JObjectHelper.GetInt32(jObjectProperty, "y");
                                property.Value = new Point(x, y);
                                break;
                            case Enum.GenericPropertyType.Size:
                                var width = JObjectHelper.GetInt32(jObjectProperty, "width");
                                var height = JObjectHelper.GetInt32(jObjectProperty, "height");
                                property.Value = new Size(width, height);
                                break;
                            case Enum.GenericPropertyType.Rectangle:
                                var x_ = JObjectHelper.GetInt32(jObjectProperty, "x");
                                var y_ = JObjectHelper.GetInt32(jObjectProperty, "y");
                                var width_ = JObjectHelper.GetInt32(jObjectProperty, "width");
                                var height_ = JObjectHelper.GetInt32(jObjectProperty, "height");
                                property.Value = new Rectangle(x_, y_, width_, height_);
                                break;
                            case Enum.GenericPropertyType.DateTime:
                                var valueStr = JObjectHelper.GetString(jObjectProperty, "value", DateTime.UtcNow.ToString("o"));
                                property.Value = DateTime.Parse(valueStr);
                                break;
                            case Enum.GenericPropertyType.Version:
                                var versionStr = JObjectHelper.GetString(jObjectProperty, "value");
                                property.Value = new Version(versionStr);
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
            }
        }

        public static Dictionary<string, object> DeserializeFromJArrayToDictionary(GenericPropertyCollection propertyCollection, JArray jArray)
        {
            var result = new Dictionary<string, object>();

            foreach (var property in propertyCollection)
            {
                foreach (JObject jObjectProperty in jArray)
                {
                    var propertyName = JObjectHelper.GetString(jObjectProperty, "name");
                    if (!string.IsNullOrEmpty(propertyName) && propertyName == property.Name)
                    {
                        switch (property.Type)
                        {
                            case Enum.GenericPropertyType.String:
                            case Enum.GenericPropertyType.Path:
                                result.Add(propertyName, JObjectHelper.GetString(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Integer:
                                result.Add(propertyName, JObjectHelper.GetInt32(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Decimal:
                                result.Add(propertyName, JObjectHelper.GetDouble(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Boolean:
                                result.Add(propertyName, JObjectHelper.GetBoolean(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Enumeration:
                                result.Add(propertyName, JObjectHelper.GetString(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Guid:
                                result.Add(propertyName, JObjectHelper.GetGuid(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Color:
                                result.Add(propertyName, JObjectHelper.GetColor(jObjectProperty, "value"));
                                break;
                            case Enum.GenericPropertyType.Point:
                                var x = JObjectHelper.GetInt32(jObjectProperty, "x");
                                var y = JObjectHelper.GetInt32(jObjectProperty, "y");
                                result.Add(propertyName, new Point(x, y));
                                break;
                            case Enum.GenericPropertyType.Size:
                                var width = JObjectHelper.GetInt32(jObjectProperty, "width");
                                var height = JObjectHelper.GetInt32(jObjectProperty, "height");
                                result.Add(propertyName, new Size(width, height));
                                break;
                            case Enum.GenericPropertyType.Rectangle:
                                var x_ = JObjectHelper.GetInt32(jObjectProperty, "x");
                                var y_ = JObjectHelper.GetInt32(jObjectProperty, "y");
                                var width_ = JObjectHelper.GetInt32(jObjectProperty, "width");
                                var height_ = JObjectHelper.GetInt32(jObjectProperty, "height");
                                result.Add(propertyName, new Rectangle(x_, y_, width_, height_));
                                break;
                            case Enum.GenericPropertyType.DateTime:
                                var valueStr = JObjectHelper.GetString(jObjectProperty, "value", DateTime.UtcNow.ToString("o"));
                                result.Add(propertyName, DateTime.Parse(valueStr));
                                break;
                            case Enum.GenericPropertyType.Version:
                                var versionStr = JObjectHelper.GetString(jObjectProperty, "value");
                                result.Add(propertyName, new Version(versionStr));
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
            }

            return result;
        }

        public static JArray SerializeFromDictionaryToJArray(GenericPropertyCollection propertyCollection, Dictionary<string, object> dictionary)
        {
            var jArray = new JArray();

            foreach (var property in propertyCollection)
            {
                if (dictionary.ContainsKey(property.Name))
                {
                    var jObject = new JObject();

                    jObject["name"] = property.Name;

                    object value = dictionary[property.Name];

                    switch (property.Type)
                    {
                        case Enum.GenericPropertyType.String:
                        case Enum.GenericPropertyType.Path:
                            jObject["value"] = Convert.ToString(value);
                            break;
                        case Enum.GenericPropertyType.Integer:
                            jObject["value"] = Convert.ToInt32(value);
                            break;
                        case Enum.GenericPropertyType.Decimal:
                            jObject["value"] = Convert.ToDouble(value);
                            break;
                        case Enum.GenericPropertyType.Boolean:
                            jObject["value"] = Convert.ToBoolean(value);
                            break;
                        case Enum.GenericPropertyType.Enumeration:
                            jObject["value"] = Convert.ToString(value);
                            break;
                        case Enum.GenericPropertyType.Guid:
                            jObject["value"] = Convert.ToString(value);
                            break;
                        case Enum.GenericPropertyType.Color:
                            jObject["value"] = ColorHelper.ToHtml((Color)value, true);
                            break;
                        case Enum.GenericPropertyType.Point:
                            var point = (Point)value;
                            jObject["value"] = new JObject()
                            {
                                ["x"] = point.X,
                                ["y"] = point.Y
                            };
                            break;
                        case Enum.GenericPropertyType.Size:
                            var size = (Size)value;
                            jObject["value"] = new JObject()
                            {
                                ["width"] = size.Width,
                                ["height"] = size.Height
                            };
                            break;
                        case Enum.GenericPropertyType.Rectangle:
                            var rectangle = (Rectangle)value;
                            jObject["value"] = new JObject()
                            {
                                ["x"] = rectangle.X,
                                ["y"] = rectangle.Y,
                                ["width"] = rectangle.Width,
                                ["height"] = rectangle.Height
                            };
                            break;
                        case Enum.GenericPropertyType.DateTime:
                            var dateTime = (DateTime)value;
                            var dateTimeUtc = dateTime.ToUniversalTime();
                            jObject["value"] = dateTimeUtc.ToString("o");
                            break;
                        case Enum.GenericPropertyType.Version:
                            var version = (Version)value;
                            jObject["value"] = version.ToString(3);
                            break;
                        default:
                            break;
                    }

                    jArray.Add(jObject);
                }
            }

            return jArray;
        }

        public static GenericPropertyCollection SetDictionaryIntoCollection(GenericPropertyCollection collection, Dictionary<string, object> dictionary)
        {
            var result = collection.Clone();

            foreach (var key in dictionary.Keys)
            {
                if (result.Contains(key))
                    result.Set(key, dictionary[key]);
            }

            return result;
        }

        public static Dictionary<string, object> GetDictionaryFromCollection(GenericPropertyCollection collection)
        {
            var result = new Dictionary<string, object>();

            foreach (var property in collection)
            {
                if (property.Value != property.DefaultValue)
                    result.Add(property.Name, property.Value);
            }

            return result;
        }

        public static void DeserializePropertiesFromObject(GenericPropertyCollection propertyCollection, JObject jObject)
        {
            foreach (var property in propertyCollection)
            {
                if (jObject[property.Name] != null)
                {
                    switch (property.Type)
                    {
                        case GenericPropertyType.String:
                        case GenericPropertyType.Path:
                            property.Value = JObjectHelper.GetString(jObject, property.Name);
                            break;
                        case GenericPropertyType.Integer:
                            property.Value = JObjectHelper.GetInt32(jObject, property.Name);
                            break;
                        case GenericPropertyType.Decimal:
                            property.Value = JObjectHelper.GetDouble(jObject, property.Name);
                            break;
                        case GenericPropertyType.Boolean:
                            property.Value = JObjectHelper.GetBoolean(jObject, property.Name);
                            break;
                        case GenericPropertyType.Enumeration:
                            property.Value = JObjectHelper.GetString(jObject, property.Name);
                            break;
                        case GenericPropertyType.Guid:
                            property.Value = JObjectHelper.GetGuid(jObject, property.Name);
                            break;
                        case GenericPropertyType.Color:
                            property.Value = JObjectHelper.GetColor(jObject, property.Name);
                            break;
                        case GenericPropertyType.Point:
                            var x = JObjectHelper.GetInt32(jObject, "x");
                            var y = JObjectHelper.GetInt32(jObject, "y");
                            property.Value = new Point(x, y);
                            break;
                        case GenericPropertyType.Size:
                            var width = JObjectHelper.GetInt32(jObject, "width");
                            var height = JObjectHelper.GetInt32(jObject, "height");
                            property.Value = new Size(width, height);
                            break;
                        case GenericPropertyType.Rectangle:
                            var x_ = JObjectHelper.GetInt32(jObject, "x");
                            var y_ = JObjectHelper.GetInt32(jObject, "y");
                            var width_ = JObjectHelper.GetInt32(jObject, "width");
                            var height_ = JObjectHelper.GetInt32(jObject, "height");
                            property.Value = new Rectangle(x_, y_, width_, height_);
                            break;
                        case GenericPropertyType.DateTime:
                            var valueStr = JObjectHelper.GetString(jObject, property.Name, DateTime.UtcNow.ToString("o"));
                            property.Value = DateTime.Parse(valueStr);
                            break;
                        case GenericPropertyType.Version:
                            var versionStr = JObjectHelper.GetString(jObject, property.Name);
                            property.Value = new Version(versionStr);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
