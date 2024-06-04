using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionButton : MonoBehaviour
{

    [HideInInspector] public Button m_thisButton;
    public MoveControl m_moveControl;
    [HideInInspector] public OneDie die;
    [HideInInspector] public Sprite[] diceSprites;
    public TruckDirection m_truckDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 getTruckLoadingLocation()
    {
        return GetComponent<RectTransform>().position;
        //return new Vector2(m_roadPiece.position.x, m_roadPiece.position.y);
        //return new Vector2(thisLocation.x ,thisLocation.y);

    }

    public void InitiatePositionButton(Sprite[] sprites)
    {
        m_thisButton = GetComponent<Button>();
        diceSprites = sprites;
    }

    public void ActivatePositionDie(OneDie newdie)
    {
        die = newdie;
        GetComponent<Image>().sprite = diceSprites[die.value-1];
    }

    public void DiePicked()
    {
        die.DiePicked();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
