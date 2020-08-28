using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Sinba.BusinessModel.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceKey;
        private readonly ResourceManager resource;
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            resource = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = resource.GetString(resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", resourceKey)
                    : displayName;
            }
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }
    }
}
