using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SnakeSelectItem : MonoBehaviour
{
    public TMP_Dropdown controlsDropdown;
    public TMP_Dropdown colorsDropdown;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetControlsOptions(List<string> options)
    {
        controlsDropdown.ClearOptions();
        controlsDropdown.AddOptions(options);
    }
    
    public void SetColorsOptions(List<string> options)
    {
        colorsDropdown.ClearOptions();
        colorsDropdown.AddOptions(options);
    }
    
    public void SetActive(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }
   
}
