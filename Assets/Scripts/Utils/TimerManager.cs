using System.Collections.Generic;

namespace Game.Utils {
    public static class TimerManager {
        private static readonly List<Timer> timers = new List<Timer>();

        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void UnregisterTimer(Timer timer) => timers.Remove(timer);

        public static void UpdateTimers() {
            foreach(Timer timer in new List<Timer>(timers)) {
                timer.Tick();
            }
        }

        public static void Clear() => timers.Clear();
    }
}