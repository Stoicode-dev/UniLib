using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control
{
    public class Keybind
    {
        public byte Index { get; set; }

        public string Name { get; set; }

        public KeyCode Primary { get; set; }

        public KeyCode Alternative { get; set; }


        public Keybind(byte index, string name, KeyCode primary)
        {
            Index = index;
            Name = name;
            Primary = primary;
        }

        public Keybind(byte index, string name, KeyCode primary, KeyCode alternative) 
            : this(index, name, primary)
        {
            Alternative = alternative;
        }

        public bool Holding()
        {
            return Input.GetKey(Primary)
                   || Input.GetKey(Alternative);
        }

        public bool Pressed()
        {
            return Input.GetKeyDown(Primary)
                   || Input.GetKeyDown(Alternative);
        }

        public bool Released()
        {
            return Input.GetKeyUp(Primary)
                   || Input.GetKeyUp(Alternative);
        }
    }
}