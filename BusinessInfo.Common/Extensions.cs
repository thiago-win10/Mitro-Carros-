using Newtonsoft.Json;
using System.ComponentModel;

namespace BusinessInfo.Common
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString() ?? value.ToString();
        }

        public static string ToJson(this object obj, bool ignoreNull = false)
        {
            if (ignoreNull)
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
            }

            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
        }

        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static string UnMask(this string value)
        {
            return value.HasValue() ? value.Replace("-", "").Replace(",", "").Replace("/", "") : value;
        }
    }
}
