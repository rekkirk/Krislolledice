using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocStockButton : MonoBehaviour
{
    public StockControl m_stockControl;
    [HideInInspector] public Button thisButton;
    [HideInInspector] public int thisIndex;
    [HideInInspector] public int parentIndex;

    public void PickGoods()
    {
        m_stockControl.PickLocGoods(parentIndex, thisIndex);
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    public void InitiateButton()
    {
        thisButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
