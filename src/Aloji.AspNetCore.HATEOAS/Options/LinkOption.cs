using System;
using System.Net.Http;

namespace Aloji.AspNetCore.HATEOAS.Options
{
    public class LinkOption
    {
        public string Relation { get; private set; }
        public string RouteName { get; private set; }
        public HttpMethod HttpMethod { get; private set; }
        public object Values { get; private set; }

        public LinkOption(string relation, HttpMethod httpMethod, string routeName, object values)
        {
            this.Relation = relation ?? throw new ArgumentNullException(nameof(relation));
            this.HttpMethod = httpMethod ?? throw new ArgumentNullException(nameof(httpMethod));
            this.RouteName = routeName ?? throw new ArgumentNullException(nameof(routeName));
            this.Values = values;
        }
    }
}
