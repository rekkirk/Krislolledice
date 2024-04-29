using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TruckStock : MonoBehaviour
{
    public TextMeshProUGUI[] m_stockTexts;
    public GameObject[] m_stockObjects;
    private int[] currentStock;
    private int currentStockTotal;
    public StockControl m_stockControl;
    public int m_capacity;
    public TextMeshProUGUI m_capacityNumber;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitiateTruckStock()
    {
        Debug.Assert(m_capacity > 0, "Wrong truck capacity");
        currentStock = new int[6];
        currentStockTotal = 0;
        m_capacityNumber.text = "" + m_capacity;
        for (int i = 0; i < m_stockTexts.Length; i++)
        {
            m_stockObjects[i].GetComponent<TruckStockButton>().thisIndex = i;
        }

        for (int i = 0; i < currentStock.Length; i++)
        {
            currentStock[i] = 0;
            m_stockObjects[i].SetActive(false);
        }
    }

    public bool addGoods(int goodsIndex)
    {
        if (currentStockTotal < m_capacity)
        {
            if (currentStock[goodsIndex] == 0)
            {
                m_stockObjects[goodsIndex].SetActive(true);
            }
            currentStockTotal++;
            currentStock[goodsIndex]++;
            m_stockTexts[goodsIndex].text = "" + currentStock[goodsIndex];
            m_capacityNumber.text = "" + (m_capacity - currentStockTotal);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void removeGoods(int goodsIndex)
    {
        Debug.Assert(currentStock[goodsIndex]>0,". Error. Goods gameobject should be disabled at 0 goods");
        currentStockTotal--;
        currentStock[goodsIndex]--;
        m_stockTexts[goodsIndex].text = "" + currentStock[goodsIndex];
        m_stockObjects[goodsIndex].SetActive(currentStock[goodsIndex] > 0);
        m_stockControl.PickTruckGoods(goodsIndex);
        m_capacityNumber.text = "" + (m_capacity - currentStockTotal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
