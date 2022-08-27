using System;

namespace Axwabo.Helpers.Config.Translations {

    /// <summary>
    /// Determines that the properties or fields in the object this attribute is applied to should also be checked for translations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RegisterNestedTranslationsAttribute : Attribute {

    }

}
