using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ReportCard", menuName = "ScriptableObjects/Scoring/ReportCard")]
public class ReportCard : ScriptableObject
{
    [BoxGroup("Scores")] public int GroupChatScore;
    [BoxGroup("Scores")] public RelationshipStatus MomRelationshipStatus;
    [BoxGroup("Scores")] public RelationshipStatus BoyfriendRelationshipStatus;
    
    [field: BoxGroup("Popularity Grade Thresholds")] [field: SerializeField] public int PopularityGradeThresholdA { get; private set; }
    [field: BoxGroup("Popularity Grade Thresholds")] [field: SerializeField] public int PopularityGradeThresholdB { get; private set; }

    [Button][GUIColor(1, 0, 0)]
    public void Clean()
    {
        GroupChatScore = 0;
        MomRelationshipStatus = RelationshipStatus.Good;
        BoyfriendRelationshipStatus = RelationshipStatus.Good;
    }
    
    public Grade GetPopularityGrade()
    {
        if (GroupChatScore >= PopularityGradeThresholdA)
        {
            return Grade.A;
        }

        if (GroupChatScore >= PopularityGradeThresholdB)
        {
            return Grade.B;
        }
        
        return Grade.C;
    }
    
    public Grade GetMomGrade()
    {
        return MomRelationshipStatus switch
        {
            RelationshipStatus.Good => Grade.A,
            RelationshipStatus.Medium => Grade.B,
            RelationshipStatus.Bad => Grade.C,
            _ => Grade.C
        };
    }

    public Grade GetBoyfriendGrade()
    {
        return BoyfriendRelationshipStatus switch
        {
            RelationshipStatus.Good => Grade.S,
            _ => Grade.F
        };
    }
}

public enum Grade
{
    A = 0,
    B = 1,
    C = 2,
    S = 3,
    F = 4
}