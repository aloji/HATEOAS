using System;
using System.Net.Http;

namespace Aloji.AspNetCore.HATEOAS.Options
{
    public class LinkOption<T> : ILinkOption
    {
        public string Relation { get; private set; }
        public string RouteName { get; private set; }
        public HttpMethod HttpMethod { get; private set; }

        public Func<T, object> valuesFunc;

        public LinkOption(string relation, HttpMethod httpMethod,
            string routeName, Func<T, object> valuesFunc)
        {
            this.Relation = relation ?? throw new ArgumentNullException(nameof(relation));
            this.HttpMethod = httpMethod ?? throw new ArgumentNullException(nameof(httpMethod));
            this.RouteName = routeName ?? throw new ArgumentNullException(nameof(routeName));
            this.valuesFunc = valuesFunc;
        }

        public object GetValues(object obj)
        {
            if (obj == null || typeof(T) != obj.GetType())
                return null;

            var result = valuesFunc((T)obj);
            return result;
        }
    }
}