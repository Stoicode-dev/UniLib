using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class GuiController : MonoBehaviour
    {
        public static ViewHandler ViewHandler { get; protected set; }
        public static PopupHandler PopupHandler { get; protected set; }


        /// <summary>
        /// Initialize controller
        /// </summary>
        /// <param name="startView">(optional) View to start with</param>
        /// <returns>Null</returns>
        public IEnumerator Initialize(string startView = "")
        {
            ViewHandler = startView.Equals("") ? 
                new ViewHandler() : new ViewHandler(startView);
            PopupHandler = new PopupHandler();

            yield return null;
        }
    }
}