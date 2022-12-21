using System;
using UnityEngine;

namespace Axwabo.Helpers {

    /// <summary>
    /// Specifies the alignment of text.
    /// </summary>
    public enum TextAlignment {

        /// <summary>The text is aligned to the left.</summary>
        Left,

        /// <summary>The text is centered.</summary>
        Center,

        /// <summary>The text is aligned to the right.</summary>
        Right

    }

    /// <summary>
    /// Helps replace hardcoded rich text tags with more readable methods.
    /// </summary>
    public static class RichTextHelper {

        /// <summary>
        /// Converts a <see cref="TextAlignment"/> to a Unity-friendly string.
        /// </summary>
        /// <param name="alignment">The alignment to convert.</param>
        /// <returns>The Unity-friendly string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="TextAlignment"/> value is unknown.</exception>
        public static string AsString(this TextAlignment alignment) => alignment switch {
            TextAlignment.Left => "left",
            TextAlignment.Center => "center",
            TextAlignment.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };

        /// <summary>
        /// Makes the text bold.
        /// </summary>
        /// <param name="text">The text to make bold.</param>
        /// <returns>The bold text.</returns>
        public static string Bold(this string text) => $"<b>{text}</b>";

        /// <summary>
        /// Makes the text italic.
        /// </summary>
        /// <param name="text">The text to make italic.</param>
        /// <returns>The italic text.</returns>
        public static string Italic(this string text) => $"<i>{text}</i>";

        /// <summary>
        /// Underlines the text.
        /// </summary>
        /// <param name="text">The text to underline.</param>
        /// <returns>The underlined text.</returns>
        public static string Underline(this string text) => $"<u>{text}</u>";

        /// <summary>
        /// Makes the text strikethrough.
        /// </summary>
        /// <param name="text">The text to make strikethrough.</param>
        /// <returns>The strikethrough text.</returns>
        public static string Strikethrough(this string text) => $"<s>{text}</s>";

        /// <summary>
        /// Colors the text.
        /// </summary>
        /// <param name="text">The text to color.</param>
        /// <param name="color">The color to use.</param>
        /// <returns>The colored text.</returns>
        public static string Color(this string text, string color) => $"<color={color}>{text}</color>";

        /// <summary>
        /// Colors the text.
        /// </summary>
        /// <param name="text">The text to color.</param>
        /// <param name="color">The color to use.</param>
        /// <param name="alpha">Whether to include the alpha value. Defaults to false.</param>
        /// <returns>The colored text.</returns>
        public static string Color(this string text, Color color, bool alpha = false) => $"<color={color.ToHex(includeAlpha: alpha)}>{text}</color>";

        /// <summary>
        /// Changes the size of the text.
        /// </summary>
        /// <param name="text">The text to change the size of.</param>
        /// <param name="size">The size to use.</param>
        /// <returns>The text with the new size.</returns>
        public static string Size(this string text, int size) => $"<size={size}>{text}</size>";

        /// <summary>
        /// Aligns the text using the specified alignment mode.
        /// </summary>
        /// <param name="text">The text to align.</param>
        /// <param name="alignment">The alignment to use.</param>
        /// <returns>The aligned text.</returns>
        public static string Align(this string text, TextAlignment alignment) => $"<align={AsString(alignment)}>{text}</align>";

    }

}
