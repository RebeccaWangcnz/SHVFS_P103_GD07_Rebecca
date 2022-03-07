using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int minNum;
    float timer = 0f;
    public Dictionary<int, int> teamMember = new Dictionary<int, int>();
    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < TeamPointSystem.Instance.teams.Count; i++)
        {
            teamMember.Add(TeamPointSystem.Instance.teams[i].ID, 0);
        }
    }

    public void ChangeMember(int teamID, bool isAdd)
    {
        if(isAdd)
            teamMember[teamID] += 1;
        else
            teamMember[teamID] -= 1;
        //foreach (KeyValuePair<int,int> kvp in teamMember)
        //{
        //    Debug.Log($"Key={kvp.Key}, Value={kvp.Value}");
        //}
    }
    private void Update()
    {
      // Debug.Log( AddScore());
        if(AddScore()!=-1)
        {
            TeamPointSystem.Instance.ScorePoints(AddScore(), ref timer);
            //for (var i = 0; i < TeamPointSystem.Instance.teams.Count; i++)
            //{
            //    if (TeamPointSystem.Instance.teams[i].ID == AddScore())
            //        TeamPointSystem.Instance.teams[i].cubeNum++;
            //    else
            //        TeamPointSystem.Instance.teams[i].cubeNum--;
            //}
        }
    }
    public int AddScore()
    {
        foreach (KeyValuePair<int, int> kvp in teamMember)
        {
            bool canScore=false;
            if(kvp.Value >=minNum)
            {
                canScore = true;
                foreach (KeyValuePair<int, int> ikvp in teamMember)
                {
                    if(ikvp.Key!=kvp.Key&&ikvp.Value!=0)
                    {
                        canScore = false;
                    }
                }
                if(canScore==true)
                {
                    return kvp.Key;
                }
            }
           // Debug.Log($"Key={kvp.Key}, Value={kvp.Value}");
        }
        return -1;
    }
}
