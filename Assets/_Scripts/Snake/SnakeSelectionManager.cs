using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Helpers;
using _Scripts.Snake;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ColorOption
{
    public string name;
    public Color color;
}

[System.Serializable]
public class ControlOption{
    public string name;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
}

/// <summary>
/// singleton class that manages snake selection UI
/// </summary>
public class SnakeSelectionManager : MonoBehaviour
{
    public int defaultSnakeCount = 2;
    [Header("UI")]
    public TMP_Dropdown numSnakeDropdown;
    public RectTransform snakeSelectionTransform;
    [Header("Options")]
    public List<ColorOption> colors;
    public List<ControlOption> controls;

    private List<SnakeSelectItem> selectItems;
    private int maxSnakeCount;
    private int currentSnakeCount;
    
    public static SnakeSelectionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectItems = snakeSelectionTransform.GetComponentsInChildren<SnakeSelectItem>().ToList();
        maxSnakeCount = selectItems.Count;
        currentSnakeCount = defaultSnakeCount;
        SetNumSnakes(defaultSnakeCount);
        // init item's color options
        for(int i = 0; i < selectItems.Count; i++)
        {
            var selectItem = selectItems[i];
            selectItem.SetColorsOptions(colors, i);
            selectItem.SetControlsOptions(controls, i);
            selectItem.SetSnakeName($"Snake {i + 1}");
        }
    }

    public void StartGame()
    {
        // get all the snake init infos from select items
        var snakeInitInfos = new List<SnakeInitInfo>();
        for (int i = 0; i < currentSnakeCount; i++)
        {
            snakeInitInfos.Add(selectItems[i].GenerateSnakeInitInfo());
        }
        SnakeGameManager.Instance.StartGame(snakeInitInfos);
    }
    
    public void OnNumSnakeDropdownValueChanged()
    {
        SetNumSnakes(numSnakeDropdown.value + defaultSnakeCount);
    }

    private void SetNumSnakes(int numSnakes)
    {
        XLogger.Log($"SetNumSnakes: {numSnakes}");
        currentSnakeCount = numSnakes;
        for (int i = 0; i < selectItems.Count; i++)
        {
            selectItems[i].SetActive(i < currentSnakeCount);
        }
    }

}