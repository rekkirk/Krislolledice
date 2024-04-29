using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StockControl : MonoBehaviour
{
    public int m_goodsPerLocation; 
    public int m_goodsToRefill;
    public int m_numberOfGoodsTypes;
    public Positions m_positions;
    public TruckStock m_truckStock;
    public TruckMove m_truckMove;
    public MessageFade m_userMessage;
    public LoadingLocation[] m_stockLocations;
    

    public void PickLocGoods(int locIndex, int goodsIndex)
    {
        if (m_truckStock.addGoods(goodsIndex))
        {
            m_stockLocations[locIndex].RemoveGoods(goodsIndex);
        }
        else
        {
            m_userMessage.SetText("No available room", false, false);
        }
    }

    public void PickTruckGoods(int index)
    {
        m_stockLocations[m_truckMove.currentArrayPosition].AddGoods(index);
    }

    public void activateLocationButtons(int index, bool activate)
    {
        if (activate)
        {
            EnableLocationButtons(index);
        }
        else
        {
            DisableLocationButtons(index);
        }
    }

    private void DisableLocationButtons(int locIndex)
    {
        for (int i=0; i<m_numberOfGoodsTypes;i++)
        {
            m_stockLocations[locIndex].locStockButtons[i].thisButton.enabled = false;
        }
    }

    private void EnableLocationButtons(int locIndex)
    {
        for (int i = 0; i < m_numberOfGoodsTypes; i++)
        {
            LoadingLocation loc = m_stockLocations[locIndex];
            loc.locStockButtons[i].thisButton.enabled = true;
            //m_stockLocations[locIndex].locStockButtons[i].thisButton.enabled = true;
        }
    }

    public void InitiateStock(int numberOfLocations, int initialPosition)
    {
        Debug.Assert(m_goodsPerLocation >= 2, "wrong number of goods per location");
        Debug.Assert(m_numberOfGoodsTypes>0 && m_numberOfGoodsTypes < 7, "wrong number of goods types");

        int totalGoods = (int)(m_goodsPerLocation * numberOfLocations);

        for (int i= 0;i<numberOfLocations;i++)
        {
            int mappedIndex = m_positions.indexMapping[i];
            m_stockLocations[mappedIndex].InitiateStockLocation(FillOneLocation(false), mappedIndex);
        }
        EnableLocationButtons(initialPosition);
        m_stockLocations[initialPosition].PickLocation(true);

    }

    public int[] FillOneLocation(bool refill)
    {
        int goodsToFill = 0;
        if (refill)
        {
            goodsToFill = m_goodsToRefill;
        }
        else
        {
            goodsToFill = m_goodsPerLocation;
        }

        int[] locationStock = new int[6];
        int prevValue = 0; int value = 0;
        bool OnlyOneType = true;
        for (int i = 0; i<goodsToFill-1; i++)
        {
            value = Random.Range(0, m_numberOfGoodsTypes);
            OnlyOneType = OnlyOneType && (i == 0 || value == prevValue);
            locationStock[value]++;
            prevValue = value;
        }
        if (OnlyOneType)
        {
            value = (value + Random.Range(1, m_numberOfGoodsTypes)) % m_numberOfGoodsTypes;
            locationStock[value]++;
            Debug.Assert(value != prevValue,"Coding error. Should be different");
        }
        else
        {
            locationStock[Random.Range(0, m_numberOfGoodsTypes)]++;
        }
        return locationStock;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
