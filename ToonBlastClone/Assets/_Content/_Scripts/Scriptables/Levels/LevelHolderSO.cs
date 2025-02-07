using UnityEngine;

namespace YBlast.Scriptables
{
    [CreateAssetMenu(menuName = "SO/Holder/Level")]
    public class LevelHolderSO : ScriptableObject
    {
        [SerializeField] private LevelDataSO[] _levelDatas;

        public LevelDataSO GetLevel(int desiredLevelIndex)
        {
            desiredLevelIndex %= _levelDatas.Length;

            return _levelDatas[desiredLevelIndex];
        }
    }
}
