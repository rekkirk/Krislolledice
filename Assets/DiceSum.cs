using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSum : MonoBehaviour
{
    public RectTransform m_movingDice;
    public RectTransform[] m_locations;

    public void Move(int index)
    {
        m_movingDice.position = m_locations[index].position;
    }

    public void InitiateDiceSum()
    {
        for (int i=0;i<m_locations.Length;i++)
        {
            m_locations[i].gameObject.SetActive(false);
        }
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
