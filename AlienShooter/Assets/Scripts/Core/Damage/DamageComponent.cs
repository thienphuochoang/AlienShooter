using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent : MonoBehaviour
{
    [SerializeField] protected bool damageFriendly;
    [SerializeField] protected bool damageEnemy;
    [SerializeField] protected bool damageNeutral;
    [SerializeField] private GameObject _teamInterfaceSource;
    private ITeam _teamInterface;

    public void SetTeamInterfaceSource(ITeam teamInterface)
    {
        _teamInterface = teamInterface;
    }
    public bool ShouldDamage(GameObject other)
    {
        if (_teamInterface == null) 
            return false;
        
        TeamRelation relation = _teamInterface.GetRelationTowards(other);
        if (damageFriendly && relation == TeamRelation.Friendly)
            return true;
        if (damageEnemy && relation == TeamRelation.Enemy)
            return true;
        if (damageNeutral && relation == TeamRelation.Neutral)
            return true;
        
        return false;
    }
}
