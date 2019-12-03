using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    /// <summary>
    /// Window class
    /// This is the base class for Gui window elements.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : MonoBehaviour
    {
        // Window identifier
        public string Identifier { get; protected set; }

        // Canvas
        private Canvas canvas;

        // Content object
        private GameObject content;
        

        /// <summary>
        /// Window initialization
        /// Requires Configure call
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Window load
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// Window unload
        /// </summary>
        protected abstract void Unload();

        /// <summary>
        /// Set window active status
        /// </summary>
        /// <param name="status">Active status</param>
        public void SetActive(bool status)
        {
            if (status)
            {
                content?.SetActive(true);
                canvas.enabled = true;
                Load();
            }
            else
            {
                Unload();
                canvas.enabled = false;
                content?.SetActive(false);
            }
        }

        /// <summary>
        /// Get active status
        /// </summary>
        /// <returns>Active status</returns>
        public bool IsActive()
        {
            return canvas.enabled;
        }

        /// <summary>
        /// Get identifier
        /// </summary>
        /// <returns>Window identifier</returns>
        public override string ToString()
        {
            return Identifier ?? "Gui.Window";
        }

        /// <summary>
        /// Configure the window
        /// Must be called in Initialize!
        /// </summary>
        /// <param name="identifier">Window identifier</param>
        /// <param name="useContent">Use content object for (de)activation of children on toggle</param>
        protected void Configure(string identifier, bool useContent = true)
        {
            Identifier = identifier;

            canvas = GetComponent<Canvas>();
            if (canvas == null)
                Debug.LogError("[Gui] Window is missing canvas component!");

            if (useContent)
            {
                if (transform.childCount == 0)
                    Debug.LogError("[Gui] Window is missing content object!");
                else
                    content = transform.GetChild(0).gameObject;
            }
        }
    }
}