using System;
using UnityEngine;

namespace Game.Gameplay {
    [Serializable]
    public class Wave {
       [field: SerializeField] public EnemyWave[] enemyInfos { get; private set; }
    }

    [Serializable]
    public class EnemyWave {
        [field: SerializeField] public GameObject enemy { get; private set; }
        [field: SerializeField] public int enemyCount { get; private set; }
    }
}