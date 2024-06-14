using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockControl : MonoBehaviour
{
    public int m_goodsPerLocation; 
    public int m_goodsToRefill;
    public int m_numberOfGoodsTypes;
    [HideInInspector] private int numberOfLocations;
    public Positions m_positions;
    public TruckStock m_truckStock;
    public TruckMove m_truckMove;
    public MessageFade m_userMessage;
    [HideInInspector] public LoadingLocation[] m_stockLocations;
    

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

    private void InitiatePlayArea()
    {
        m_positions.m_positionButtons = new PositionButton[numberOfLocations];
        m_positions.m_movePositions = new GameObject[numberOfLocations];
        m_positions.LocationIndexing();

        for (int i=0; i<numberOfLocations; i++)
        {
            GameObject parent = m_stockLocations[i].transform.parent.gameObject;
            m_positions.m_positionButtons[i] = parent.GetComponentInChildren<PositionButton>();
            m_positions.m_movePositions[i] = m_positions.m_positionButtons[i].gameObject;
            parent.GetComponent<Image>().enabled = false;
        }
    }

    public void InitiateStock()
    {
        Debug.Assert(m_goodsPerLocation >= 2, "wrong number of goods per location");
        Debug.Assert(m_numberOfGoodsTypes>0 && m_numberOfGoodsTypes < 7, "wrong number of goods types");

        numberOfLocations = transform.childCount;
        m_stockLocations = new LoadingLocation[numberOfLocations];
        for (int i = 0; i < numberOfLocations; ++i)
        {
            m_stockLocations[i] = transform.GetChild(i).GetComponentInChildren<LoadingLocation>();
            //Debug.Log("i ; child: " + i + " ; " + m_stockLocations[i].transform.parent.gameObject.name);
        }

        InitiatePlayArea();

        for (int i= 0;i<numberOfLocations;i++)
        {
            int mappedIndex = m_positions.indexMapping[i];
            m_stockLocations[mappedIndex].InitiateStockLocation(FillOneLocation(false), mappedIndex);
        }

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
