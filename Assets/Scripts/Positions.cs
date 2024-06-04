using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Positions : MonoBehaviour
{
    public RectTransform truck;
    public NumberOfLocations m_numberOfLocationsEnum;
    public Deliveries m_deliveries;
    public TurnCounter m_turnCounter;
    public TruckStock m_truckStock;
    public GameObject[] m_stockLocations;
    public PositionButton[] m_positionButtons;
    public GameObject[] m_movePositions;
    private bool m_extra2locations1;
    private bool m_extra2locations2;
    private bool m_extra2locations3;
    public MoveControl m_moveControl;
    public StockControl m_stockControl;
    public int initialPosition;

    [HideInInspector] public int[] indexMapping;
    private int numberOfAllLocations;

    [HideInInspector] public int numberOfLocations;
    private Quaternion[] fourRotations= new Quaternion[4];


    // Start is called before the first frame update
    void Start()
    {
        InitiatePositions();
    }

    public void InitiatePositions()
    {
        switch(m_numberOfLocationsEnum)
        {
            case NumberOfLocations.Eight :
                m_extra2locations1 = false;
                m_extra2locations2 = true;
                m_extra2locations3 = false;
                break;
            case NumberOfLocations.Ten:
                m_extra2locations1 = true;
                m_extra2locations2 = false;
                m_extra2locations3 = true;
                break;
            case NumberOfLocations.Twelfe:
                m_extra2locations1 = true;
                m_extra2locations2 = true;
                m_extra2locations3 = true;
                break;
        }
        LocationIndexing();
        RemoveMissingLocations();
        DefineRotations();
        InitiateButtons();
        m_deliveries.InitiateDeliveries();
        m_turnCounter.InitiateTurnCounter();
        m_truckStock.InitiateTruckStock();
        m_moveControl.InitiateMoveControl(initialPosition);
        m_stockControl.InitiateStock(numberOfLocations, initialPosition);
    }

    public Vector3 GetVector3(int arrayIndex)
    {
        Vector3 pos = m_positionButtons[arrayIndex].GetComponent<RectTransform>().position;
        return pos;
    }

    public Quaternion GetRotation(TruckDirection direction)
    {
        return fourRotations[(int)direction];
    }

    public Quaternion GetCounterRotation(TruckDirection direction)
    {
        if (direction == TruckDirection.faceNorth)
        {
            return fourRotations[(int)TruckDirection.faceSouth];
        }
        if (direction == TruckDirection.faceSouth)
        {
            return fourRotations[(int)TruckDirection.faceNorth];
        }
        return fourRotations[(int)direction];
    }

    public void ActivateStock(int index, bool activate)
    {
        m_stockControl.m_stockLocations[index].PickLocation(activate);
        m_stockControl.activateLocationButtons(index, activate);
    }

    public PositionButton GetPositionButton(int index)
    {
        return m_positionButtons[indexMapping[index]];
    }

    public GameObject GetPositionObject(int index)
    {
        return m_movePositions[indexMapping[index]];
    }

    private void InitiateButtons()
    {
        for (int i = 0; i<numberOfAllLocations;i++)
        {
            m_positionButtons[i].InitiatePositionButton(m_moveControl.m_die1.m_diceSprites);
        }
    }

    private void LocationIndexing()
    {
        
        numberOfAllLocations = m_stockLocations.Length;
        numberOfLocations = numberOfAllLocations - 3 -2 * (
                            Convert.ToInt32(!m_extra2locations1) +
                            Convert.ToInt32(!m_extra2locations2) +
                            Convert.ToInt32(!m_extra2locations3));
        indexMapping = new int[numberOfLocations];
        indexMapping[0] = 0; indexMapping[1] = 1; indexMapping[2] = 2;

        int rowIndex = 1;

        if (m_extra2locations1)
        {
            indexMapping[2+rowIndex] = 5;
            indexMapping[numberOfLocations-rowIndex] = 3;
            rowIndex++;
        }

        if (m_extra2locations2)
        {
            indexMapping[2+rowIndex] = 8;
            indexMapping[numberOfLocations - rowIndex] = 6;
            rowIndex++;
        }

        if (m_extra2locations3)
        {
            indexMapping[2 + rowIndex] = 11;
            indexMapping[numberOfLocations - rowIndex] = 9;
            rowIndex++;
        }

        indexMapping[2+rowIndex] = 14;
        indexMapping[3+rowIndex] = 13;
        indexMapping[4+rowIndex] = 12;

        //for (int i = 0; i < indexMapping.Length; i++)
        //{
          //  Debug.Log("i=" + i + " arrayI =" + indexMapping[i]);
        //}
    }

    private void RemoveMissingLocations()
    {
        m_stockLocations[5].SetActive(m_extra2locations1);
        m_stockLocations[3].SetActive(m_extra2locations1);
        m_stockLocations[8].SetActive(m_extra2locations2);
        m_stockLocations[6].SetActive(m_extra2locations2);
        m_stockLocations[11].SetActive(m_extra2locations3);
        m_stockLocations[9].SetActive(m_extra2locations3);
        m_movePositions[5].SetActive(m_extra2locations1);
        m_movePositions[3].SetActive(m_extra2locations1);
        m_movePositions[8].SetActive(m_extra2locations2);
        m_movePositions[6].SetActive(m_extra2locations2);
        m_movePositions[11].SetActive(m_extra2locations3);
        m_movePositions[9].SetActive(m_extra2locations3);
    }

    private void DefineRotations()
    {
        fourRotations[0] = truck.rotation;
        truck.Rotate(0, 0, -90);
        fourRotations[1] = truck.rotation;
        truck.Rotate(0, 0, -90);
        fourRotations[2] = truck.rotation;
        truck.Rotate(0, 0, -90);
        fourRotations[3] = truck.rotation;
        truck.Rotate(0, 0, -90);
    }


        // Update is called once per frame
        void Update()
    {
    }

    public enum NumberOfLocations
    {
        Eight,
        Ten,
        Twelfe
    }

}
