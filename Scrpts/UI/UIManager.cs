using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get 
        { 
            if( _instance == null)
            {
                Debug.LogError("UiManager is not present!");
            }
            return _instance;
        }
    }
    public Text playerGemCountText;

    public Image selection;

    public Text gemCountText;

    public GameObject[] lives;

    public GameObject DeathScreen;

    public GameObject WinScreen;

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount.ToString() + "G";
    }

    public void UpdateSelection(int yPos)
    {
        selection.rectTransform.anchoredPosition = new Vector2(selection.rectTransform.anchoredPosition.x, yPos);
    }

    public void TakeDamage(float health)
    {
        if (health <= 0)
        {
            for (int i = 0; i <= lives.Length - 1; i++)
            {
                lives[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i <= health; i++)
            {
                if (i == health)
                {                        
                    lives[i].SetActive(false);
                }
            }
        }
    }

    public void Heal(float health)
    {
        for (int i = 0; i < health; i++)
        {
            if (!lives[i].activeInHierarchy)
            {
                lives[i].SetActive(true);
            }
        }
    }

    public void StartDeathScreen()
    {
        DeathScreen.SetActive(true);
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void MainMenu()
    {
        GameManager.Instance.LoadScene(0);
    }

    public void Win()
    {
        if(WinScreen != null)
        {
            WinScreen.SetActive(true);
            StartCoroutine(LoadMenu());
        }
    }
    
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(12f);
        MainMenu();
    }

    private void Awake()
    {
        _instance = this;
    }
}
