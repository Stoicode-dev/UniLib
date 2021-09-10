using Stoicode.UniLib.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Stoicode.UniLib.Gui.Editor
{
    public static class GuiCanvasEditor
    {
        [MenuItem("GameObject/UniLib/GUI/Canvas", false, 0)]
        private static void AddObject(MenuCommand menuCommand)
        {
            // Create GUI Canvas object
            var main = new GameObject("GUI Canvas");
            
            var canvas = main.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            main.AddComponent<CanvasScaler>();
            main.AddComponent<GraphicRaycaster>();
            GameObjectUtility.SetParentAndAlign(main, menuCommand.context as GameObject);
            
            // Create event system
            var eventSystem = new GameObject("Event System");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            GameObjectUtility.SetParentAndAlign(eventSystem, main);

            // Create view container
            var viewContainer = new GameObject("View Container");
            EditorGuiUtils.AttachStretchedRect(viewContainer);
            GameObjectUtility.SetParentAndAlign(viewContainer, main);
            
            // Create popup container
            var popupContainer = new GameObject("Popup Container");
            EditorGuiUtils.AttachStretchedRect(popupContainer);
            GameObjectUtility.SetParentAndAlign(popupContainer, main);

            // Register the creation in the Unity undo system
            Undo.RegisterCreatedObjectUndo(main, "Created a GUI Canvas");
            Selection.activeObject = main;
        }
    }
}