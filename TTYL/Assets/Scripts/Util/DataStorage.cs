using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "ScriptableObjects/Data/DataStorage")]
public class DataStorage : ScriptableObject
{
    [field: SerializeField] public string CurrentScene { get; private set; }
    [field: SerializeField] public Character Mom { get; private set; }
    [field: SerializeField] public Character Boyfriend { get; private set; }

    public void StoreRelationshipData(string sceneName, Character mom, Character boyfriend)
    {
        SetCurrentScene(sceneName);
        SetMomData(mom.CurrentStrikes, mom.RelationshipStatus);
        SetBoyfriendData(boyfriend.CurrentStrikes, boyfriend.RelationshipStatus);

        Debug.Log("RELATIONSHIP STATUS STORED");
    }

    public void SetCurrentScene(string sceneName)
    {
        CurrentScene = sceneName;
    }

    public void SetMomData(int strikes, RelationshipStatus status)
    {
        Mom.CurrentStrikes = strikes;
        Mom.RelationshipStatus = status;
    }

    public void SetBoyfriendData(int strikes, RelationshipStatus status)
    {
        Boyfriend.CurrentStrikes = strikes;
        Boyfriend.RelationshipStatus = status;
    }
}
