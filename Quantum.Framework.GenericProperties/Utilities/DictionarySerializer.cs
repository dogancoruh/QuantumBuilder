using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Utilities
{
    public class DictionarySerializer
    {
        public static JArray Serialize(Dictionary<string, object> dictionary)
        {
            var result = new JArray();

            foreach (var key in dictionary.Keys)
            {
                var jObjectItem = new JObject();

                jObjectItem["name"] = key;

                object value = dictionary[key];

                if (value is string valueString)
                {
                    jObjectItem["type"] = "string";
                    jObjectItem["value"] = valueString;
                }
                else if (value is int valueInt)
                {
                    jObjectItem["type"] = "integer";
                    jObjectItem["value"] = valueInt;
                }
                else if (value is bool valueBool)
                {
                    jObjectItem["type"] = "boolean";
                    jObjectItem["value"] = valueBool;
                }
                else if (value is double valueDouble)
                {
                    jObjectItem["type"] = "double";
                    jObjectItem["value"] = valueDouble;
                }
                else if (value is decimal valueDecimal)
                {
                    jObjectItem["type"] = "decimal";
                    jObjectItem["value"] = valueDecimal;
                }
                else if (value is Point valuePoint)
                {
                    jObjectItem["type"] = "point";
                    jObjectItem["x"] = valuePoint.X;
                    jObjectItem["y"] = valuePoint.Y;
                }
                else if (value is Size valueSize)
                {
                    jObjectItem["type"] = "size";
                    jObjectItem["width"] = valueSize.Width;
                    jObjectItem["height"] = valueSize.Height;
                }
                else if (value is Rectangle valueRectangle)
                {
                    jObjectItem["type"] = "rectangle";
                    jObjectItem["x"] = valueRectangle.X;
                    jObjectItem["y"] = valueRectangle.Y;
                    jObjectItem["width"] = valueRectangle.Width;
                    jObjectItem["height"] = valueRectangle.Height;
                }
                else if (value is DateTime valueDateTime)
                {
                    jObjectItem["type"] = "datetime";
                    var dateTimeUtc = valueDateTime.ToUniversalTime();
                    jObjectItem["value"] = dateTimeUtc.ToString("o");
                }
                else if (value is Guid valueGuid)
                {
                    jObjectItem["type"] = "guid";
                    jObjectItem["value"] = valueGuid.ToString();
                }
                else if (value is Color valueColor)
                {
                    jObjectItem["type"] = "color";
                    jObjectItem["value"] = ColorHelper.ToHtml(valueColor);
                }
                else if (value.GetType().IsEnum)
                {
                    jObjectItem["type"] = "enumeration";
                    jObjectItem["value"] = value.ToString();
                }
            }

            return result;
        }
        public static Dictionary<string, object> Deserialize(JArray jArray)
        {
            var result = new Dictionary<string, object>();

            foreach (JObject jObject in jArray)
            {
                var itemName = JObjectHelper.GetString(jObject, "name");
                var itemType = JObjectHelper.GetString(jObject, "type");

                switch (itemType)
                {
                    case "string":
                        result.Add(itemName, JObjectHelper.GetString(jObject, "value"));
                        break;
                    case "integer":
                        result.Add(itemName, JObjectHelper.GetInt32(jObject, "value"));
                        break;
                    case "boolean":
                        result.Add(itemName, JObjectHelper.GetBoolean(jObject, "value"));
                        break;
                    case "double":
                        result.Add(itemName, JObjectHelper.GetDouble(jObject, "value"));
                        break;
                    case "decimal":
                        result.Add(itemName, JObjectHelper.GetDecimal(jObject, "value"));
                        break;
                    case "point":
                        result.Add(itemName, new Point()
                        {
                            X = JObjectHelper.GetInt32(jObject, "x"),
                            Y = JObjectHelper.GetInt32(jObject, "y")
                        });
                        break;
                    case "size":
                        result.Add(itemName, new Size()
                        {
                            Width = JObjectHelper.GetInt32(jObject, "width"),
                            Height = JObjectHelper.GetInt32(jObject, "height")
                        });
                        break;
                    case "rectangle":
                        result.Add(itemName, new Rectangle()
                        {
                            X = JObjectHelper.GetInt32(jObject, "x"),
                            Y = JObjectHelper.GetInt32(jObject, "y"),
                            Width = JObjectHelper.GetInt32(jObject, "width"),
                            Height = JObjectHelper.GetInt32(jObject, "height")
                        });
                        break;
                    case "datetime":
                        var valueStr = JObjectHelper.GetString(jObject, "value", DateTime.UtcNow.ToString("o"));
                        result.Add(itemName, DateTime.Parse(valueStr));
                        break;
                    case "guid":
                        result.Add(itemName, JObjectHelper.GetGuid(jObject, "value"));
                        break;
                    case "color":
                        result.Add(itemName, JObjectHelper.GetColor(jObject, "value"));
                        break;
                    case "enumeration":
                        result.Add(itemName, JObjectHelper.GetString(jObject, "value"));
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
