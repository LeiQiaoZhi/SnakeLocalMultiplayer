using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SnakeSelectItem : MonoBehaviour
{
    public TMP_Dropdown controlsDropdown;
    public TMP_Dropdown colorsDropdown;
    public TMP_InputField nameInputField;

    private CanvasGroup canvasGroup;
    private List<ColorOption> colorOptions;
    private List<ControlOption> controlOptions;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetControlsOptions(List<ControlOption> options, int value = 0)
    {
        controlOptions = options;
        controlsDropdown.ClearOptions();
        controlsDropdown.AddOptions(options.Select(c=>c.name).ToList());
        controlsDropdown.value = value;
    }
    
    public void SetColorsOptions(List<ColorOption> options, int value = 0)
    {
        colorOptions = options;
        colorsDropdown.ClearOptions();
        colorsDropdown.AddOptions(options.Select(c=>c.name).ToList());
        colorsDropdown.value = value;
        OnColorDropdownValueChanged();
    }
    
    public void OnControlsDropdownValueChanged()
    {
        XLogger.Log(Category.UI,$"OnControlsDropdownValueChanged: {controlsDropdown.value}");
    }

    public void OnColorDropdownValueChanged()
    {
        var color = colorOptions[colorsDropdown.value].color;
        XLogger.Log(Category.UI,$"OnColorDropdownValueChanged: {color}");
        colorsDropdown.GetComponent<Image>().color = color;
    }
    
    public SnakeInitInfo GenerateSnakeInitInfo()
    {
        var color = colorOptions[colorsDropdown.value].color;
        var control = controlOptions[controlsDropdown.value];
        return new SnakeInitInfo(color, control, nameInputField.text);
    }

    public void SetActive(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }

    public void SetSnakeName(string snakeName)
    {
        nameInputField.text = snakeName;
    }
}