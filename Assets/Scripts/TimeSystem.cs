using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeSystem : MonoBehaviour
{
    public int timeInTotal;
    //public TeamPointSystem teamPointSystem;
    private float timer;
    private float currentTime;
    public Text timeText;
    //public Text player1_score;
    //public Text player2_score;
    public GameObject endPage;
    private bool isEnd=false;//ensure gameover do once
    private void Start()
    {
        endPage.SetActive(false);
        var teams = TeamPointSystem.Instance;
        currentTime = timeInTotal;
        timeText.text = timeInTotal.ToString();
    }
    private void Update()
    {
        //show score
        var teams = TeamPointSystem.Instance;
        for(int i=0;i<teams.teams.Count;i++)
        {
            teams.teams[i].scoreText.text = teams.teams[i].Score.ToString();
        }
        if (currentTime > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                currentTime -= 1;
                timeText.text = currentTime.ToString();
                timer = 0;
            }
        }
        else if (!isEnd)
        {
            GameOver();
        }
        if(isEnd&&Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            Destroy(TeamPointSystem.Instance);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           
        }
        if (isEnd && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Destroy(TeamPointSystem.Instance);
            SceneManager.LoadScene("StartScene");
        }
    }
    public void GameOver()
    {
             isEnd = true;
            Time.timeScale = 0;
            endPage.SetActive(true);
            //endPage.transform.GetChild(1).GetComponent<Text>().text = $"";
            Debug.Log("game over!");
            var teams = TeamPointSystem.Instance;
             for (var i = teams.teams.Count-1; i >=0; i--)
            {
                Debug.Log($"teams:{teams.teams[i].ID}, scores: {teams.teams[i].Score}");
                endPage.transform.GetChild(1).GetComponent<Text>().text += $"{teams.teams[i].Score}";
                if (i != 0)
                {
                    endPage.transform.GetChild(1).GetComponent<Text>().text += " : ";
                }
            }
        }
}
