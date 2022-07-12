using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers {

    /// <summary>
    /// Helper methods for <see cref="Color"/> objects.
    /// </summary>
    public static class ColorHelper {

        /// <summary>
        /// Converts a HTML color string to a <see cref="Color"/> object.
        /// </summary>
        /// <param name="html">The string to parse.</param>
        /// <returns>A color object matching the code.</returns>
        public static Color ParseColor(string html) => ColorUtility.TryParseHtmlString(html, out var color) ? color : Color.black;

        /// <summary>
        /// A map of color codes that can be used in badges and <see cref="Player.CustomInfo">player info</see>.
        /// </summary>
        public static readonly IDictionary<string, string> NorthwoodApprovedColorCodes = new ReadOnlyDictionary<string, string>(new Dictionary<string, string> {
            {"Pink", "#FF96DE"},
            {"Red", "#C50000"},
            {"Brown", "#944710"},
            {"Silver", "#A0A0A0"},
            {"LightGreen", "#32CD32"},
            {"Crimson", "#DC143C"},
            {"Cyan", "#00B7EB"},
            {"Aqua", "#00FFFF"},
            {"DeepPink", "#FF1493"},
            {"Tomato", "#FF6448"},
            {"Yellow", "#FAFF86"},
            {"Magenta", "#FF0090"},
            {"BlueGreen", "#4DFFB8"},
            {"Orange", "#FF9966"},
            {"Lime", "#BFFF00"},
            {"Green", "#228B22"},
            {"Emerald", "#50C878"},
            {"Carmine", "#960018"},
            {"Nickel", "#727472"},
            {"Mint", "#98FB98"},
            {"ArmyGreen", "#4B5320"},
            {"Pumpkin", "#EE7600"},
            {"Black", "#000000"},
            {"White", "#FFFFFF"}
        });

        /// <summary>
        /// Gets the closest color approved by Northwood based on HSV values.
        /// </summary>
        /// <param name="color">The color code to compare.</param>
        /// <returns>A Northwood approved color code.</returns>
        public static string GetClosestNorthwoodColor(string color) => GetClosestNorthwoodColor(ParseColor(color));

        /// <summary>
        /// Gets the closest color approved by Northwood based on HSV values.
        /// </summary>
        /// <param name="color">The color to compare.</param>
        /// <returns>A Northwood approved color code.</returns>
        public static string GetClosestNorthwoodColor(this Color color) {
            var hsv = color.Hsv();
            var value = hsv[0] * 36000 + hsv[1] * 100 + hsv[2];
            var closest = NorthwoodApprovedColorCodes
                .ToDictionary(k => k.Key, v => ParseColor(v.Value).Hsv())
                .OrderBy(e => {
                    var colorHsv = e.Value;
                    return value - (colorHsv[0] * 36000 + colorHsv[1] * 100 + colorHsv[2]);
                }).FirstOrDefault();
            return NorthwoodApprovedColorCodes.TryGetValue(closest.Key, out var c) ? c : "#FFFFFF";
        }

        /// <summary>
        /// Gets the HSV values of a color as a float array.
        /// </summary>
        /// <param name="color">The color convert.</param>
        /// <returns>An array containing the HSV values.</returns>
        public static float[] Hsv(this Color color) {
            Color.RGBToHSV(color, out var h, out var s, out var v);
            return new[] {h, s, v};
        }

    }

}
