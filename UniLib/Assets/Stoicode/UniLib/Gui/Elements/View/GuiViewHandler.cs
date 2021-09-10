using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class GuiViewHandler
    {
        // Current active view
        public int ActiveView { get; private set; }
        
        // View content (GuiView)
        private readonly Dictionary<int, GuiView> content;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="startView"></param>
        public GuiViewHandler(int startView)
        {
            var views = Object.FindObjectsOfType<GuiView>();

            foreach (var v in views)
            {
                v.Initialize();
                if (v.Identifier != startView)
                    v.gameObject.SetActive(false);
                else
                    v.Open();
            }
            
            content = views
                .ToDictionary(x => x.Identifier, x => x);

            ActiveView = startView;
        }

        /// <summary>
        /// Open a view
        /// </summary>
        /// <param name="identifier">View identifier</param>
        public void Open(int identifier)
        {
            if (!content.ContainsKey(identifier))
            {
                Debug.LogError("[GuiViewHandler] Invalid view identifier!");
                return;
            }
            
            content[ActiveView].Close();
            content[ActiveView].gameObject.SetActive(false);
            
            content[identifier].gameObject.SetActive(true);
            content[identifier].Open();

            ActiveView = identifier;
        }

        /// <summary>
        /// Returns a view by identifier
        /// </summary>
        /// <param name="identifier">View identifier</param>
        /// <returns>Requested view</returns>
        public GuiView Get(int identifier)
        {
            if (!content.ContainsKey(identifier))
            {
                Debug.LogError("[GuiViewHandler] Invalid view identifier!");
                return null;
            }

            return content[identifier];
        }
    }
}