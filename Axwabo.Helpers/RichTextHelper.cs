namespace Axwabo.Helpers;

#region Enums

/// <summary>Specifies the alignment of text.</summary>
public enum TextAlignment
{

    /// <summary>The text is aligned to the left.</summary>
    Left,

    /// <summary>The text is centered.</summary>
    Center,

    /// <summary>The text is aligned to the right.</summary>
    Right

}

/// <summary>Specifies capitalization modes.</summary>
public enum Capitalization
{

    /// <summary>All letters are lowercase.</summary>
    Lowercase,

    /// <summary>All letters are uppercase.</summary>
    Uppercase,

    /// <summary>All letters are capitalized, with uppercase letters being slightly larger.</summary>
    SmallCaps

}

#endregion

/// <summary>
/// Helps replace hardcoded rich text tags with more readable methods.
/// </summary>
public static class RichTextHelper
{

    #region Wrap

    /// <summary>
    /// Wrap the text with the given tag.
    /// </summary>
    /// <param name="text">The string to wrap.</param>
    /// <param name="tag">The tag to wrap the string with.</param>
    /// <returns>The wrapped string.</returns>
    public static string WrapWithTag(this string text, string tag) => $"<{tag}>{text}</{tag}>";

    /// <summary>
    /// Wrap the text with the given tag and value.
    /// </summary>
    /// <param name="text">The string to wrap.</param>
    /// <param name="tag">The tag to wrap the string with.</param>
    /// <param name="value">The value of the tag.</param>
    /// <returns>The wrapped string.</returns>
    public static string WrapWithTag(this string text, string tag, string value)
        => value is null ? text.WrapWithTag(tag) : $"<{tag}={value}>{text}</{tag}>";

    #endregion

    #region Simple Tags

    /// <summary>
    /// Makes the text bold.
    /// </summary>
    /// <param name="text">The text to make bold.</param>
    /// <returns>The bold text.</returns>
    public static string Bold(this string text) => text.WrapWithTag("b");

    /// <summary>
    /// Makes the text italic.
    /// </summary>
    /// <param name="text">The text to make italic.</param>
    /// <returns>The italic text.</returns>
    public static string Italic(this string text) => text.WrapWithTag("i");

    /// <summary>
    /// Underlines the text.
    /// </summary>
    /// <param name="text">The text to underline.</param>
    /// <returns>The underlined text.</returns>
    public static string Underline(this string text) => text.WrapWithTag("u");

    /// <summary>
    /// Makes the text strikethrough.
    /// </summary>
    /// <param name="text">The text to make strikethrough.</param>
    /// <returns>The strikethrough text.</returns>
    public static string Strikethrough(this string text) => text.WrapWithTag("s");

    /// <summary>
    /// Makes the text superscript.
    /// </summary>
    /// <param name="text">The text to make superscript.</param>
    /// <returns>The superscript text.</returns>
    public static string Superscript(this string text) => text.WrapWithTag("sup");

    /// <summary>
    /// Makes the text subscript.
    /// </summary>
    /// <param name="text">The text to make subscript.</param>
    /// <returns>The subscript text.</returns>
    public static string Subscript(this string text) => text.WrapWithTag("sub");

    #endregion

    #region Color & Alpha

    /// <summary>
    /// Colors the text.
    /// </summary>
    /// <param name="text">The text to color.</param>
    /// <param name="color">The color to use.</param>
    /// <returns>The colored text.</returns>
    public static string Color(this string text, string color) => text.WrapWithTag("color", color);

    /// <summary>
    /// Colors the text.
    /// </summary>
    /// <param name="text">The text to color.</param>
    /// <param name="color">The color to use.</param>
    /// <param name="alpha">Whether to include the alpha value. Defaults to false.</param>
    /// <returns>The colored text.</returns>
    public static string Color(this string text, Color color, bool alpha = false) => text.Color(color.ToHex(includeAlpha: alpha));

    #endregion

    /// <summary>
    /// Changes the size of the text.
    /// </summary>
    /// <param name="text">The text to change the size of.</param>
    /// <param name="size">The size to use (in pixels).</param>
    /// <returns>The text with the new size.</returns>
    public static string Size(this string text, int size) => text.Size($"{size}px");

