using System;
using System.Resources;
using System.Collections;
using System.Threading;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Framework.Helpers
{
    public static class AttributeHelper
    {
        public static string GetLocalizedDisplayName<T>(object enumValue)
        {
            Type enumType = typeof(T);

            var memberInfos = enumType.GetMember(enumValue.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (valueAttributes == null || valueAttributes.Length == 0)
                return enumValue.ToString();

            var displayAttribute = ((DisplayAttribute)valueAttributes[0]);
            ResourceManager resourceManager = new ResourceManager(displayAttribute.ResourceType);
            var entry = resourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true)
                                       .OfType<DictionaryEntry>()
                                       .FirstOrDefault(p => p.Key.ToString() == displayAttribute.Name);

            return entry.Value.ToString();
        }
    }
}

