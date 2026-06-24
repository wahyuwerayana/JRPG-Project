using Game.Managers;

namespace Game.Gameplay {
    public class Player : UnitCombatBase {
        public void Attack(IDamagable target) {
            GameEventManager.Instance.BattleEvent.RaiseOnUnitAttack(this, Stats.Attack);
            
            target.TakeDamage(Stats.Attack);
        }
    }
}