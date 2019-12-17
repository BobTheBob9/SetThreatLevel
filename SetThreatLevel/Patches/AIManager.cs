using Harmony;

namespace SetThreatLevel.Patches {
    [HarmonyPatch(typeof(AIManager), "Start")]
    public class Patch_AIManager_Start {
        private static void Postfix(AIManager __instance) {
            ThreatLevel.Manager = __instance;
        }
    }
}
