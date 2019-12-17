using UnityEngine;
using System.Collections.Generic;

namespace SetThreatLevel {
    public class ThreatLevel : MonoBehaviour {
        public static InventoryScript Inventory;
        public static HealthScript Health;
        public static AIManager Manager;

        private static int QueuedThreat;
        public static void Set(int level) {
            QueuedThreat = level;
        }

        private void Update() {
            bool isSpawning = false;
            if (Manager != null)
                isSpawning = Manager.IsSpawning;

            if (QueuedThreat != -1 && Inventory != null && Health != null && isSpawning) {
                Manager.CurrentLevel = QueuedThreat;
                float timeToNext = 20f;
                for (int i = 0; i < QueuedThreat; i++)
                    timeToNext *= 1.75f;
                Manager.StopAllCoroutines();
                Manager.LevelWaitTime = timeToNext;
                Manager.CurrentSpawnObject = Manager.Spawners[Manager.CurrentLevel];
                if (Manager.CurrentLevel < 7) {
                    if (Manager.CurrentLevel == 6) {
                        Manager.Player.GetComponent<InventoryScript>().PrintFancyMessage("THREAT LEVEL " + Manager.CurrentLevel.ToString() + " --- ENEMY DAMAGE DOUBLED");
                        GameManager.GM.Player.GetComponent<HealthScript>().DamageMultiplier = 2f;
                    } else {
                        Manager.Player.GetComponent<InventoryScript>().PrintFancyMessage("THREAT LEVEL " + Manager.CurrentLevel.ToString());
                    }
                    Manager.StartCoroutine(Manager.LeveledSpawner());
                } else {
                    GameManager.GM.Player.GetComponent<HealthScript>().DamageMultiplier = 2f;
                    Manager.Player.GetComponent<InventoryScript>().PrintFancyMessage("THREAT LEVEL " + Manager.CurrentLevel.ToString() + " --- ENEMY HEALTH DOUBLED");
                }
                QueuedThreat = -1;
            }
        }
    }
}
