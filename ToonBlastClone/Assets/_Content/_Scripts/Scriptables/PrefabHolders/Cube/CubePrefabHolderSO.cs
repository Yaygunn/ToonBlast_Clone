using UnityEngine;

namespace YBlast.Scriptables
{
    [CreateAssetMenu(menuName = "SO/Holder/CubePrefabs")]
    public class CubePrefabHolderSO : ScriptableObject
    {
        [SerializeField] private ColorCube _colorCube;


        public ColorCube ColorCube => _colorCube;
    }
}
