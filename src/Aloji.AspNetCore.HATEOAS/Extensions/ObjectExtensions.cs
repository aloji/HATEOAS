using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;

namespace Aloji.AspNetCore.HATEOAS.Extensions
{
    public static class ObjectExtensions
    {
        public static object WithLink(this object obj, string rel, HttpMethod method, string href)
        {
            if (obj == null)
                return null;

            if (!(obj is ExpandoObject expando))
            {
                expando = new ExpandoObject();
                var dictionary = (IDictionary<string, object>)expando;
                foreach (var property in obj.GetType().GetProperties())
                    dictionary.Add(property.Name, property.GetValue(obj));
            }
            return AddLink();

            object AddLink()
            {
                var dictionary = (IDictionary<string, object>)expando;
                if (!dictionary.ContainsKey("_links"))
                    dictionary["_links"] = new List<object>();

                ((List<object>)dictionary["_links"]).Add(new
                {
                    rel = rel,
                    href = href,
                    method = method
                });

                return expando;
            }
        }
    }
}
