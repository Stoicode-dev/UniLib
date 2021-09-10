using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class GuiPopupHandler
    {
        // Active popups list
        private readonly List<int> activated;
        
        // Popup content
        private readonly Dictionary<int, GuiPopup> content;


        /// <summary>
        /// Default constructor
        /// </summary>
        public GuiPopupHandler()
        {
            activated = new List<int>();

            var popups = Object.FindObjectsOfType<GuiPopup>();

            foreach (var p in popups)
            {
                p.Initialize();
                p.gameObject.SetActive(false);
            }
            
            content = popups.ToDictionary(x => x.Identifier, x => x);
        }

        /// <summary>
        /// Toggle popup on/off
        /// </summary>
        /// <param name="identifier">Popup identifier</param>
        /// <param name="status">Activation status</param>
        public GuiPopup Toggle(int identifier, bool status)
        {
            if (!content.ContainsKey(identifier))
            {
                Debug.LogWarning("[GuiPopupHandler] Attempting to toggle popup with invalid identifier.");
                return null;
            }
            
            if (status && !activated.Contains(identifier))
                return null;
            
            if (!status && !activated.Contains(identifier))
                return null;

            if (status)
            {
                content[identifier].gameObject.SetActive(true);
                content[identifier].Open();
                activated.Add(identifier);
            }
            else
            {
                content[identifier].Close();
                content[identifier].gameObject.SetActive(false);
                activated.Remove(identifier);
            }

            return content[identifier];
        }

        /// <summary>
        /// Get popup by id
        /// </summary>
        /// <param name="identifier">Popup identifier</param>
        /// <returns>Popup</returns>
        public GuiPopup Get(int identifier)
        {
            if (!content.ContainsKey(identifier))
            {
                Debug.LogError("[GuiPopupHandler] Attempting to get popup with invalid identifier.");
                 return null;
            }

            return content[identifier];
        }

        /// <summary>
        /// Check if popup is active
        /// </summary>
        /// <param name="identifier">Popup identifier</param>
        /// <returns>Activation status</returns>
        public bool CheckActive(int identifier)
        {
            if (!content.ContainsKey(identifier))
            {
                Debug.LogWarning("[GuiPopupHandler] Attempting to check popup with invalid identifier.");
                return false;
            }

            return activated.Contains(identifier);
        }
    }
}