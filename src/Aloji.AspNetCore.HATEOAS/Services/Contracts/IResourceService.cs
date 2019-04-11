using Microsoft.AspNetCore.Mvc;

namespace Aloji.AspNetCore.HATEOAS.Services.Contracts
{
    public interface IResourceService
    {
        object AddLinks(object value, ActionContext context);
    }
}
