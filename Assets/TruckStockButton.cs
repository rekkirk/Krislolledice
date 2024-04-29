using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckStockButton : MonoBehaviour
{
    [HideInInspector] public Button thisButton;
    public TruckStock m_truckStock;
    [HideInInspector] public int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
    }

    public void MoveGoods()
    {
        m_truckStock.removeGoods(thisIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