    /// <summary>
    /// Changes the size of the text.
    /// </summary>
    /// <param name="text">The text to change the size of.</param>
    /// <param name="size">The size to use (with units).</param>
    /// <returns>The text with the new size.</returns>
    public static string Size(this string text, string size) => text.WrapWithTag("size", size);

    /// <summary>
    /// Aligns the text using the specified alignment mode.
    /// </summary>
    /// <param name="text">The text to align.</param>
    /// <param name="alignment">The alignment to use.</param>
    /// <returns>The aligned text.</returns>
    public static string Align(this string text, TextAlignment alignment) => text.WrapWithTag("align", alignment.ToString().ToLower());

    /// <summary>
    /// Adds an overlay to the text.
    /// </summary>
    /// <param name="text">The text to add an overlay to.</param>
    /// <param name="color">The color of the overlay.</param>
    /// <returns>The text with the overlay.</returns>
    public static string Mark(this string text, string color) => text.WrapWithTag("mark", color);

    /// <summary>
    /// Adds an overlay to the text.
    /// </summary>
    /// <param name="text">The text to add an overlay to.</param>
    /// <param name="color">The color of the overlay.</param>
    /// <returns>The text with the overlay.</returns>
    public static string Mark(this string text, Color color) => text.Mark(color.ToHex());

    /// <summary>
    /// Adds an overlay to the text and specific alpha value.
    /// </summary>
    /// <param name="text">The text to add an overlay to.</param>
    /// <param name="color">The color of the overlay.</param>
    /// <param name="alpha">The alpha value of the overlay (255 is fully visible).</param>
    /// <returns></returns>
    public static string Mark(this string text, Color color, byte alpha) => text.Mark($"{color.ToHex(true, false)}{alpha:X2}");

    /// <summary>
    /// Prevents parsing of some tags.
    /// </summary>
    /// <param name="text">The text to prevent parsing of.</param>
    /// <returns>The text with parsing disabled.</returns>
    public static string NoParse(this string text) => text.WrapWithTag("noparse");

    /// <summary>
    /// Applies a specific capitalization mode to the text.
    /// </summary>
    /// <param name="text">The text to capitalize.</param>
    /// <param name="mode">The capitalization mode to use.</param>
    /// <returns>The capitalized text.</returns>
    public static string Capitalize(this string text, Capitalization mode) => text.WrapWithTag(mode.ToString().ToLower());

    /// <summary>
    /// Adds character spacing to the text.
    /// </summary>
    /// <param name="text">The text to add character spacing to.</param>
    /// <param name="spacing">The spacing to use (in pixels).</param>
    /// <returns>The text with the character spacing.</returns>
    public static string CharacterSpacing(this string text, int spacing) => text.CharacterSpacing($"{spacing}px");

    /// <summary>
    /// Adds character spacing to the text.
    /// </summary>
    /// <param name="text">The text to add character spacing to.</param>
    /// <param name="spacing">The spacing to use (with units).</param>
    /// <returns>The text with the character spacing.</returns>
    public static string CharacterSpacing(this string text, string spacing) => text.WrapWithTag("cspace", spacing);

    /// <summary>
    /// Indents the text by the specified amount.
    /// </summary>
    /// <param name="text">The text to indent.</param>
    /// <param name="amount">The amount to indent (in pixels).</param>
    /// <returns>The indented text.</returns>
    /// <remarks>The effect persists across lines.</remarks>
    /// <seealso cref="LineIndent(string, int)"/>
    /// <seealso cref="LineIndent(string, string)"/>
    public static string Indent(this string text, int amount) => text.Indent($"{amount}px");

    /// <summary>
    /// Indents the text by the specified amount.
    /// </summary>
    /// <param name="text">The text to indent.</param>
    /// <param name="amount">The amount to indent (with units).</param>
    /// <returns>The indented text.</returns>
    /// <remarks>The effect persists across lines.</remarks>
    /// <seealso cref="LineIndent(string, int)"/>
    /// <seealso cref="LineIndent(string, string)"/>
    public static string Indent(this string text, string amount) => text.WrapWithTag("indent", amount);

