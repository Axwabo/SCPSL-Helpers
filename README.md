# Axwabo.Helpers

Helper library for working with SCP:SL plugins.

Includes various methods to make workflow easier and to shorten code.

Makes using Reflection and patching methods with Harmony simpler.

# Installation

> [!NOTE]
> This is a dependency, not a plugin.

> [!IMPORTANT]
> **EXILED no longer receives dedicated updates.** 
> This library is not designed to be specifically compatible with 3rd-party plugin frameworks. Despite this, most if not all functionality should work just fine.
> 
> With the upcoming release of LabAPI, the Northwood Plugin API version will only be supported until NWAPI gets deprecated.

## Northwood Plugin API

1. Download the **Axwabo.Helpers-nw.dll** from the [latest release](https://github.com/Axwabo/SCPSL-Helpers/releases/)
2. Put the DLL into the PluginAPI dependencies folder
   - Windows: `%appdata%/SCP Secret Laboratory/PluginAPI/port/dependencies`
   - Linux: `.config/SCP SecretLaboratory/PluginAPI/port/dependencies`
3. Any plugins using this library should work after a server restart

# Examples

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
