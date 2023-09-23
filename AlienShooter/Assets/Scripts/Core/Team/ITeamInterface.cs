using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamRelation
{
    Friendly,
    Enemy,
    Neutral
}
public interface ITeam
{
    public int GetTeamID()
    {
        return -1;
    }

    public TeamRelation GetRelationTowards(GameObject other)
    {
        ITeam otherTeamInterface = other.GetComponent<ITeam>();
        if (otherTeamInterface == null)
            return TeamRelation.Neutral;

        if (otherTeamInterface.GetTeamID() == GetTeamID())
            return TeamRelation.Friendly;
        else if (otherTeamInterface.GetTeamID() == -1 || GetTeamID() == -1)
            return TeamRelation.Neutral;
        return TeamRelation.Enemy;
    }
}
