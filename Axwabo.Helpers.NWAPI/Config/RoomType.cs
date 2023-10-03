using System;

#pragma warning disable CS1591
// disable XML doc warning lol

namespace Axwabo.Helpers.Config;

/// <summary>
/// Temporary enum for storing Room Types until NW decides to add them to the API.
/// </summary>
public enum RoomType
{

    Unknown,

    [RoomName("EZ_CollapsedTunnel")]
    EzCollapsedTunnel,

    [RoomName("LCZ_TCross")]
    LczTCross,

    [RoomName("EZ_Intercom")]
    EzIntercom,

    [RoomName("LCZ_Toilets")]
    LczToilets,

    [RoomName("EZ_PCs_small")]
    EzOfficeSmall,

    [RoomName("HCZ_Curve")]
    HczCurve,

    [RoomName("LCZ_330")]
    Lcz330,

    [RoomName("LCZ_ClassDSpawn")]
    LczClassDSpawn,

    [RoomName("LCZ_Airlock")]
    LczAirlock,

    [RoomName("LCZ_Crossing")]
    LczCrossing,

    [RoomName("HCZ_Room3ar")]
    HczArmory,

    [RoomName("LCZ_173")]
    Lcz173,

    [RoomName("EZ_GateB")]
    EzGateB,

    [RoomName("EZ_Cafeteria")]
    EzCafeteria,

    [RoomName("EZ_PCs")]
    EzOfficeLarge,

    [RoomName("EZ_Smallrooms2")]
    EzSmallrooms2,

    [RoomName("LCZ_Cafe")]
    LczCafe,

    [RoomName("LCZ_Armory")]
    LczArmory,

    [RoomName("HCZ_079")]
    Hcz079,

    [RoomName("LCZ_372")]
    LczGlassroom,

    [RoomName("HCZ_Testroom")]
    HczTestroom,

    [RoomName("HCZ_Room3")]
    HczRoom3,

    [RoomName("PocketWorld")]
    PocketWorld,

    [RoomName("EZ_Curve")]
    EzCurve,

    [RoomName("HCZ_Nuke")]
    HczWarhead,

    [RoomName("LCZ_ChkpA")]
    LczCheckpointA,

    [RoomName("HCZ_Straight")]
    HczStraight,

    [RoomName("LCZ_Straight")]
    LczStraight,

    [RoomName("EZ Part")]
    HczCheckpointToEntranceZone,

    [RoomName("LCZ_Plants")]
    LczGreenhouse,

    [RoomName("EZ_Shelter")]
    EzEvacShelter,

    [RoomName("EZ_upstairs")]
    EzOfficeStoried,

    [RoomName("HCZ_106")]
    Hcz106,

    [RoomName("EZ_Endoof")]
    EzRedroom,

    [RoomName("HCZ Part")]
    HczPart,

    [RoomName("HCZ_939")]
    Hcz939,

    [RoomName("EZ_Straight")]
    EzStraight,

    [RoomName("EZ_GateA")]
    EzGateA,

    [RoomName("LCZ_914")]
    Lcz914,

    [RoomName("HCZ_ChkpB")]
    HczCheckpointB,

    [RoomName("HCZ_049")]
    Hcz049,

    [RoomName("HCZ_Hid")]
    HczHid,

    [RoomName("Outside")]
    Outside,

    [RoomName("HCZ_Tesla")]
    HczTesla,

    [RoomName("HCZ_457")]
    Hcz096,

    [RoomName("LCZ_ChkpB")]
    LczCheckpointB,

    [RoomName("HCZ_Servers")]
    HczServers,

    [RoomName("HCZ_Crossing")]
    HczCrossing,

    [RoomName("EZ_ThreeWay")]
    EzThreeWay,

    [Obsolete($"Use {nameof(EzOfficeSmall)} instead")]
    [RoomName("EZ_PCs_small")]
    EzPcsSmall,

    [Obsolete($"Use {nameof(HczArmory)} instead")]
    [RoomName("HCZ_Room3ar")]
    HczRoom3Ar,

    [Obsolete($"Use {nameof(EzOfficeLarge)} instead")]
    [RoomName("EZ_PCs")]
    EzPcs,

    [Obsolete($"Use {nameof(LczGlassroom)} instead")]
    [RoomName("LCZ_372")]
    Lcz372,

    [Obsolete($"Use {nameof(HczWarhead)} instead")]
    [RoomName("HCZ_Nuke")]
    HczNuke,

    [Obsolete($"Use {nameof(LczCheckpointA)} instead")]
    [RoomName("LCZ_ChkpA")]
    LczChkpA,

    [RoomName("HCZ_ChkpA")]
    HczCheckpointA,

    [Obsolete($"Use {nameof(LczCheckpointB)} instead")]
    [RoomName("LCZ_ChkpB")]
    LczChkpB,

    [Obsolete($"Use {nameof(HczCheckpointB)} instead")]
    [RoomName("HCZ_ChkpB")]
    HczChkpB,

    [Obsolete($"Use {nameof(HczCheckpointToEntranceZone)} instead")]
    [RoomName("EZ Part")]
    EzPart,

    [Obsolete($"Use {nameof(LczGreenhouse)} instead")]
    [RoomName("LCZ_Plants")]
    LczPlants,

    [Obsolete($"Use {nameof(EzEvacShelter)} instead")]
    [RoomName("EZ_Shelter")]
    EzShelter,

    [Obsolete($"Use {nameof(EzOfficeStoried)} instead")]
    [RoomName("EZ_upstairs")]
    EzUpstairs,

    [Obsolete($"Use {nameof(EzRedroom)} instead")]
    [RoomName("EZ_Endoof")]
    EzEndoof,

    [Obsolete($"Use {nameof(Hcz096)} instead")]
    [RoomName("HCZ_457")]
    Hcz457

}
