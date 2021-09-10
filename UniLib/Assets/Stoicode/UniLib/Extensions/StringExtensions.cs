using UnityEngine;

namespace Stoicode.UniLib.Extensions
{
    public static class StringExtensions
    {
        public static Color HexToColor(this string s)
        {
            ColorUtility.TryParseHtmlString(s, out var c);

            return c;
        }
    }
}