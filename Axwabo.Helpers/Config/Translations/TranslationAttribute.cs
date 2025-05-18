namespace Axwabo.Helpers.Config.Translations;

/// <summary>
/// An attribute to mark fields and properties as a translation value. Must be inherited with a specific enum type to use.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public abstract class TranslationAttribute : Attribute
{

    /// <summary>The key to use for the translation.</summary>
    public readonly Enum Value;

    /// <summary>The cached type of the enum.</summary>
    public readonly Type EnumType;

    /// <summary>
    /// Creates a new instance of the attribute.
    /// </summary>
    /// <param name="value">The key to use.</param>
    protected TranslationAttribute(Enum value)
    {
        Value = value;
        EnumType = Value.GetType();
    }

}
