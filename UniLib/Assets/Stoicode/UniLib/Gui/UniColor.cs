using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class UniColor
    {
        // Identifier
        public string Name { get; set; }

        // Hex code
        public string Hex { get; protected set; }

        // Color
        public Color Color { get; protected set; }

        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UniColor() { }

        /// <summary>
        /// Constructor for Hex code
        /// </summary>
        /// <param name="hex">Hex color code</param>
        public UniColor(string hex)
        {
            Hex = hex;
            Color = HexToColor(hex);
        }

        /// <summary>
        /// Constructor for Color
        /// </summary>
        /// <param name="color">Color object</param>
        public UniColor(Color color)
        {
            Color = color;
            Hex = ColorToHex(color);
        }
        
        /// <summary>
        /// Change color to hex
        /// </summary>
        /// <param name="color">Color object</param>
        /// <returns>Hex color code</returns>
        public static string ColorToHex(Color color)
        {
            var hex = $"{color.r:X2}{color.r:X2}{color.b:X2}{color.a:X2}";

            return hex;
        }

        /// <summary>
        /// Change hex to color with alpha
        /// </summary>
        /// <param name="hex">Hex color code</param>
        /// <param name="alpha">Alpha value</param>
        /// <returns></returns>
        public static Color HexToColor(string hex, byte alpha = 255)
        {
            var r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, alpha);
        }
        
        /// <summary>
        /// Change hex to color with alpha
        /// </summary>
        /// <param name="hex">Hex color code</param>
        /// <param name="alpha">Alpha percentage 0f - 1f</param>
        /// <returns></returns>
        public static Color HexToColor(string hex, float alpha)
        {
            return HexToColor(hex, (byte) (255 * alpha));
        }

        /// <summary>
        /// Get color info string
        /// </summary>
        /// <returns>String with color information</returns>
        public string GetInfo()
        {
            return $"Name: {Name} | Hex: {Hex} | Color: {GetRgbaString()}";
        }

        /// <summary>
        /// Get RGBA string
        /// </summary>
        /// <returns>RGBA format string</returns>
        public string GetRgbaString()
        {
            return $"R:{Color.r} G:{Color.g} B:{Color.b} A:{Color.a}";
        }
    }
}