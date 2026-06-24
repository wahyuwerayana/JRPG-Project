using UnityEngine;

namespace Game.Utils {
    public class CombatUtils {
        public static float CalculateDamage(float attack, float defense) {
            return Mathf.Max(1, attack - defense);
        }
    }
}