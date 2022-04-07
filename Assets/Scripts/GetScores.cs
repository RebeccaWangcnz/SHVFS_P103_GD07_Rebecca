using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetScores : MonoBehaviour
{
    public int scores;
    public int tick;
    public int teamID;
    float timer=0;
  //  public Text scoreText;

    private void Start()
    {
        scores = 0;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Zone>())
        {
            //other.GetComponent<Zone>().ChangeMember(teamID,true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Zone>())
        {
           //other.GetComponent<Zone>().ChangeMember(teamID,false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Zone>())
        {
            //other.GetComponent<Zone>().AddMember(TeamID);
            //AddScores();
        }
    }
    //private void AddScores()
    //{
    //    if (timer < tick)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        timer = 0f;
    //       // scores += x;
    //       // scoreText.text = "Scores: " + scores.ToString();
    //        //TeamPointSystem.Instance.ScorePoints();
    //    }
    //}
}
