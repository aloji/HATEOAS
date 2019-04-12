using System;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {
        public static HATEOASResult HATEOASResult(this ControllerBase controllerBase,
            object value, Func<object, IActionResult> func)
        {
            return new HATEOASResult(value, func);
        }
    }
}
