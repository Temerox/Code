using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject endPanel;
    public TextMeshProUGUI[] countText;
    public GameObject[] pauseUI; // 0 IS BUTTON 1 IS PANEL

    //public Text colorchangingFont;

    //public class LivesUI : MonoBehaviour
    //{
   //     public PlayerController playerController;
   //     public Text livesText;

      //       void Update()
        //{
           // livesText.text = "Lives: " + (3 - playerController.enemyHits).ToString();
     //   }
       //      }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   // void Update()
    //{
        
   // }

    public void TransitionScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI[0].SetActive(false);
        pauseUI[1].SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseUI[0].SetActive(true);
        pauseUI[1].SetActive(false);
    }

    public void LoseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game over, returning to main menu for level select";
        
        Invoke("ResetTimeScale", 1.5f);
        Invoke("TransitionScene0", 1.5f);
        

    }

    public void WinGame()
    {

    if (SceneManager.GetActiveScene().buildIndex == 2)
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level two Complete, advancing now....!";
        Time.timeScale = 0.5f;
        Invoke("ResetTimeScale", 1.5f);
        Invoke("TransitionScene4", 1.5f);
    }
    
    else if (SceneManager.GetActiveScene().buildIndex == 1)
    {
    endPanel.SetActive(true);
    endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level One Complete, advancing now....!";
    Time.timeScale = 0.5f;
    Invoke("ResetTimeScale", 1.5f);
    Invoke("TransitionScene2", 1.5f);
    }
    
    }

    private void TransitionScene2()
    {
    SceneManager.LoadScene(2);
    
    }

    private void TransitionScene4()
    {
    SceneManager.LoadScene(4);
    }

    private void TransitionScene0()
    {
    SceneManager.LoadScene(0);
    }
    

    private void ResetTimeScale()
{
    Time.timeScale = 1;
}

}
