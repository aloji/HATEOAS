using Aloji.AspNetCore.HATEOAS.Extensions;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace HATEOAS.UnitTest
{
    public class WithLinkShould
    {
        [Fact]
        public void ReturnNullIfTheObjIsNull()
        {
            var obj = default(object);
            var rel = default(string);
            var method = HttpMethod.Get;
            var href = default(string);

            var actual = obj.WithLink(rel, method, href);

            Assert.Null(actual);
        }

        [Fact]
        public void ReturnObjectWithLink()
        {
            var obj = new
            {
                Id = 3
            };

            var rel = "self";
            var method = HttpMethod.Get;
            var href = "https://github.com/aloji";

            var actual = obj.WithLink(rel, method, href);

            Assert.NotNull(actual);
            Assert.IsType<ExpandoObject>(actual);

            var properties = (IDictionary<string, object>)actual;

            Assert.Contains("Id", properties);
            Assert.Contains("_links", properties);

            Assert.IsType<int>(properties["Id"]);
            Assert.Equal(obj.Id, (int)properties["Id"]);

            Assert.IsType<List<object>>(properties["_links"]);

            var links = (List<object>)properties["_links"];
            Assert.Single(links);

            var firstLink = links.First();
            var linkProperties = firstLink.GetType().GetProperties();
            Assert.Equal(3, linkProperties.Count());
            Assert.Contains(linkProperties, x => x.Name == "rel");
            Assert.Contains(linkProperties, x => x.Name == "method");
            Assert.Contains(linkProperties, x => x.Name == "href");

            Assert.Equal(rel, linkProperties.First(x => x.Name == "rel").GetValue(firstLink));
            Assert.Equal(method.ToString(), linkProperties.First(x => x.Name == "method").GetValue(firstLink));
            Assert.Equal(href, linkProperties.First(x => x.Name == "href").GetValue(firstLink));
        }
    }
}
