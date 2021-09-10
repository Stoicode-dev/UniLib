using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Stoicode.UniLib.Gui
{
    public abstract class HoverToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected bool inverted;

        protected abstract void Toggle(bool status);


        private void Awake()
        {
            Toggle(inverted);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Toggle(!inverted);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Toggle(inverted);
        }
    }
}