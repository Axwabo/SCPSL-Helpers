// ReSharper disable SuspiciousTypeConversion.Global

namespace Axwabo.Helpers.Config.Translations;

internal static class AttributeHandler
{

    #region Register non-static

    public static int RegisterAttributeProperty<T>(object attribute, object containingClass, PropertyInfo property) where T : Enum
    {
        if (containingClass == null || property == null)
            return 0;
        var type = typeof(T);
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations<T>(property.GetValue(containingClass));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(property.PropertyType);
            case TranslationAttribute translation when property.PropertyType == typeof(string) && translation.EnumType == type:
                var key = (T) translation.Value;
                var value = (string) property.GetValue(containingClass);
                key.RegisterTranslation(value);
                if (translation is ITranslationRegisteredTrigger trigger)
                    trigger.OnProcessed(property);
                TranslationHelper.InvokeOnTranslationRegistered(translation, property, key, value);
                return 1;
            default:
                return 0;
        }
    }

    public static int RegisterAttributeField<T>(object attribute, object containingObject, FieldInfo field) where T : Enum
    {
        if (containingObject == null || field == null)
            return 0;
        var type = typeof(T);
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations<T>(field.GetValue(containingObject));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(field.FieldType);
            case TranslationAttribute translation when field.FieldType == typeof(string) && translation.EnumType == type:
                var key = (T) translation.Value;
                var value = (string) field.GetValue(containingObject);
                key.RegisterTranslation(value);
                if (translation is ITranslationRegisteredTrigger trigger)
                    trigger.OnProcessed(field);
                TranslationHelper.InvokeOnTranslationRegistered(translation, field, key, value);
                return 1;
            default:
                return 0;
        }
    }

    public static int RegisterAttributeProperty(object attribute, object containingClass, PropertyInfo property)
    {
        if (containingClass == null || property == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(property.GetValue(containingClass));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(property.PropertyType);
            case TranslationAttribute translation when property.PropertyType == typeof(string):
                var value = property.GetValue(containingClass);
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("RegisterTranslation", translation.Value, value);
                if (translation is ITranslationRegisteredTrigger trigger)
                    trigger.OnProcessed(property);
                TranslationHelper.InvokeOnTranslationRegistered(translation, property, translation.Value, (string) value);
                return 1;
            default:
                return 0;
        }
    }

    public static int RegisterAttributeField(object attribute, object containingObject, FieldInfo field)
    {
        if (containingObject == null || field == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(field.GetValue(containingObject));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(field.FieldType);
            case TranslationAttribute translation when field.FieldType == typeof(string):
                var value = field.GetValue(containingObject);
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("RegisterTranslation", translation.Value, value);
                if (translation is ITranslationRegisteredTrigger trigger)
                    trigger.OnProcessed(field);
                TranslationHelper.InvokeOnTranslationRegistered(translation, field, translation.Value, (string) value);
                return 1;
            default:
                return 0;
        }
    }

    #endregion

    #region Register static

    public static int RegisterAttributePropertyStatic(object attribute, PropertyInfo property)
    {
        if (property == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(property.GetValue(null));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(property.PropertyType);
            case TranslationAttribute translation when property.PropertyType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("RegisterTranslation", translation.Value, property.GetValue(null));
                return 1;
            default:
                return 0;
        }
    }

    public static int RegisterAttributeFieldStatic(object attribute, FieldInfo field)
    {
        if (field == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(field.GetValue(null));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(field.FieldType);
            case TranslationAttribute translation when field.FieldType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("RegisterTranslation", translation.Value, field.GetValue(null));
                return 1;
            default:
                return 0;
        }
    }

    #endregion

    #region Unregister non-static

    public static int UnregisterAttributeProperty(object attribute, object containingClass, PropertyInfo property)
    {
        if (containingClass == null || property == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(property.GetValue(containingClass));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(property.PropertyType);
            case TranslationAttribute translation when property.PropertyType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("UnregisterTranslation", translation.Value);
                return 1;
            default:
                return 0;
        }
    }

    public static int UnregisterAttributeField(object attribute, object containingClass, FieldInfo field)
    {
        if (containingClass == null || field == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(field.GetValue(containingClass));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(field.FieldType);
            case TranslationAttribute translation when field.FieldType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("UnregisterTranslation", translation.Value);
                return 1;
            default:
                return 0;
        }
    }

    #endregion

    #region Unregister static

    public static int UnregisterAttributePropertyStatic(object attribute, PropertyInfo property)
    {
        if (property == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(property.GetValue(null));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(property.PropertyType);
            case TranslationAttribute translation when property.PropertyType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("UnregisterTranslation", translation.Value);
                return 1;
            default:
                return 0;
        }
    }

    public static int UnregisterAttributeFieldStatic(object attribute, FieldInfo field)
    {
        if (field == null)
            return 0;
        switch (attribute)
        {
            case RegisterNestedTranslationsAttribute:
                return TranslationHelper.RegisterAllTranslations(field.GetValue(null));
            case RegisterStaticTranslationsInTypeAttribute:
                return TranslationHelper.RegisterAllStaticTranslations(field.FieldType);
            case TranslationAttribute translation when field.FieldType == typeof(string):
                TranslationHelper.TranslationRegistryType.MakeGenericType(translation.EnumType).StaticCall("UnregisterTranslation", translation.Value);
                return 1;
            default:
                return 0;
        }
    }

    #endregion

}
