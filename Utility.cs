using UnityEngine;

namespace WeaponSharpening
{
    public static class Utility
    {
        public static void Log(string message)
        {
            Debug.Log(string.Format("[WeaponSharpening] {0}", message));
        }
    }
}
