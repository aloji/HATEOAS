using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Aloji.AspNetCore.HATEOAS.Options
{
    public class ResourceOptions
    {
        readonly Dictionary<Type, Dictionary<string, ILinkOption>> links;
        public ResourceOptions()
        {
            this.links = new Dictionary<Type, Dictionary<string, ILinkOption>>();
        }

        public void AddLink<T>(string relation, string routeName, HttpMethod httpMethod, 
            Func<T, object> values)
        {
            var type = typeof(T);
            if (!this.links.ContainsKey(type))
                this.links[type] = new Dictionary<string, ILinkOption>();

            this.links[type][relation] = 
                new LinkOption<T>(relation, httpMethod, routeName, values);
        }

        public IEnumerable<ILinkOption> GetLinks(Type type)
        {
            var result = default(IEnumerable<ILinkOption>);
            if (this.links.ContainsKey(type))
            {
                result = this.links[type].Values;
            }
            return result;
        }
    }
}