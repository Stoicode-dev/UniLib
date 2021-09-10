using Stoicode.UniLib.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Stoicode.UniLib.Gui.Editor
{
    public static class GuiPopupEditor
    {
        [MenuItem("GameObject/UniLib/GUI/Popup", false, 0)]
        private static void AddObject(MenuCommand menuCommand)
        {
            // Create GUI popup object
            var main = new GameObject("Empty Popup");
            EditorGuiUtils.AttachSizedRect(main, 100, 100);
            main.AddComponent<EmptyPopup>();
            GameObjectUtility.SetParentAndAlign(main, menuCommand.context as GameObject);

            var bg = new GameObject("Background Image");
            GameObjectUtility.SetParentAndAlign(bg, main);
            EditorGuiUtils.AttachStretchedRect(bg);
            bg.AddComponent<Image>();

            // Register the creation in the Unity undo system
            Undo.RegisterCreatedObjectUndo(main, "Created an empty view");
            Selection.activeObject = main;
        }
    }
}