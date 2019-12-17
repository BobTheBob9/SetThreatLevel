using System;
using System.Collections.Generic;
using System.Text;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Runtime.Patching;
using Reactor.API.Logging;
using UnityEngine;
using CommandTerminal;

using Logger = Reactor.API.Logging.Log;

namespace SetThreatLevel {
    [ModEntryPoint(ModID)]
    public class Mod : MonoBehaviour {
        public const string ModID = "BobTheBob9/SetThreatLevel";

        public static IManager ModManager;
        public static Logger Logger => LogManager.GetForCurrentAssembly();

        public void Initialize(IManager manager) {
            ModManager = manager;
            RuntimePatcher.RunTranspilers();
            RuntimePatcher.AutoPatch();
            Centrifuge.GTTOD.Internal.Terminal.InitFinished += delegate {
                Terminal.Shell.AddCommand("threatlevel", (args) => {
                    if (args[0].Int < 0 || args[0].Int > 7) {
                        Debug.Log("Invalid threat level! Should be at least 0 and at most 7");
                        return;
                    }
                        
                    ThreatLevel.Set(args[0].Int);
                    Debug.Log("Set current threat level to " + args[0].Int);
                }, 1, 1, "Sets the current threat level");
            };

            Logger.Info("Initialised SetThreatLevel successfully!");
        }

        private void Awake() {
            DontDestroyOnLoad(new GameObject("ThreatLevel").AddComponent<ThreatLevel>());
        }
    }
}
