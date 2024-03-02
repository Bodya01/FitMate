using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.Linq;

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


        private static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return attribute?.Description ?? value.ToString();
        }
    }
}
