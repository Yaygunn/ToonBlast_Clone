using UnityEngine;

namespace YBlast.Scriptables
{
    [CreateAssetMenu(menuName = "SO/LayerMasks")]
    public class LayerMasksSO : ScriptableObject
    {
        [SerializeField] private LayerMask _cubeLayerMask;

        public LayerMask CubeLayerMask => _cubeLayerMask;
    }
}
