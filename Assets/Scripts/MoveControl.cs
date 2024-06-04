using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveControl : MonoBehaviour
{
    public DiceSetup m_diceSetup;
    public OneDie m_die1;
    public OneDie m_die2;
    [HideInInspector] public PositionButton m_diePosition1;
    [HideInInspector] public PositionButton m_diePosition2;
    public Positions m_positions;
    public Deliveries m_deliveries;
    public TurnCounter m_turnCounter;
    public MessageFade m_userMessage;

    private TruckMove theTruck;
    private int possibleMove1ArrayIndex;
    private int possibleMove2ArrayIndex;
    private int possibleMove1RoadIndex;
    private int possibleMove2RoadIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitiateMoveControl(int startLocation)
    {

        theTruck = GetComponent<TruckMove>();
        theTruck.positions = m_positions;
        theTruck.currentRoadPosition = startLocation;
        theTruck.currentArrayPosition = startLocation;
        m_die1.dieID = 1;
        m_die2.dieID = 2;

        for (int i = 0; i < m_positions.numberOfLocations; i++)
        {
            m_positions.GetPositionObject(i).SetActive(false);
        }


        switch (m_diceSetup)
        {
            case DiceSetup.DoubleMoveOne:
                UpdateVisiblePositions(m_die1.Reroll(), m_die2.Reroll());
                break;
            case DiceSetup.Single:
                m_die2.gameObject.SetActive(false);
                UpdateVisiblePositions(m_die1.Reroll(), true);
                break;
                //default:
                //    Debug.Assert(false,"This case shouldn't exist")
        }        
    }

    private void UpdateVisiblePositions(int roll, bool isDie1)
    {
        if (isDie1)
        {
            possibleMove1RoadIndex = (theTruck.currentRoadPosition + roll) % m_positions.numberOfLocations;
            possibleMove1ArrayIndex = m_positions.indexMapping[possibleMove1RoadIndex];
            m_positions.m_movePositions[possibleMove1ArrayIndex].SetActive(true);
            m_diePosition1 = m_positions.m_positionButtons[possibleMove1ArrayIndex];
            m_diePosition1.ActivatePositionDie(m_die1);
        }
        else
        {
            possibleMove2RoadIndex = (theTruck.currentRoadPosition + roll) % m_positions.numberOfLocations;
            possibleMove2ArrayIndex = m_positions.indexMapping[possibleMove2RoadIndex];
            m_positions.m_movePositions[possibleMove2ArrayIndex].SetActive(true);
            m_diePosition2 = m_positions.m_positionButtons[possibleMove2ArrayIndex];
            m_diePosition2.ActivatePositionDie(m_die2);
        }

    }

    private void UpdateVisiblePositions(int roll1, int roll2)
    {
        possibleMove1RoadIndex = (theTruck.currentRoadPosition + roll1) % m_positions.numberOfLocations;
        possibleMove1ArrayIndex = m_positions.indexMapping[possibleMove1RoadIndex];
        m_positions.m_movePositions[possibleMove1ArrayIndex].SetActive(true);
        m_diePosition1 = m_positions.m_positionButtons[possibleMove1ArrayIndex];
        m_diePosition1.ActivatePositionDie(m_die1);

        possibleMove2RoadIndex = (theTruck.currentRoadPosition + roll2) % m_positions.numberOfLocations;
        possibleMove2ArrayIndex = m_positions.indexMapping[possibleMove2RoadIndex];
        m_positions.m_movePositions[possibleMove2ArrayIndex].SetActive(true);
        m_diePosition2 = m_positions.m_positionButtons[possibleMove2ArrayIndex];
        m_diePosition2.ActivatePositionDie(m_die2);

        int diceSumRoadIndex = (theTruck.currentRoadPosition + roll1 + roll2) % m_positions.numberOfLocations;
    }

    public void MovePicked(OneDie callerDie)
    {
        LoadingLocation currentLoc = m_positions.m_stockControl.m_stockLocations[theTruck.currentArrayPosition];
        if (currentLoc.CheckIfComplete())
        {
            if (m_deliveries.CompleteDelivery())
            {
                m_userMessage.SetText("You Won", true, true);
                return;
            } else
            {
                m_userMessage.SetText("Delivery Complete", false, false);
                currentLoc.RefillStockLocation(m_positions.m_stockControl.FillOneLocation(true));
                currentLoc.m_confetti.SetActive(false);
                currentLoc.m_confetti.SetActive(true);
            }
        }

        if (m_turnCounter.SpendTurn())
        {
            m_userMessage.SetText("You Lost", true, false);
                return;
        }

        m_positions.m_movePositions[possibleMove1ArrayIndex].SetActive(false);
        m_positions.m_movePositions[possibleMove2ArrayIndex].SetActive(false);
        m_positions.ActivateStock(theTruck.currentArrayPosition, false);

        

        if (callerDie.dieID == m_die1.dieID)
        {
            theTruck.Move(possibleMove1RoadIndex , possibleMove1ArrayIndex);
            switch (m_diceSetup)
            {
                case DiceSetup.DoubleMoveOne:
                    UpdateVisiblePositions(m_die1.Reroll(), m_die2.Reroll());
                    break;
                case DiceSetup.Single:
                    UpdateVisiblePositions(m_die1.Reroll(),true);
                    break;
                //default:
                //    Debug.Assert(false,"This case shouldn't exist")
            }
        }
        else
        {
            Debug.Assert(callerDie.dieID == m_die2.dieID,"Problem with dice ID");
            theTruck.Move(possibleMove2RoadIndex, possibleMove2ArrayIndex);
            switch (m_diceSetup)
            {
                case DiceSetup.DoubleMoveOne:
                    UpdateVisiblePositions(m_die1.Reroll(), m_die2.Reroll());
                    break;
                case DiceSetup.Single:
                    Debug.Assert(false, "Die2 should not be active with single die");
                    break;
                    //default:
                    //    Debug.Assert(false,"This case shouldn't exist")
            }        
        }
        m_positions.ActivateStock(theTruck.currentArrayPosition, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum DiceSetup
    {
        DoubleMoveOne,
        Single
    }
}
