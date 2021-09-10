using UnityEngine;
using UnityEngine.UI;

namespace Stoicode.UniLib.Gui
{
    public class ColorToggle : HoverToggle
    {
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        
        [SerializeField] private Image[] imageTargets;
        [SerializeField] private Text[] textTargets;
        
        
        protected override void Toggle(bool status)
        {
            for (var i = 0; i < imageTargets.Length; i++)
                imageTargets[i].color = status ? activeColor : inactiveColor;
            
            for (var i = 0; i < textTargets.Length; i++)
                textTargets[i].color = status ? activeColor : inactiveColor;
        }
    }
}