using UnityEngine;

namespace YBlast.Utilities
{
    public static class Extensions 
    {
        public static Vector2Int Add(this Vector2Int vector ,int xChangeAmount, int yChangeAmount)
        {
            vector.x += xChangeAmount;
            vector.y += yChangeAmount;
            return vector;
        }

        public static Vector3 Add(this Vector3 vector, float xChange, float yChange, float zChange = 0)
        {
            vector.x += xChange;
            vector.y += yChange;
            vector.z += zChange;
            return vector;
        }
        
        public static int GetMultiplication(this Vector2Int vector)
        {
            return vector.x * vector.y;
        }
    }
}
