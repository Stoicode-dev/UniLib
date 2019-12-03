using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Extensions
{
    public static class StringExtension
    {
        public static Color HexToColor(this String s)
        {
            ColorUtility.TryParseHtmlString(s, out var c);

            return c;
        }
    }
}