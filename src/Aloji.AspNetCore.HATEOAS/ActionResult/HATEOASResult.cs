using Aloji.AspNetCore.HATEOAS.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public class HATEOASResult : IActionResult
    {
        private readonly object value;
        private readonly Func<object, IActionResult> func;

        public HATEOASResult(object value, Func<object, IActionResult> func)
        {
            this.value = value;
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var resourceService = GetService<IResourceService>();

            var valueWithLinks = resourceService.AddLinks(value, context);
            await this.func(valueWithLinks).ExecuteResultAsync(context);

            T GetService<T>()
            {
                var result = (T)context.HttpContext.RequestServices.GetService(typeof(T));
                return result;
            }
        }
    }
}
