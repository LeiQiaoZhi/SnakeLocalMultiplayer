using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeSelectionManager : MonoBehaviour
{
    public int defaultSnakeCount = 2;

    private List<SnakeSelectItem> selectItems;

    // Start is called before the first frame update
    void Start()
    {
        selectItems = GetComponentsInChildren<SnakeSelectItem>().ToList();
        // set active default snakes, set inactive others
        for (int i = 0; i < selectItems.Count; i++)
        {
            selectItems[i].SetActive(i < defaultSnakeCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}