using System;

namespace Axwabo.Helpers.Config;

internal sealed class RoomNameAttribute : Attribute
{

    public string Name { get; }

    public RoomNameAttribute(string name) => Name = name;

}
