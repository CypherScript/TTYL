using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

public class RelationshipManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner _groupDialogueRunner = null;
    [SerializeField] private DialogueRunner _momDialogueRunner = null;
    [SerializeField] private DialogueRunner _boyfriendDialogueRunner = null;
    [SerializeField] private PhoneCallChannel _phoneCallChannel = null;
    [SerializeField] private PromptChannel _momPromptChannel = null;
    [SerializeField] private PromptChannel _boyfriendPromptChannel = null;
    [FormerlySerializedAs("_gameManagerChannel")] [SerializeField] private ReportCard reportCard = null;
    [SerializeField] private DayChannel _dayChannel = null;
    [SerializeField] private DataStorage _storedData = null;
    [SerializeField] private FloatReference _popularity = null;
    [SerializeField] private Character _mom = null;
    [SerializeField] private Character _boyfriend = null;

    private void Awake()
    {
        _groupDialogueRunner.AddCommandHandler<bool>("MomCallPlayer", MomCallPlayer);
        _groupDialogueRunner.AddCommandHandler<bool>("BoyfriendCallPlayer", BoyfriendCallPlayer);
        _momDialogueRunner.AddCommandHandler<bool>("MomCallPlayer", MomCallPlayer);
        _boyfriendDialogueRunner.AddCommandHandler<bool>("BoyfriendCallPlayer", BoyfriendCallPlayer);

        StoreData();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        _phoneCallChannel.PhoneCallAnswered.AddListener(OnPhoneAnswered);
        _phoneCallChannel.PhoneCallEnded.AddListener(OnPhoneNotAnswered);
        _momPromptChannel.promptEnded.AddListener(OnMomPromptResults);
        _boyfriendPromptChannel.promptEnded.AddListener(OnBoyfriendPromptResults);
        _dayChannel.DayEnded.AddListener(GetCharacterRelationshipAtEndOfDay);
        _popularity.ValueChanged.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        _phoneCallChannel.PhoneCallAnswered.RemoveListener(OnPhoneAnswered);
        _phoneCallChannel.PhoneCallEnded.RemoveListener(OnPhoneNotAnswered);
        _momPromptChannel.promptEnded.RemoveListener(OnMomPromptResults);
        _boyfriendPromptChannel.promptEnded.RemoveListener(OnBoyfriendPromptResults);
        _dayChannel.DayEnded.RemoveListener(GetCharacterRelationshipAtEndOfDay);
        _popularity.ValueChanged.RemoveListener(OnGameOver);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void StoreData()
    {
        _storedData.StoreRelationshipData(SceneManager.GetActiveScene().name, _mom, _boyfriend);
    }

    private void OnGameOver(float value)
    {
        if (_popularity.Value != 0) return;

        _mom = _storedData.Mom;
        _boyfriend = _storedData.Boyfriend;
    }

    public void MomCallPlayer(bool ignoresRelationship)
    {
        if (_mom.RelationshipStatus == RelationshipStatus.Good && !ignoresRelationship)
        {
            Debug.Log("MOM NOT CALLING BECAUSE THE RELATIONSHIP IS: " + _mom.RelationshipStatus);
            return;
        }

        _phoneCallChannel.StartPhoneCall(Characters.Mom);
    }

    public void BoyfriendCallPlayer(bool ignoresRelationship)
    {
        if (_boyfriend.RelationshipStatus == RelationshipStatus.Good && !ignoresRelationship)
        {
            Debug.Log("MOM NOT CALLING BECAUSE THE RELATIONSHIP IS: " + _boyfriend.RelationshipStatus);
            return;
        }

        _phoneCallChannel.StartPhoneCall(Characters.Boyfriend);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Day0" || scene.name != "Day1") return;

        Debug.Log("RESETTING CHARACTERS");

        _mom.ResetCharacter();
        _boyfriend.ResetCharacter();
    }

    private void OnPhoneAnswered(Characters caller)
    {
        ManageCharacterRelationShip(caller, false);
    }

    private void OnPhoneNotAnswered(bool wasAnswered)
    {
        if (wasAnswered) return;

        ManageCharacterRelationShip(_phoneCallChannel.CurrentCaller, true);
    }

    private void OnMomPromptResults(PromptResult result)
    {
        if (result == PromptResult.GoodReply) return;

        ManageCharacterRelationShip(_mom.CurrentCharacter, true);
    }

    private void OnBoyfriendPromptResults(PromptResult result)
    {
        if (result == PromptResult.GoodReply) return;

        ManageCharacterRelationShip(_boyfriend.CurrentCharacter, true);
    }

    private void ManageCharacterRelationShip(Characters character, bool isApplyingStrike, int value = 1)
    {
        Character person = null;

        switch(character)
        {
            case Characters.Mom:
                person = _mom;
                break;
            case Characters.Boyfriend:
                person = _boyfriend;
                break;
        }

        if (isApplyingStrike)
            person.ApplyStrike(value);
        else
            person.RemoveStrike(value);
    }

    private void GetCharacterRelationshipAtEndOfDay()
    {
        reportCard.MomRelationshipStatus = _mom.RelationshipStatus;
        reportCard.BoyfriendRelationshipStatus = _boyfriend.RelationshipStatus;
    }

    [Button]
    private void DebugCallPlayer()
    {
        MomCallPlayer(true);
    }

    [Button]
    private void HangPhone()
    {
        _phoneCallChannel.EndPhoneCall(false);
    }

    [Button]
    private void MomAddStrike(int value)
    {
        _mom.ApplyStrike(value);
    }

    [Button]
    private void MomRemoveStrike(int value)
    {
        _mom.RemoveStrike(value);
    }
}
