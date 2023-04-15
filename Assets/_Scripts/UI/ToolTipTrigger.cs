using _Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.UI
{
    public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header;
        public string content;

        private void OnMouseEnter()
        {
            ToolTipManager.Instance.ShowToolTip(header,content);
        }

        private void OnMouseExit()
        {
            ToolTipManager.Instance.HideToolTip();
        }

        private void OnDisable()
        {
            ToolTipManager.Instance.HideToolTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipManager.Instance.ShowToolTip(header,content);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipManager.Instance.HideToolTip();
        }
    }
}
