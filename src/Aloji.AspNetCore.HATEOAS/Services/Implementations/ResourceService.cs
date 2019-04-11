using Aloji.AspNetCore.HATEOAS.Extensions;
using Aloji.AspNetCore.HATEOAS.Options;
using Aloji.AspNetCore.HATEOAS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aloji.AspNetCore.HATEOAS.Services.Implementations
{
    public class ResourceService : IResourceService
    {
        readonly IOptions<ResourceOptions> options;

        public ResourceService(IOptions<ResourceOptions> options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public object AddLinks(object value, ActionContext context)
        {
            if (value == null)
                return null;
            else
            {
                var urlHelper = this.options.Value.UrlHelperFactory(context);
                if (value is IEnumerable)
                {
                    var result = new List<object>();
                    foreach (var item in (IEnumerable)value)
                    {
                        result.Add(this.Add(item, urlHelper));
                    }
                    return result;
                }
                else
                {
                    var result = this.Add(value, urlHelper);
                    return result;
                }
            }
        }

        private object Add(object obj, IUrlHelper urlHelper)
        {
            var result = obj;
            var links = this.options.Value.GetLinks(obj.GetType());
            if (links != null)
            {
                foreach (var link in links)
                {
                    result = result.WithLink(
                        rel: link.Relation,
                        method: link.HttpMethod,
                        href: urlHelper.Link(link.RouteName, link.Values));
                }
            }
            return result;
        }
    }
}
