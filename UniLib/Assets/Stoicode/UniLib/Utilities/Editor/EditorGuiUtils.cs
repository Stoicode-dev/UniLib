using UnityEngine;

namespace Stoicode.UniLib.Utilities.Editor
{
    public static class EditorGuiUtils
    {
        /// <summary>
        /// Add a stretched rect transform to GUI object
        /// </summary>
        /// <param name="target">Target GUI object</param>
        public static void AttachStretchedRect(GameObject target)
        {
            var component = target.GetComponent<RectTransform>();
            if (component != null)
                Object.DestroyImmediate(component);
            
            var rt = target.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.sizeDelta = new Vector2(0f, 0f);
        }

        public static void AttachSizedRect(GameObject target, int x, int y)
        {
            var rt = target.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(x, y);
        }
    }
}