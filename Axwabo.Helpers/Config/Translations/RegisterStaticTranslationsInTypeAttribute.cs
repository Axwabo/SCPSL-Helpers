using System;

namespace Axwabo.Helpers.Config.Translations {

    /// <summary>
    /// Determines that the static properties or fields in the <see cref="Type"/> this attribute is applied to should also be checked for translations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RegisterStaticTranslationsInTypeAttribute : Attribute {

    }

}
