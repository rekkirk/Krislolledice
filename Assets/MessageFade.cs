using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MessageFade : MonoBehaviour
{
    public double m_fadeTime;
    private System.DateTime targetTime;
    private bool timerOn = false;
    public TextMeshProUGUI m_messageText;
    public GameObject m_playAgainButton;
    public GameObject m_confetti;

    public void SetText(string message, bool gameIsOver, bool didWin)
    {
        timerOn = !gameIsOver;
        m_playAgainButton.SetActive(gameIsOver);
        m_confetti.SetActive(gameIsOver && didWin);
        targetTime = System.DateTime.Now.AddSeconds(m_fadeTime);
        
        gameObject.SetActive(true);
        m_messageText.text = message;
    }

    public void RestartGame()
    {
        m_confetti.SetActive(false);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (System.DateTime.Now > targetTime)
            {
                gameObject.SetActive(false);
                timerOn = false;
            }
        }
    }
}
