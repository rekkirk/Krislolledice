using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StartGameInput : MonoBehaviour
{
    public Positions m_positions;
    public StockControl m_stockControl;
    public TruckStock m_truckStock;
    public Deliveries m_deliveries;
    public TurnCounter m_turnCounter;
    public MoveControl m_moveControl;

    public TMP_Dropdown m_numberOfLocationsInput;
    public TMP_Dropdown m_numberOfGoodsTypesInput;
    public TMP_InputField m_startGoodsPerLocationInput;
    public TMP_InputField m_goodsPerRefillInput;
    public TMP_InputField m_truckCapacityInput;
    public TMP_InputField m_deliveriesToWinInput;
    public TMP_InputField m_numberOfTurnsInput;
    public TMP_Dropdown m_diceTypesInput;

    private const String m_numberOfLocationsKey = "numberOfLocations";
    private const String m_numberOfGoodsTypesKey = "numberOfGoodsTypes";
    private const String m_startGoodsPerLocationKey = "startGoodsPerLocation";
    private const String m_goodsPerRefillKey = "goodsPerRefill";
    private const String m_truckCapacityKey = "truckCapacity";
    private const String m_deliveriesToWinKey = "deliveriesToWin";
    private const String m_numberOfTurnsKey = "numberOfTurns";
    private const String m_diceTypesKey = "diceTypes";

    public GameObject m_errorMessage;
    public GameObject m_defaultValues;

    public void StartGame()
    {
        //m_stockControl.m_goodsPerLocation = int.Parse(m_startGoodsPerLocationInput.text);
        ReadInput();
        m_positions.InitiatePositions();
        gameObject.SetActive(false);
    }

    private void ReadInput()
    {
        switch (m_numberOfLocationsInput.value)
        {
            case 0:
                m_positions.m_numberOfLocationsEnum = Positions.NumberOfLocations.Eight;
                break;
            case 1:
                m_positions.m_numberOfLocationsEnum = Positions.NumberOfLocations.Ten;
                break;
            case 2:
                m_positions.m_numberOfLocationsEnum = Positions.NumberOfLocations.Twelfe;
                break;
        }

        switch (m_numberOfGoodsTypesInput.value)
        {
            case 0:
                m_stockControl.m_numberOfGoodsTypes = 2;
                break;
            case 1:
                m_stockControl.m_numberOfGoodsTypes = 3;
                break;
            case 2:
                m_stockControl.m_numberOfGoodsTypes = 4;
                break;
            case 3:
                m_stockControl.m_numberOfGoodsTypes = 5;
                break;
            case 4:
                m_stockControl.m_numberOfGoodsTypes = 6;
                break;
        }

        try
        {
            m_stockControl.m_goodsPerLocation = int.Parse(m_startGoodsPerLocationInput.text);
        }
        catch (FormatException) {
            m_startGoodsPerLocationInput.text = "";
            m_errorMessage.SetActive(true);
            return;
        }

        try
        {
            m_stockControl.m_goodsToRefill = int.Parse(m_goodsPerRefillInput.text);
        }
        catch (FormatException)
        {
            m_goodsPerRefillInput.text = "";
            m_errorMessage.SetActive(true);
            return;
        }

        try
        {
            m_truckStock.m_capacity = int.Parse(m_truckCapacityInput.text);
        }
        catch (FormatException)
        {
            m_truckCapacityInput.text = "";
            m_errorMessage.SetActive(true);
            return;
        }

        try
        {
            m_deliveries.m_totalDeliveries = int.Parse(m_deliveriesToWinInput.text);
        }
        catch (FormatException)
        {
            m_deliveriesToWinInput.text = "";
            m_errorMessage.SetActive(true);
            return;
        }

        try
        {
            m_turnCounter.m_turnsLeft = int.Parse(m_numberOfTurnsInput.text);
        }
        catch (FormatException)
        {
            m_numberOfTurnsInput.text = "";
            m_errorMessage.SetActive(true);
            return;
        }
            //public TMP_Dropdown m_diceTypesInput;
        switch (m_diceTypesInput.value)
        {
            case 0:
                m_moveControl.m_diceSetup = MoveControl.DiceSetup.DoubleMoveOne;
                break;
            case 1:
                m_moveControl.m_diceSetup = MoveControl.DiceSetup.Single;
                break;
        }

        PlayerPrefs.SetInt(m_numberOfLocationsKey, (int)(m_positions.m_numberOfLocationsEnum));
        PlayerPrefs.SetInt(m_numberOfGoodsTypesKey, (int)(m_stockControl.m_numberOfGoodsTypes));
        PlayerPrefs.SetInt(m_startGoodsPerLocationKey, m_stockControl.m_goodsPerLocation);
        PlayerPrefs.SetInt(m_goodsPerRefillKey, m_stockControl.m_goodsToRefill);
        PlayerPrefs.SetInt(m_truckCapacityKey, m_truckStock.m_capacity);
        PlayerPrefs.SetInt(m_deliveriesToWinKey, m_deliveries.m_totalDeliveries);
        PlayerPrefs.SetInt(m_numberOfTurnsKey, m_turnCounter.m_turnsLeft);
        PlayerPrefs.SetInt(m_diceTypesKey, (int)(m_moveControl.m_diceSetup));
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(m_numberOfLocationsKey))
        {
            ValuesToInputFields(); //at this point, the values are defined in unity editor
            return;
        }

        m_positions.m_numberOfLocationsEnum = (Positions.NumberOfLocations)PlayerPrefs.GetInt(m_numberOfLocationsKey);
        m_stockControl.m_numberOfGoodsTypes = PlayerPrefs.GetInt(m_numberOfGoodsTypesKey);
        m_stockControl.m_goodsPerLocation = PlayerPrefs.GetInt(m_startGoodsPerLocationKey);
        m_stockControl.m_goodsToRefill = PlayerPrefs.GetInt(m_goodsPerRefillKey);
        m_truckStock.m_capacity = PlayerPrefs.GetInt(m_truckCapacityKey);
        m_deliveries.m_totalDeliveries = PlayerPrefs.GetInt(m_deliveriesToWinKey);
        m_turnCounter.m_turnsLeft = PlayerPrefs.GetInt(m_numberOfTurnsKey);
        m_moveControl.m_diceSetup = (MoveControl.DiceSetup)PlayerPrefs.GetInt(m_diceTypesKey);

        ValuesToInputFields();
    }

    public void ValuesToInputFields()
    {
        switch (m_positions.m_numberOfLocationsEnum)
        {
            case Positions.NumberOfLocations.Eight:
                m_numberOfLocationsInput.SetValueWithoutNotify(0);
                break;
            case Positions.NumberOfLocations.Ten:
                m_numberOfLocationsInput.SetValueWithoutNotify(1);
                break;
            case Positions.NumberOfLocations.Twelfe:
                m_numberOfLocationsInput.SetValueWithoutNotify(2);
                break;
        }

        switch (m_stockControl.m_numberOfGoodsTypes)
        {
            case 2:
                m_numberOfGoodsTypesInput.SetValueWithoutNotify(0);
                break;
            case 3:
                m_numberOfGoodsTypesInput.SetValueWithoutNotify(1);
                break;
            case 4:
                m_numberOfGoodsTypesInput.SetValueWithoutNotify(2);
                break;
            case 5:
                m_numberOfGoodsTypesInput.SetValueWithoutNotify(3);
                break;
            case 6:
                m_numberOfGoodsTypesInput.SetValueWithoutNotify(4);
                break;
        }

        m_startGoodsPerLocationInput.text = "" + m_stockControl.m_goodsPerLocation;
        m_goodsPerRefillInput.text = "" + m_stockControl.m_goodsToRefill;
        m_truckCapacityInput.text = "" + m_truckStock.m_capacity;
        m_deliveriesToWinInput.text = "" + m_deliveries.m_totalDeliveries;
        m_numberOfTurnsInput.text = "" + m_turnCounter.m_turnsLeft;

        switch (m_moveControl.m_diceSetup)
        {
            case MoveControl.DiceSetup.DoubleMoveOne:
                m_diceTypesInput.SetValueWithoutNotify(0);
                break;
            case MoveControl.DiceSetup.Single:
                m_diceTypesInput.SetValueWithoutNotify(1);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
