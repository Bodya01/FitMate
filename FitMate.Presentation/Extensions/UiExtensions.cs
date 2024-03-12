using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FitMate.Presentation.Extensions
{
    internal static class UiExtensions
    {
        internal static SelectList GetEnumSelectListWithDescriptions<T>(this IHtmlHelper helper) where T : struct, Enum
        {
            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ChangeType(e, Enum.GetUnderlyingType(typeof(T))).ToString(),
                    Text = GetEnumDescription(e)
                }).ToList();

            return new SelectList(values, "Value", "Text");
        }

        internal static string Attr(this IHtmlHelper helper, string name, string value, Func<bool> condition = null)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var render = condition != null && condition();

            return render ?
                new string(string.Format("{0}=\"{1}\"", name, HttpUtility.HtmlAttributeEncode(value)))
                : string.Empty;
        }


        private static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return attribute?.Description ?? value.ToString();
        }
    }
}