    /// <summary>
    /// Specifies the line height of the rest of the row.
    /// </summary>
    /// <param name="text">The text to add line height to.</param>
    /// <param name="spacing">The spacing to use (in pixels).</param>
    /// <returns>The text with the line height.</returns>
    public static string LineHeight(this string text, int spacing) => text.LineHeight($"{spacing}px");

    /// <summary>
    /// Specifies the line height of the rest of the row.
    /// </summary>
    /// <param name="text">The text to add line height to.</param>
    /// <param name="spacing">The spacing to use (with units).</param>
    /// <returns>The text with the line height.</returns>
    public static string LineHeight(this string text, string spacing) => $"<line-height={spacing}>{text}";

    /// <summary>
    /// Indents the line by the specified amount.
    /// </summary>
    /// <param name="text">The text to indent.</param>
    /// <param name="amount">The amount to indent (in pixels).</param>
    /// <returns>The indented text.</returns>
    /// <remarks>The effect does not persist across lines.</remarks>
    /// <seealso cref="Indent(string, int)"/>
    /// <seealso cref="Indent(string, string)"/>
    public static string LineIndent(this string text, int amount) => text.LineIndent($"{amount}px");

    /// <summary>
    /// Indents the line by the specified amount.
    /// </summary>
    /// <param name="text">The text to indent.</param>
    /// <param name="amount">The amount to indent (with units).</param>
    /// <returns>The indented text.</returns>
    /// <remarks>The effect does not persist across lines.</remarks>
    /// <seealso cref="Indent(string, int)"/>
    /// <seealso cref="Indent(string, string)"/>
    public static string LineIndent(this string text, string amount) => $"<line-indent={amount}>{text}";

    /// <summary>
    /// Creates a hyperlink.
    /// </summary>
    /// <param name="text">The text to make a link of.</param>
    /// <param name="id">The metadata ID to point to.</param>
    /// <returns>The text as a hyperlink.</returns>
    public static string Link(this string text, string id) => text.WrapWithTag("link", $"\"{id}\"");

    /// <summary>
    /// Modifies the horizontal position of the rest of the row.
    /// </summary>
    /// <param name="text">The text to modify.</param>
    /// <param name="offset">The offset to use (in pixels).</param>
    /// <returns>The text with the horizontal offset.</returns>
    public static string HorizontalPosition(this string text, int offset) => text.HorizontalPosition($"{offset}px");

    /// <summary>
    /// Modifies the horizontal position of the rest of the row.
    /// </summary>
    /// <param name="text">The text to modify.</param>
    /// <param name="offset">The offset to use (with units).</param>
    /// <returns>The text with the horizontal offset.</returns>
    public static string HorizontalPosition(this string text, string offset) => $"<pos={offset}>{text}";

    /// <summary>
    /// Sets the margin of the text area.
    /// </summary>
    /// <param name="text">The text to set the margin of.</param>
    /// <param name="margin">The margin to use (in pixels).</param>
    /// <param name="alignment">Whether to set both, only the left or only the right margin.</param>
    /// <returns>The text with the margin.</returns>
    public static string Margin(this string text, int margin, TextAlignment alignment = TextAlignment.Center) => text.Margin($"{margin}px", alignment);

