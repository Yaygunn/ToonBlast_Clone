using UnityEngine;
using UnityEngine.Serialization;

namespace YBlast.Data
{
    [System.Serializable]
    public class Goals
    {
        [SerializeField] private SGoal _goal;

        public SGoal GetGoal()
        {
            return _goal;
        }
    }

    [System.Serializable]
    public struct SGoal
    {
        public ECubeColor DesiredCubeColor;
        public int Amount;
    }
}
