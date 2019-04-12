using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
namespace Aloji.AspNetCore.HATEOAS.Options
{
    public interface ILinkOption
    {
        string Relation { get; }
        string RouteName { get; }
        HttpMethod HttpMethod { get; }
        object GetValues(object obj);
    }
}
