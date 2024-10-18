using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CShapr.API.Helpers
{
	public class CompressResponseAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			context.HttpContext.Request.Headers["Accept-Encoding"] = "gzip";
			base.OnActionExecuting(context);
		}
	}
}
