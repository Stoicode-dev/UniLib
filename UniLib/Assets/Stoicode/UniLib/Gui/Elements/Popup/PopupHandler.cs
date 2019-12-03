using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class PopupHandler
    {
        // Popups
        private List<Popup> content;


        /// <summary>
        /// Default constructor
        /// </summary>
        public PopupHandler()
        {
            Find();
            Initialize();
        }

        /// <summary>
        /// Find popups
        /// </summary>
        public void Find()
        {
            content = Object.FindObjectsOfType<Popup>().ToList();
        }

        /// <summary>
        /// Initialize popups
        /// </summary>
        public void Initialize()
        {
            for (var i = 0; i < content.Count; i++)
                content[i].Initialize();
        }

        /// <summary>
        /// Get popup by identifier
        /// </summary>
        /// <param name="identifier">Popup identifier</param>
        /// <returns>Popup object</returns>
        public Popup Get(string identifier)
        {
            var p = content.Find(x => x.Identifier.Equals(identifier));

            if (p == null)
                Debug.LogError("[PopupHandler] Attempting to get popup with non-existent identifier!");

            return p;
        }

        /// <summary>
        /// Set popup active status
        /// </summary>
        /// <param name="identifier">Popup identifier</param>
        /// <param name="status">Active status</param>
        public void SetActive(string identifier, bool status)
        {
            var p = Get(identifier);

            if (p.IsActive().Equals(status))
                return;

            p.SetActive(status);
        }

        /// <summary>
        /// Set multiple popup active status
        /// </summary>
        /// <param name="identifiers">Popup identifiers</param>
        /// <param name="status">Active status</param>
        public void SetActive(string[] identifiers, bool status)
        {
            for (var i = 0; i < identifiers.Length; i++)
                SetActive(identifiers[i], status);
        }

        /// <summary>
        /// Deactivate all popups
        /// </summary>
        public void DeactivateAll()
        {
            for (var i = 0; i < content.Count; i++)
                SetActive(content[i].Identifier, false);
        }

        /// <summary>
        /// Deactivate all other popups
        /// </summary>
        /// <param name="identifier">Popup to preserve</param>
        public void DeactivateOther(string identifier)
        {
            for (var i = 0; i < content.Count; i++)
            {
                if (content[i].Identifier.Equals(identifier))
                    continue;

                SetActive(content[i].Identifier, false);
            }

        }
    }
}