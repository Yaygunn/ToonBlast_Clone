using UnityEngine;

namespace YBlast.Scriptables
{
    [CreateAssetMenu(menuName = "SO/Settings/GridSpacing")]
    public class SpacingSettingsSO : ScriptableObject
    {
        [SerializeField] private float _cellSize = 1;

        [SerializeField] private float _cellSpacing = 1;

        [SerializeField] private float _spawnYOffset = 1;

        
        public float CellSize => _cellSize;

        public float CellSpacing => _cellSpacing;

        public float SpawnYOffset => _spawnYOffset;
    }
}
