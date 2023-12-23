using System.Reflection;
using Lingualizr.Configuration;

namespace Lingualizr;

/// <summary>
/// Contains extension methods for humanizing Enums
/// </summary>
public static class EnumHumanizeExtensions
{
    private const string DisplayAttributeTypeName = "System.ComponentModel.DataAnnotations.DisplayAttribute";
    private const string DisplayAttributeGetDescriptionMethodName = "GetDescription";
    private const string DisplayAttributeGetNameMethodName = "GetName";

    private static readonly Func<PropertyInfo, bool> _stringTypedProperty = p => p.PropertyType == typeof(string);

    /// <summary>
    /// Turns an enum member into a human readable string with the provided casing; e.g. AnonymousUser with Title casing -> Anonymous User. It also honors DescriptionAttribute data annotation
    /// </summary>
    /// <param name="input">The enum member to be humanized</param>
    /// <param name="casing">The casing to use for humanizing the enum member</param>
    /// <returns></returns>
    public static string Humanize(this Enum input, LetterCasing casing)
    {
        string humanizedEnum = Humanize(input);

        return humanizedEnum.ApplyCase(casing);
    }

    /// <summary>
    /// Turns an enum member into a human readable string; e.g. AnonymousUser -> Anonymous user. It also honors DescriptionAttribute data annotation
    /// </summary>
    /// <param name="input">The enum member to be humanized</param>
    /// <returns></returns>
    public static string Humanize(this Enum input)
    {
        Type enumType = input.GetType();
        TypeInfo enumTypeInfo = enumType.GetTypeInfo();

        if (IsBitFieldEnum(enumTypeInfo) && !Enum.IsDefined(enumType, input))
        {
            return Enum.GetValues(enumType).Cast<Enum>().Where(e => e.CompareTo(Convert.ChangeType(Enum.ToObject(enumType, 0), enumType)) != 0).Where(input.HasFlag).Select(e => e.Humanize()).Humanize();
        }

        string caseName = input.ToString();
        FieldInfo? memInfo = enumTypeInfo.GetDeclaredField(caseName);

        if (memInfo != null)
        {
            string? customDescription = GetCustomDescription(memInfo);

            if (customDescription != null)
            {
                return customDescription;
            }
        }

        return caseName.Humanize();
    }

    /// <summary>
    /// Checks whether the given enum is to be used as a bit field type.
    /// </summary>
    /// <param name="typeInfo"></param>
    /// <returns>True if the given enum is a bit field enum, false otherwise.</returns>
    private static bool IsBitFieldEnum(TypeInfo typeInfo)
    {
        return typeInfo.GetCustomAttribute(typeof(FlagsAttribute)) != null;
    }

    // I had to add this method because PCL doesn't have DescriptionAttribute & I didn't want two versions of the code & thus the reflection
    private static string? GetCustomDescription(MemberInfo memberInfo)
    {
        object[] attrs = memberInfo.GetCustomAttributes(true);

        foreach (object? attr in attrs)
        {
            Type attrType = attr.GetType();
            if (attrType.FullName == DisplayAttributeTypeName)
            {
                MethodInfo? methodGetDescription = attrType.GetRuntimeMethod(DisplayAttributeGetDescriptionMethodName, Array.Empty<Type>());
                if (methodGetDescription != null)
                {
                    object? executedMethod = methodGetDescription.Invoke(attr, Array.Empty<object>());
                    if (executedMethod != null)
                    {
                        return executedMethod.ToString();
                    }
                }

                MethodInfo? methodGetName = attrType.GetRuntimeMethod(DisplayAttributeGetNameMethodName, Array.Empty<Type>());
                if (methodGetName != null)
                {
                    object? executedMethod = methodGetName.Invoke(attr, Array.Empty<object>());
                    if (executedMethod != null)
                    {
                        return executedMethod.ToString();
                    }
                }

                return null;
            }

            PropertyInfo? descriptionProperty = attrType.GetRuntimeProperties().Where(_stringTypedProperty).FirstOrDefault(Configurator.EnumDescriptionPropertyLocator);
            if (descriptionProperty != null)
            {
                return descriptionProperty.GetValue(attr, null)?.ToString();
            }
        }

        return null;
    }
}
