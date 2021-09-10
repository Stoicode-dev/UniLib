using UnityEngine;

namespace Stoicode.UniLib.Configuration.Keybinding
{
    public class Keybind
    {
        public KeyId Identifier { get; set; }
        
        public string Type { get; set; }
        public string Name { get; set; }

        public KeyCode Default { get; set; }
        public KeyCode Primary { get; set; }
        public KeyCode Secondary { get; set; }


        public Keybind()
        {
        }

        public Keybind(string type, string name, KeyCode def)
        {
            Type = type;
            Name = name;

            Default = def;
            Primary = def;
            Secondary = KeyCode.None;
        }
        
        public Keybind(string type, string name, KeyCode def, KeyCode alt)
        {
            Type = type;
            Name = name;

            Default = def;
            Primary = def;
            Secondary = alt;
        }

        public bool Holding()
        {
            var status = false;

            if (Primary != KeyCode.None)
                status = Input.GetKey(Primary);

            if (!status && Secondary != KeyCode.None)
                status = Input.GetKey(Secondary);

            return status;
        }

        public bool Pressed()
        {
            var status = false;

            if (Primary != KeyCode.None)
                status = Input.GetKeyDown(Primary);

            if (!status && Secondary != KeyCode.None)
                status = Input.GetKeyDown(Secondary);

            return status;
        }

        public bool Released()
        {
            var status = false;

            if (Primary != KeyCode.None)
                status = Input.GetKeyUp(Primary);

            if (!status && Secondary != KeyCode.None)
                status = Input.GetKeyUp(Secondary);

            return status;
        }
    }
}