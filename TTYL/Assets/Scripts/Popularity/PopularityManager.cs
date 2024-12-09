using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    [SerializeField] private PromptChannel _promptChannel = null;
    [SerializeField] private PopularitySettings _popularitySettings = null;

    [field: SerializeField] public FloatReference Popularity { get; private set; }

    private void Awake()
    {
        ResetPopularity();
    }

    private void OnEnable()
    {
        _promptChannel.promptEnded.AddListener(DecreasePopularity);
    }

    private void OnDisable()
    {
        _promptChannel.promptEnded.RemoveListener(DecreasePopularity);
    }

    private void ResetPopularity()
    {
        Debug.Log("CALLED RESET POPULARITY");
        Popularity.Value = _popularitySettings.StartingPopularity;
    }

    private void DecreasePopularity(PromptResult result)
    {
        switch (result)
        {
            case PromptResult.BadReply:
                Popularity.Value = Mathf.Max(Popularity.Value - _popularitySettings.InaccurateReplyDamage, _popularitySettings.MinPopularity);
                break;
            case PromptResult.NoReply:
                Popularity.Value = Mathf.Max(Popularity.Value - _popularitySettings.InaccurateReplyDamage, _popularitySettings.MinPopularity);
                break;
            default:
                break;
        }
    }
}
