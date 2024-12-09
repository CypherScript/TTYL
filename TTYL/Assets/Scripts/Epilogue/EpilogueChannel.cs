using UnityEngine;
using UnityEngine.Events;

namespace Epilogue
{
    [CreateAssetMenu(fileName = "EpilogueChannel", menuName = "ScriptableObjects/Epilogue/Channel")]
    public class EpilogueChannel : ScriptableObject
    {
        public UnityEvent GradeRevealed = new UnityEvent();
    }
}