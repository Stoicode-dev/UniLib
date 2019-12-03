using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class ViewHandler
    {
        // Views
        private List<View> content;

        // Active view
        private string active;


        /// <summary>
        /// Default constructor
        /// </summary>
        public ViewHandler()
        {
            Find();
            Initialize();
        }

        /// <summary>
        /// Alternative constructor
        /// </summary>
        /// <param name="startView">View to enable on start</param>
        public ViewHandler(string startView) : this()
        {
            SetActive(startView);
        }

        /// <summary>
        /// Find views
        /// </summary>
        public void Find()
        {
            content = Object.FindObjectsOfType<View>().ToList();
        }

        /// <summary>
        /// Initialize views
        /// </summary>
        public void Initialize()
        {
            for (var i = 0; i < content.Count; i++)
                content[i].Initialize();
        }

        /// <summary>
        /// Get view by identifier
        /// </summary>
        /// <param name="identifier">View identifier</param>
        /// <returns>View object</returns>
        public View Get(string identifier)
        {
            var v = content.Find(x => x.Identifier.Equals(identifier));

            if (v == null)
                Debug.LogError("[ViewHandler] Attempting to get view with non-existent identifier!");

            return v;
        }

        /// <summary>
        /// Set active view
        /// </summary>
        /// <param name="identifier">View identifier</param>
        public void SetActive(string identifier)
        {
            var v = Get(identifier);
            v.SetActive(true);

            if (!string.IsNullOrWhiteSpace(active))
            {
                var a = Get(active);
                a.SetActive(false);
            }

            active = identifier;
        }
    }
}