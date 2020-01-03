using Newtonsoft.Json;

namespace Core.Tests.Extensions
{
    public static class ObjectExtension
    {
        public static T DeepClone<T>(this T source) where T: class
        {
            return JsonConvert.DeserializeObject<T>(source.ToJson());
        }
        
        public static string ToJson<T>(this T source) where T: class
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}