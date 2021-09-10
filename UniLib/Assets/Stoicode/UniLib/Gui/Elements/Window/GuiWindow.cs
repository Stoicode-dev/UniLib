using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    /// <summary>
    /// Base class for GUI windows
    /// </summary>
    public abstract class GuiWindow : MonoBehaviour
    {
        // Identifier
        public int Identifier { get; protected set; }

        // Initialization on app / gui launch
        public abstract void Initialize();
        
        // On window activation
        public abstract void Open();
        
        // On window deactivation
        public abstract void Close();
    }
}