using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LivingWellMVC.Models {
    public static class Utility {
        public static string GetEnumDescription(this Enum value) {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string GetPhoneNumberWithDash(string value) {
            return FormatPhoneNumber(value, "-");
        }
        public static string GetPhoneNumberWithPeriod(string value) {
            return FormatPhoneNumber(value, ".");
        }
        public static string GetPhoneNumberWithLettersAndSpaces(string value) {
            string formattedValue = value;

            formattedValue = formattedValue.Insert(1, " ").Insert(5, " ").Insert(8, " ").Insert(11, " ");

            return formattedValue;
        }


        private static string FormatPhoneNumber(string value, string separator) {
            int result;
            string formattedValue = "";
            
            formattedValue = value.Trim().Replace("-", string.Empty).Replace(".", string.Empty);

            if (!string.IsNullOrWhiteSpace(formattedValue) && int.TryParse(formattedValue, out result)) {
                formattedValue = formattedValue.Insert(3, separator).Insert(7, separator);
            }

            return formattedValue;
        }
    }

}