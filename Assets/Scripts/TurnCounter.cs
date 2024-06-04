using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCounter : MonoBehaviour
{
    public int m_turnsLeft;
    public TextMeshProUGUI m_TurnText;
    
    public bool SpendTurn()
    {
        m_turnsLeft--;
        m_TurnText.text = "" + m_turnsLeft;
        return (m_turnsLeft == -1);
    }

    public void AddTurns(int number)
    {
        m_turnsLeft = m_turnsLeft + number;
        m_TurnText.text = "" + m_turnsLeft;
    }

    public void InitiateTurnCounter()
    {
        m_TurnText.text = "" + m_turnsLeft;
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
