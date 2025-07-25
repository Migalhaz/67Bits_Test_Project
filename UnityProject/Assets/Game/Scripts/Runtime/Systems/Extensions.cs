using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public static class Extensions 
    {
        static Dictionary<float, WaitForSeconds> m_waitForSeconds;
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            m_waitForSeconds ??= new Dictionary<float, WaitForSeconds>();
            if (m_waitForSeconds.TryGetValue(seconds, out WaitForSeconds result))
            {
                return result;
            }

            WaitForSeconds newWaitForSeconds = new WaitForSeconds(seconds);
            m_waitForSeconds.Add(seconds, newWaitForSeconds);
            return newWaitForSeconds;
        }

        public static Vector3 With(this Vector3 _vector3, float? x = null, float? y = null, float? z = null)
        {
            float newX = x is null ? _vector3.x : (float)x;
            float newY = y is null ? _vector3.y : (float)y;
            float newZ = z is null ? _vector3.z : (float)z;
            _vector3.Set(newX, newY, newZ);
            return _vector3;
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            int randomIndex = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(randomIndex);
        }
    }


}
