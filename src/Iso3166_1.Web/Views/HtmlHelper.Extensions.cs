using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Iso3166_1.Crowdsource_it.org.Web.Views
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString MenuEntry(this HtmlHelper helper, string linkText, string actionName, string controllerName)
		{
			string currentController = getValue(helper, "controller");
			string currentAction = getValue(helper, "action");
			bool isCurrent = controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase) &&
				actionName.Equals(currentAction, StringComparison.OrdinalIgnoreCase);
			var li = new TagBuilder("li")
			{
				InnerHtml = helper.ActionLink(linkText, actionName, controllerName).ToString()
			};
			if (isCurrent)
			{
				li.AddCssClass("active");
			}
			
			return new MvcHtmlString(li.ToString(TagRenderMode.Normal));
		}

		private static string getValue(HtmlHelper helper, string key)
		{
			return helper.ViewContext.Controller.ValueProvider.GetValue(key).RawValue.ToString();
		}
	}
}