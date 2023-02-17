using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers {

    /// <summary>
    /// Helper methods for <see cref="Color"/> structs.
    /// </summary>
    public static class ColorHelper {

        /// <summary>
        /// Converts a HTML color string to a <see cref="Color"/> object.
        /// </summary>
        /// <param name="html">The string to parse.</param>
        /// <returns>A color object matching the code.</returns>
        public static Color ParseColor(string html) => ColorUtility.TryParseHtmlString(html, out var color) ? color : Color.black;

        /// <summary>A map of color codes that can be used in badges and <see cref="Player.CustomInfo">player info</see>.</summary>
        public static readonly IDictionary<string, string> NorthwoodApprovedColorCodes = new ReadOnlyDictionary<string, string>(new Dictionary<string, string> {
            {"pink", "#FF96DE"},
            {"red", "#C50000"},
            {"white", "#FFFFFF"},
            {"brown", "#944710"},
            {"silver", "#A0A0A0"},
            {"light_green", "#32CD32"},
            {"crimson", "#DC143C"},
            {"cyan", "#00B7EB"},
            {"aqua", "#00FFFF"},
            {"deep_pink", "#FF1493"},
            {"tomato", "#FF6448"},
            {"yellow", "#FAFF86"},
            {"magenta", "#FF0090"},
            {"blue_green", "#4DFFB8"},
            {"orange", "#FF9966"},
            {"lime", "#BFFF00"},
            {"green", "#228B22"},
            {"emerald", "#50C878"},
            {"carmine", "#960018"},
            {"nickel", "#727472"},
            {"mint", "#98FB98"},
            {"army_green", "#4B5320"},
            {"pumpkin", "#EE7600"},
            {"black", "#000000"}
        });

        /// <summary>
        /// Gets the Northwood approved color for a given color name.
        /// </summary>
        /// <param name="colorName">The name of the color.</param>
        /// <returns>The Northwood approved color if it was found; otherwise, <see cref="Color.black"/>.</returns>
        public static Color GetNorthwoodApprovedColor(string colorName) =>
            NorthwoodApprovedColorCodes.TryGetValue(colorName, out var colorCode) ? ParseColor(colorCode) : Color.black;

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
        public static string GetClosestNorthwoodColor(this Color color) =>
            NorthwoodApprovedColorCodes.TryGetValue(GetClosestNorthwoodColorName(color), out var c) ? c : "#FFFFFF";

        /// <summary>
        /// Gets name of the closest color approved by Northwood based on HSV values.
        /// </summary>
        /// <param name="color">The color code to compare.</param>
        /// <returns>A Northwood approved color name.</returns>
        public static string GetClosestNorthwoodColorName(string color) => GetClosestNorthwoodColorName(ParseColor(color));

        /// <summary>
        /// Gets name of the closest color approved by Northwood based on HSV values.
        /// </summary>
        /// <param name="color">The color to compare.</param>
        /// <returns>A Northwood approved color name.</returns>
        public static string GetClosestNorthwoodColorName(this Color color) {
            var hsv = color.Hsv();
            var value = hsv[0] * 36000 + hsv[1] * 100 + hsv[2];
            var closest = NorthwoodApprovedColorCodes
                .ToDictionary(k => k.Key, v => ParseColor(v.Value).Hsv())
                .OrderBy(e => {
                    var colorHsv = e.Value;
                    return Mathf.Abs(colorHsv[0] * 36000 + colorHsv[1] * 100 + colorHsv[2] - value);
                }).FirstOrDefault();
            return closest.Key;
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

        /// <summary>
        /// Converts a <see cref="Color"/> to a HEX string.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="includeHash">If the string should be prefixed with a hash ('#')</param>
        /// <param name="includeAlpha">If the alpha channel value should be added to the string.</param>
        /// <returns>The HEX code.</returns>
        public static string ToHex(this Color color, bool includeHash = true, bool includeAlpha = true) => $"{(includeHash ? "#" : "")}{(includeAlpha ? ColorUtility.ToHtmlStringRGBA(color) : ColorUtility.ToHtmlStringRGB(color))}";

        /// <summary>
        /// Converts a <see cref="Color32"/> to a HEX string.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="includeHash">If the string should be prefixed with a hash ('#')</param>
        /// <param name="includeAlpha">If the alpha channel value should be added to the string.</param>
        /// <returns>The HEX code.</returns>
        public static string ToHex(this Color32 color, bool includeHash = true, bool includeAlpha = true) => ToHex((Color) color, includeHash, includeAlpha);

    }

}
