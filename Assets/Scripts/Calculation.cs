using UnityEngine;

namespace Assets
{
    public static class Calculation
    {
        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            var v = vec2 - vec1;
            return Mathf.Sqrt(v.x * v.x + v.y * v.y);
        }

        public static float Radian(Vector3 vec1, Vector3 vec2)
        {
            return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
        }
    }
}
