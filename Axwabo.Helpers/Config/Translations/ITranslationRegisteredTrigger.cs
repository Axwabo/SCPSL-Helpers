namespace Axwabo.Helpers.Config.Translations;

/// <summary>
/// An attribute interface to invoke a method when a new translation is registered.
/// </summary>
public interface ITranslationRegisteredTrigger
{

    /// <summary>
    /// The method called when a new translation is registered.
    /// </summary>
    /// <param name="member">The member that was processed.</param>
    void OnProcessed(MemberInfo member);

}