    /// <summary>
    /// Sets the margin of the text area.
    /// </summary>
    /// <param name="text">The text to set the margin of.</param>
    /// <param name="margin">The margin to use (with units).</param>
    /// <param name="alignment">Whether to set both, only the left or only the right margin.</param>
    /// <returns>The text with the margin.</returns>
    public static string Margin(this string text, string margin, TextAlignment alignment = TextAlignment.Center)
        => $"<margin{alignment switch
        {
            TextAlignment.Left => "-left",
            TextAlignment.Right => "-right",
            _ => ""
        }}={margin}>{text}";

    /// <summary>
    /// Makes the text monospaced.
    /// </summary>
    /// <param name="text">The text to make monospaced.</param>
    /// <param name="spacing">The amount of spacing to use (based on font size, 1 = default).</param>
    /// <returns>The text with monospacing.</returns>
    public static string Monospace(this string text, float spacing = 1f) => text.WrapWithTag("mspace", $"{spacing}em");

    /// <summary>
    /// Makes the text monospaced.
    /// </summary>
    /// <param name="text">The text to make monospaced.</param>
    /// <param name="spacing">The amount of spacing to use (with units).</param>
    /// <returns>The text with monospacing.</returns>
    public static string Monospace(this string text, string spacing) => text.WrapWithTag("mspace", spacing);

    /// <summary>
    /// Sets the vertical offset of the text.
    /// </summary>
    /// <param name="text">The text to set the vertical offset of.</param>
    /// <param name="offset">The offset to use (in pixels).</param>
    /// <returns>The text with the vertical offset.</returns>
    public static string VerticalOffset(this string text, int offset) => text.VerticalOffset($"{offset}px");

    /// <summary>
    /// Sets the vertical offset of the text.
    /// </summary>
    /// <param name="text">The text to set the vertical offset of.</param>
    /// <param name="offset">The offset to use (with units).</param>
    /// <returns>The text with the vertical offset.</returns>
    public static string VerticalOffset(this string text, string offset) => text.WrapWithTag("voffset", offset);

    /// <summary>
    /// Sets the max width of the text area.
    /// </summary>
    /// <param name="text">The text to set the max width of.</param>
    /// <param name="width">The max width to use (in pixels).</param>
    /// <returns>The text with the max width.</returns>
    public static string MaxWidth(this string text, int width) => text.MaxWidth($"{width}px");

    /// <summary>
    /// Sets the max width of the text area.
    /// </summary>
    /// <param name="text">The text to set the max width of.</param>
    /// <param name="width">The max width to use (with units).</param>
    /// <returns>The text with the max width.</returns>
    public static string MaxWidth(this string text, string width) => $"<width={width}>{text}";

    /// <summary>
    /// Rotates the given text.
    /// </summary>
    /// <param name="text">The text to rotate.</param>
    /// <param name="angle">The angle to rotate the text by.</param>
    /// <returns>The rotated text.</returns>
    public static string Rotate(this string text, float angle) => text.WrapWithTag("rotate", $"{angle}");

    #region Individual Tags

    /// <summary>
    /// Creates a sprite tag.
    /// </summary>
    /// <param name="index">The index of the sprite.</param>
    /// <param name="color">The color of the sprite (optional).</param>
    /// <returns>The sprite tag.</returns>
    public static string Sprite(int index, string color = null) => $"<sprite={index}{(color is null ? "" : $" color={color}")}>";

    /// <summary>
    /// Creates a sprite tag.
    /// </summary>
    /// <param name="index">The index of the sprite.</param>
    /// <param name="color">The color of the sprite.</param>
    /// <returns>The sprite tag.</returns>
    public static string Sprite(int index, Color color) => $"<sprite={index} color={color.ToHex()}>";

    /// <summary>
    /// Creates a horizontal space tag.
    /// </summary>
    /// <param name="amount">The amount of space to create (in pixels).</param>
    /// <returns>The horizontal space tag.</returns>
    public static string Space(int amount) => $"<space={amount}px>";

    /// <summary>
    /// Creates a horizontal space tag.
    /// </summary>
    /// <param name="amount">The amount of space to create (with units).</param>
    /// <returns>The horizontal space tag.</returns>
    public static string Space(string amount) => $"<space={amount}>";

    #endregion

    /// <summary>
    /// Adds a size 0 dot and a space tag of set size on both sides of the text.
    /// </summary>
    /// <param name="text">The text to surround.</param>
    /// <param name="size">The size of the space tag.</param>
    /// <returns>The text with the "margin".</returns>
    public static string AddBlankMargin(this string text, int size = 5) => text.AddBlankMargin($"{size}px");

    /// <summary>
    /// Adds a size 0 dot and a space tag of set size on both sides of the text.
    /// </summary>
    /// <param name="text">The text to surround.</param>
    /// <param name="size">The size of the space tag.</param>
    /// <returns>The text with the "margin".</returns>
    public static string AddBlankMargin(this string text, string size) => $"{".".Size("0")}{Space(size)}{text}{Space(size)}{".".Size("0")}";

}
