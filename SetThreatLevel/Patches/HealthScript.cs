using Harmony;

namespace SetThreatLevel.Patches {
    [HarmonyPatch(typeof(HealthScript), "Start")]
    public class Patch_HealthScript_Start {
        private static void Postfix(HealthScript __instance) {
            ThreatLevel.Health = __instance;
        }
    }
}
