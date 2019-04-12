using Aloji.AspNetCore.HATEOAS.Extensions;
using Aloji.AspNetCore.HATEOAS.Options;
using Aloji.AspNetCore.HATEOAS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aloji.AspNetCore.HATEOAS.Services.Implementations
{
    public class ResourceService : IResourceService
    {
        readonly IOptions<ResourceOptions> options;
        readonly LinkGenerator linkGenerator;

        public ResourceService(IOptions<ResourceOptions> options, LinkGenerator linkGenerator)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
        }

        public object AddLinks(object value, ActionContext context)
        {
            if (value == null)
                return null;
            else
            {
                if (value is IEnumerable)
                {
                    var result = new List<object>();
                    foreach (var item in (IEnumerable)value)
                    {
                        result.Add(this.Add(item));
                    }
                    return result;
                }
                else
                {
                    var result = this.Add(value);
                    return result;
                }
            }
        }

        private object Add(object obj)
        {
            var result = obj;
            var links = this.options.Value.GetLinks(obj.GetType());
            if (links != null)
            {
                foreach (var link in links)
                {
                    var href = this.linkGenerator.GetUriByRouteValues(
                            httpContext: null,
                            routeName: link.RouteName,
                            values: link.GetValues(obj));

                    result = result.WithLink(
                        rel: link.Relation,
                        method: link.HttpMethod,
                        href: href);
                }
            }
            return result;
        }
    }
}
