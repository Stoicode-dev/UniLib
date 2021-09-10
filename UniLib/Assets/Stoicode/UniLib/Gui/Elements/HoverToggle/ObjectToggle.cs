using UnityEngine;

namespace Stoicode.UniLib.Gui
{
    public class ObjectToggle : HoverToggle
    {
        [SerializeField] private GameObject[] targets;
        
        
        protected override void Toggle(bool status)
        {
            for (var i = 0; i < targets.Length; i++)
                targets[i].SetActive(status);
        }
    }
}