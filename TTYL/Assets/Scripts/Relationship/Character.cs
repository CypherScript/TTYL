using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    public RelationshipStatus RelationshipStatus;
    public Characters CurrentCharacter;
    public int CurrentStrikes = 0;
    public int LowThreshold = 1;
    public int HighThreshold = 3;

    private void Awake()
    {
        CheckThresholdValues();
    }

    public void SetRelationshipStatus(RelationshipStatus relationshipStatus)
    {
        RelationshipStatus = relationshipStatus;
    }

    public void ResetCharacter()
    {
        CurrentStrikes = 0;
        RelationshipStatus = RelationshipStatus.Good;
    }

    public void ApplyStrike(int damage = 1)
    {
        CurrentStrikes += damage;

        CheckRelationshipStatus();
    }

    public void RemoveStrike(int damage = 1)
    {
        CurrentStrikes -= damage;

        if (CurrentStrikes <= 0)
        {
            ResetCharacter();
            return;
        }

        CheckRelationshipStatus();
    }

    private void CheckRelationshipStatus()
    {
        if (CurrentStrikes <= LowThreshold)
            RelationshipStatus = RelationshipStatus.Good;
        else if (CurrentStrikes > LowThreshold && CurrentStrikes <= HighThreshold)
            RelationshipStatus = RelationshipStatus.Medium;
        else if(CurrentStrikes > HighThreshold)
            RelationshipStatus = RelationshipStatus.Bad;

        Debug.Log(CurrentCharacter + " CURRENT STRIKES: " + CurrentStrikes);
        Debug.Log("CURRENT RELATIONSHIP STATUS: " + RelationshipStatus);
    }

    private void CheckThresholdValues()
    {
        if (LowThreshold <= 0 || HighThreshold <= 0)
        {
            LowThreshold = 1;
            HighThreshold = 3;

            Debug.Log("THRESHOLD VALUES CANNOT BE 0!!");
            Debug.Log("BOTH THRESHOLD VALUES ADJUSTE TO THEIR DEFAULT VALUE");
            Debug.Log("DEFAULY THRESHOLD VALUES: " + LowThreshold + ", " + HighThreshold);
        }


        if (LowThreshold >= HighThreshold)
        {
            LowThreshold = HighThreshold - 2;

            if (LowThreshold <= 0)
                LowThreshold = 1;

            Debug.Log("LOW THRESHOLD VALUE CANNOT BE HIGHER THAN HIGH THRESHOLD VALUE!");
            Debug.Log("LOW THRESHOLD VALUE ADJUSTED TO: " + LowThreshold);
        }

        if (HighThreshold <= LowThreshold)
        {
            HighThreshold = LowThreshold + 2;

            if (LowThreshold <= 0)
                LowThreshold = 1;

            Debug.Log("HIGH THRESHOLD VALUE CANNOT BE LOWER THAN LOWER THRESHOLD VALUE!");
            Debug.Log("HIGH THRESHOLD VALUE ADJUSTED TO: " + HighThreshold);
        }
    }
}

public enum RelationshipStatus
{
    Good,
    Medium,
    Bad
}
