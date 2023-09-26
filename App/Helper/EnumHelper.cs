using System.ComponentModel;

namespace App.Helper;

public static class EnumHelper
{
    public static string GetDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static T GetValue<T>(string description) where T : Enum
    {
        foreach (T value in Enum.GetValues(typeof(T)))
        {
            if (GetDescription(value) == description)
                return value;
        }

        throw new ArgumentException($"Значение с описанием '{description}' не найдено в enum {typeof(T).Name}.");
    }
}