using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TruckMove : MonoBehaviour
{
    [HideInInspector] public Positions positions;
    [HideInInspector] public int currentArrayPosition;
    [HideInInspector] public int currentRoadPosition;
    private RectTransform thisTransform;
    public RectTransform m_capacityText;
    public RectTransform[] m_goodsTexts;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<RectTransform>();
    }

    public void Move(int newSpaceRoadIndex, int newSpaceArrayIndex)
    {
        PositionButton newPositionButton=positions.m_positionButtons[newSpaceArrayIndex];        
        thisTransform.SetPositionAndRotation(positions.GetVector3(newSpaceArrayIndex),
                                             positions.GetRotation(newPositionButton.m_truckDirection));
        currentRoadPosition = newSpaceRoadIndex;
        currentArrayPosition = newSpaceArrayIndex;

        //counterRotate numbers:
        Quaternion textRotation = positions.GetCounterRotation(newPositionButton.m_truckDirection);
        m_capacityText.localRotation = textRotation;
        for (int i=0;i<m_goodsTexts.Length;i++)
        {
            m_goodsTexts[i].localRotation = textRotation;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
