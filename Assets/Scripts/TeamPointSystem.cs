using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class TeamPointSystem : Singleton<TeamPointSystem>
{
    public List<Team> teams = new List<Team>();
    public override void Awake()
    {
        base.Awake();
        var players = FindObjectsOfType<PlayerController>();
        // foreach all the player
        foreach (var player in players)
        {
            var matchingTeamIndex = -1;
            for (var i = 0; i < teams.Count; i++)
            {
                if (teams[i].ID.Equals(player.teamID))
                {
                    matchingTeamIndex = i;
                }
            }
            if (matchingTeamIndex < 0)
            {
                var team = new Team()
                {
                    ID = player.teamID,
                    Member = player,
                    zone = player.zone
                };
                teams.Add(team);
            }
            else
            {
                if (teams[matchingTeamIndex].Member==player) return;
                teams[matchingTeamIndex].Member=player;
                teams[matchingTeamIndex].zone= player.zone;
                teams[matchingTeamIndex].Score= 0;
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
        // var scoreTexts = FindObjectsOfType<ScoreText>();
        var scoreTexts = FindObjectsOfType<ScoreText>();
        foreach (var scoreText in scoreTexts)
        {
            for (var i = 0; i < teams.Count; i++)
            {
                if (teams[i].ID.Equals(scoreText.index))
                {
                    teams[i].scoreText = scoreText.GetComponent<Text>();
                }
            }
        }
    }
    private void Update()
    {
        GetScore();
    }
    private void GetScore()
    {
        for (var i = 0; i < teams.Count; i++)
        {
            teams[i].Score = teams[i].zone.scoreContain;
        }
    }
}


[Serializable]
public class Team
{
    public int ID;
    public PlayerController Member;
    public Zone zone;
    public int Score;
    public Text scoreText;
}

