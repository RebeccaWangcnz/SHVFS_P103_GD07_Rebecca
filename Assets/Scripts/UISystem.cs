using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UISystem : MonoBehaviour
{
    public GameObject guide;
    private void Awake()
    {
        guide.SetActive(false);
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("BattleBear_decorated");
    }
    public void ShowGuide()
    {
        guide.SetActive(true);
    }
    public void CloseGuide()
    {
        guide.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
