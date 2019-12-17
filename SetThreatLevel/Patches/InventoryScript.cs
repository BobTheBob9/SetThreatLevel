using Harmony;

namespace SetThreatLevel.Patches {
    [HarmonyPatch(typeof(InventoryScript), "Start")]
    public class Patch_InventoryScript_Start {
        private static void Postfix(InventoryScript __instance) {
            ThreatLevel.Inventory = __instance;
        }
    }
}
