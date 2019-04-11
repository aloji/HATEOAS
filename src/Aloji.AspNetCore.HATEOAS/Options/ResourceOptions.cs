using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Aloji.AspNetCore.HATEOAS.Options
{
    public class ResourceOptions
    {
        readonly Dictionary<Type, Dictionary<string, LinkOption>> links;

        public Func<ActionContext, IUrlHelper> UrlHelperFactory { get; set; }

        public ResourceOptions()
        {
            this.links = new Dictionary<Type, Dictionary<string, LinkOption>>();
        }

        public void AddLink<T>(string relation, string routeName, HttpMethod httpMethod, object values)
        {
            var type = typeof(T);
            if (!this.links.ContainsKey(type))
                this.links[type] = new Dictionary<string, LinkOption>();

            this.links[type][relation] = new LinkOption(relation, httpMethod, routeName, values);
        }

        public IEnumerable<LinkOption> GetLinks(Type type)
        {
            var result = default(IEnumerable<LinkOption>);
            if (this.links.ContainsKey(type))
            {
                result = this.links[type].Values;
            }
            return result;
        }
    }
}
