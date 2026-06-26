using UnityEngine;

namespace Game.Utils {
    public static class CombatUtils {
        /// <summary>
        /// Calculates the damage dealt based on attack and defense values, with an optional variance.
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="variance"></param>
        /// <returns></returns>
        public static float CalculateDamage(float attack, float defense, float variance = 0.1f) {
            float baseDamage = Mathf.Max(1f, attack - defense);
            float varianceAmount = baseDamage * variance;
            float randomVariance = Random.Range(-varianceAmount, varianceAmount);
            float finalDamage = baseDamage + randomVariance;

            return Mathf.Round(Mathf.Max(1f, finalDamage));
        }
    }
}