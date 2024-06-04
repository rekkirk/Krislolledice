using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneDie : MonoBehaviour 
{
    public MoveControl m_moveControl;
    public Sprite[] m_diceSprites;
    [HideInInspector] public Image m_displayDie;
    [HideInInspector] public int value;
    [HideInInspector] public int dieID;
    


    // Start is called before the first frame update
    void Start()
    {
        m_displayDie = GetComponent<Image>();
    }

    public int Reroll()
    {
        value = Random.Range(1,7);
        m_displayDie.sprite = m_diceSprites[value - 1];
        return value;
    }
    
    public void DiePicked()
    {
        m_moveControl.MovePicked(this);
    }

    public void MoveTruck()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
