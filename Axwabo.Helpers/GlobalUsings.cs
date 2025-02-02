global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using System.Reflection.Emit;
global using HarmonyLib;

#if NWAPI
global using PluginAPI.Core;
global using Logger = PluginAPI.Core.Log;
#else
global using LabApi.Features.Wrappers;
global using Logger = LabApi.Features.Console.Logger;
#endif
global using InventorySystem.Items;
global using UnityEngine;
