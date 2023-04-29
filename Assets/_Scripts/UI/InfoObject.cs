using System.Collections;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    
    [RequireComponent(typeof(CanvasGroup))]
    public class InfoObject : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public void Init(string text, Color? textColor, float? duration)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            var textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = text;
            if (textColor != null)
            {
                textMesh.color = (Color) textColor;
            }

            StartCoroutine(Show(duration??2f));
        }
        
        private IEnumerator Show(float duration)
        {
            var animationDuration = duration / 4;
            var introFinishTime = Time.time + animationDuration;
            while (Time.time < introFinishTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - (introFinishTime - animationDuration)) / animationDuration);
                yield return null;
            }
            
            yield return new WaitForSeconds(duration - 2*animationDuration);
            
            var outroFinishTime = Time.time + animationDuration;
            while (Time.time < outroFinishTime)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, (Time.time - (outroFinishTime - animationDuration)) / animationDuration);
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}