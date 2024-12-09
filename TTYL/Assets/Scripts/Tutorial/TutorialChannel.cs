using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialChannel", menuName = "ScriptableObjects/Tutorial/Channel")]
    public class TutorialChannel : ScriptableObject
    {
        [ShowInInspector] public bool IsModuleActive { get; private set; }
        public UnityEvent ModuleStarted = new UnityEvent();
        public UnityEvent ModuleEnded = new UnityEvent();

        public void StartTutorialModule()
        {
            IsModuleActive = true;
            ModuleStarted?.Invoke();
        }

        public void EndTutorialModule()
        {
            IsModuleActive = false;
            ModuleEnded?.Invoke();
        }
    }
}