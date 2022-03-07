using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TeamPointSystem : Singleton<TeamPointSystem>
{
    //private Dictionary<int, List<ScorerComponent>> teams = new Dictionary<int, List<ScorerComponent>>();
    public List<Team> teams = new List<Team>();
   // List<Zone> zones = new List<Zone>();
    public int scorePerTick;
    public int tick;
    public override void Awake()
    {
        base.Awake();
        var scoreComponents = FindObjectsOfType<GetScores>();
        foreach (var scoreComponent in scoreComponents)
        {
            var matchingTeamIndex = -1;
            for (var i = 0; i < teams.Count; i++)
            {
                if (teams[i].ID.Equals(scoreComponent.teamID))
                {
                    matchingTeamIndex = i;
                }
            }
            if (matchingTeamIndex < 0)
            {
                var team = new Team()
                {
                    ID = scoreComponent.teamID,
                    Members = new List<GetScores> { scoreComponent }
                };
                teams.Add(team);
            }
            else
            {
                if (teams[matchingTeamIndex].Members.Contains(scoreComponent)) return;
                teams[matchingTeamIndex].Members.Add(scoreComponent);
            }

           // With LINQ
            //if (!teams.Any(team => team.ID.Equals(scoreComponent.TeamID)))
            //{
            //    var team = new Team()
            //    {
            //        ID = scoreComponent.TeamID,
            //        Members = new List<GetScores> { scoreComponent }
            //    };
            //    teams.Add(team);
            //}
            //else
            //{
             //  var team = teams.FirstOrDefault(team => team.ID.Equals(scoreComponent.TeamID));
            //    if (team.Members.Contains(scoreComponent)) return;
            //    team.Members.Add(scoreComponent);
            //}
        }
    }
    public void ScorePoints(int teamID, ref float timer)
    {
        for (var i = 0; i < teams.Count; i++)
        {
            if (teams[i].ID.Equals(teamID))
            {
             if (timer < tick)
             {
                     timer += Time.deltaTime;
              }
            else
             {
                    timer = 0;
                    teams[i].Score += scorePerTick;
                }
            }
        }
    }
}
[Serializable]
public class Team
{
    public int ID;
    public List<GetScores> Members;
    public int Score;
}

