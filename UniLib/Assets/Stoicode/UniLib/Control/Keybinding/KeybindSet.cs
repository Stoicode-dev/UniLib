using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Stoicode.UniLib.Control
{
    [MessagePackObject(true)]
    public class KeybindSet
    {
        public List<Keybind> Content { get; set; }


        public KeybindSet()
        {
            SetDefault();
        }

        public KeybindSet(List<Keybind> defaultKeybinds)
        {
            Content = defaultKeybinds;
        }

        public Keybind GetKey(byte index)
        {
            return Content.Find(x => x.Index.Equals(index));
        }

        private void SetDefault()
        {
            Content = new List<Keybind>
            {
                
            };
        }
    }
}