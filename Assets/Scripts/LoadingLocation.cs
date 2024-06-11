using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingLocation : MonoBehaviour
{

    [HideInInspector] public int[] currentStock;
    public RectTransform m_grow;
    public GameObject m_confetti;
    public GameObject[] stockObjects;
    [HideInInspector] public LocStockButton[] locStockButtons;
    public TextMeshProUGUI[] stockTextNumbers;
    private RectTransform thisTransform;
    private Vector2 originalPosition;
    private Vector3 originalScale;
    private Vector3 growScale;
    [HideInInspector] public int thisIndex;

    public bool CheckIfComplete()
    {
        int counter = 0;
        for (int i=0; i<currentStock.Length;i++)
        {
            if (currentStock[i] > 0) {
                counter++;
            }
        }
        return counter == 1;
    }

    public void RemoveGoods(int goodsIndex)
    {
        Debug.Assert(currentStock[goodsIndex]>0,"Error. the goods button should be disabled at 0 goods");
        currentStock[goodsIndex]--;
        stockObjects[goodsIndex].SetActive(currentStock[goodsIndex] > 0);
        stockTextNumbers[goodsIndex].text = "" + currentStock[goodsIndex];
    }

    public void AddGoods(int goodsIndex)
    {
        currentStock[goodsIndex]++;
        stockObjects[goodsIndex].SetActive(currentStock[goodsIndex] > 0);
        stockTextNumbers[goodsIndex].text = "" + currentStock[goodsIndex];
    }

    public void PickLocation(bool picked)
    {
        if (picked)
        {
            thisTransform.anchoredPosition = m_grow.anchoredPosition;
            thisTransform.localScale = growScale;
        }
        else
        {
            thisTransform.anchoredPosition = originalPosition;
            thisTransform.localScale = originalScale;
        }
    }

    public void RefillStockLocation(int[] newStock)
    {
        currentStock = newStock;
        for (int i = 0; i < currentStock.Length; i++)
        {
            stockObjects[i].SetActive(currentStock[i] > 0);
            stockTextNumbers[i].text = "" + currentStock[i];
        }
    }

    public void InitiateStockLocation(int[] iniStock, int index)
    {
        thisIndex = index;
        locStockButtons = new LocStockButton[stockObjects.Length];
        for (int i = 0; i < stockObjects.Length; i++)
        {
            locStockButtons[i] = stockObjects[i].GetComponent<LocStockButton>();
            locStockButtons[i].InitiateButton();
            locStockButtons[i].thisIndex = i;
            locStockButtons[i].parentIndex = thisIndex;
        }

        thisTransform = GetComponent<RectTransform>();
        originalPosition = new Vector2(thisTransform.anchoredPosition.x, thisTransform.anchoredPosition.y);
        originalScale = thisTransform.localScale;
        growScale = new Vector3(1.8f * originalScale.x, 1.8f * originalScale.y, 1.8f * originalScale.z);

        RefillStockLocation(iniStock);
        m_grow.gameObject.SetActive(false); //grow location only visible in editor
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
