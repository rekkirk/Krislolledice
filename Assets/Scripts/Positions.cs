using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Positions : MonoBehaviour
{
    public RectTransform truck;
    public Deliveries m_deliveries;
    public TurnCounter m_turnCounter;
    public TruckStock m_truckStock;
    [HideInInspector] public PositionButton[] m_positionButtons;
    [HideInInspector] public GameObject[] m_movePositions;
    public MoveControl m_moveControl;
    public StockControl m_stockControl;

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
        m_stockControl.InitiateStock();
        DefineRotations();
        InitiateButtons();
        m_deliveries.InitiateDeliveries();
        m_turnCounter.InitiateTurnCounter();
        m_truckStock.InitiateTruckStock();
        m_moveControl.InitiateMoveControl(numberOfLocations-1);

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

    public void LocationIndexing()
    {
        
        numberOfAllLocations = m_movePositions.Length;
        numberOfLocations = numberOfAllLocations;
        indexMapping = new int[numberOfLocations];
        for (int i = 0 ; i<indexMapping.Length; i++)
        {
            indexMapping[i] = i;
        }

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

}
