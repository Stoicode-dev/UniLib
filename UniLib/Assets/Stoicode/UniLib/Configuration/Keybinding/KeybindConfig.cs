using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Configuration.Keybinding
{
    public class KeybindConfig
    {
        public Dictionary<KeyId, Keybind> Content { get; protected set; }


        public KeybindConfig()
        {
            Content = new Dictionary<KeyId, Keybind> 
            {
                { KeyId.Forward, new Keybind("Movement", "Move Forward", KeyCode.W, KeyCode.UpArrow) }
            };
        }

        public KeybindConfig(Dictionary<KeyId, Keybind>  content)
        {
            Content = new Dictionary<KeyId, Keybind>(content);
        }

        /// <summary>
        /// Get keybind data
        /// </summary>
        /// <param name="identifier">Key identifier</param>
        /// <returns>Keybind data</returns>
        public Keybind Get(KeyId identifier)
        {
            return Content.ContainsKey(identifier) ? null : Content[identifier];
        }
    }
}