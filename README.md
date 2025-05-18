# Axwabo.Helpers

Helper library for working with SCP:SL plugins.

Includes various methods to make workflow easier and to shorten code.

Makes using Reflection and patching methods with Harmony simpler.

# Installation

> [!NOTE]
> This is a dependency, not a plugin.

1. Download the **Axwabo.Helpers.dll** from the [latest release](https://github.com/Axwabo/SCPSL-Helpers/releases/)
2. Put the DLL into the dependencies folder
    - Windows: `%appdata%\SCP Secret Laboratory\LabAPI\<port>\dependencies`
    - Linux: `.config/SCP SecretLaboratory/PluginAPI/<port>/dependencies`
3. Any plugins using this library should work after a server restart

# Examples

## Player Info

> [!TIP]
> Use-cases include (but are not limited to):
> - storing the state to revert to later
> - swapping players' roles

Call the `GetInfoWithRole` extension method (from `Axwabo.Helpers.PlayerInfo.PlayerInfoExtensions`) to take a snapshot.
This includes information about the player's current role, items, ammo and effects.

Invoke `IPlayerInfoWithRole::SetClassAndApplyInfo` to apply the snapshot onto any player.

### Customization

You can extend the player info by registering custom obtainers for roles or items.

Custom info obtainers take priority over built-in ones.

To add a role info obtainer, call `PlayerInfoBase::RegisterCustomObtainer` - if the `roleSetter` is not specified, `SetClass` will throw an exception.

To add an item info obtainer, call `ItemInfoBase::RegisterCustomObtainer` - the `give` delegate isn't currently used because I'm lazy.

> [!IMPORTANT]
> Obtainer registration methods return an ID. Make sure to unregister them when your plugin is being disabled.

## Harmony Patch

You can _static import_ the **Axwabo.Helpers.Harmony.InstructionHelper** class to generate CodeInstructions faster.

CodeInstruction examples:

| Property/Method    | Corresponding OpCode |
|--------------------|----------------------|
| This               | ldarg.0              |
| Return             | ret                  |
| Duplicate          | dup                  |
| Pop                | pop                  |
| Ldloc(index)       | ldloc index          |
| Call(type, method) | call/callvirt        |

The **Call** method automatically sets the opcode to **callvirt** if the specified method is virtual.

## Reflection

Extension methods provide a quick way to access private fields and methods.

To get a field's value, simply use the `Get` extension method on an object.

Sample code:

```csharp
Stopwatch stopwatch = Stopwatch.StartNew();
long rawTicks = stopwatch.Get<long>("elapsed"); // gets the value of the 'elapsed' field
```
