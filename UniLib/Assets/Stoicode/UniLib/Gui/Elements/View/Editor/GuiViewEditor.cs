using Stoicode.UniLib.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Stoicode.UniLib.Gui.Editor
{
    public static class GuiViewEditor
    {
        [MenuItem("GameObject/UniLib/GUI/View", false, 0)]
        private static void AddObject(MenuCommand menuCommand)
        {
            // Create GUI view object
            var main = new GameObject("Empty View");
            EditorGuiUtils.AttachStretchedRect(main);
            main.AddComponent<EmptyView>();
            GameObjectUtility.SetParentAndAlign(main, menuCommand.context as GameObject);

            // Register the creation in the Unity undo system
            Undo.RegisterCreatedObjectUndo(main, "Created an empty view");
            Selection.activeObject = main;
        }
    }
}