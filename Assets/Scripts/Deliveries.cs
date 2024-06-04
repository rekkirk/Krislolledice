using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deliveries : MonoBehaviour
{
    public int m_totalDeliveries;
    private int m_completedDeliveries = 0;
    public TextMeshProUGUI m_totalText;
    public TextMeshProUGUI m_completedText;

    public bool CompleteDelivery()
    {
        m_completedDeliveries++;
        m_completedText.text = "" + m_completedDeliveries;
        return m_completedDeliveries == m_totalDeliveries;
    }

    public void InitiateDeliveries()
    {
        m_totalText.text = "" + m_totalDeliveries;
        m_completedText.text = "" + m_completedDeliveries;
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
