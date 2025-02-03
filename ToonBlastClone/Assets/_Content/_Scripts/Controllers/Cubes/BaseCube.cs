using UnityEngine;

namespace YBlast
{
    public class BaseCube : MonoBehaviour
    {
        public virtual ECubeType Type => ECubeType.None;
    }
}
