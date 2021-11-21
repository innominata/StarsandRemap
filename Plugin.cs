using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UltimateSurvival;
using UltimateSurvival.InputSystem;
using UltimateSurvival.StandardAssets;
using UnityEngine;
using UnityEngine.UI;
using Button = UltimateSurvival.InputSystem.Button;

namespace StarsandRemap
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class StarsandRemap : BaseUnityPlugin
    {
        public static AssetBundle ab = AssetBundle.LoadFromFile(Path.GetDirectoryName(Assembly.GetAssembly(typeof(StarsandRemap)).Location) + "\\font");
        public static Font font;
        public static ManualLogSource Logger;
        private void Awake()
        {
            Logger = new ManualLogSource("Remap");
            BepInEx.Logging.Logger.Sources.Add(Logger);
            Logger.LogInfo("Starsand Remap Mod Loading");
            Harmony.CreateAndPatchAll(typeof(StarsandRemap));
            if(ab != null)
            {
                font = ab.LoadAsset<Font>("Assets/Komon.ttf");
                
            }
            else
            {
                Debug.Log("FAILED TO LOAD ASSET BUNDLE");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PieceBuild), "Start")]
        public static void PieceBuildStart(PieceBuild __instance)
        {
            Button jDestroy = GameController.InputManager.FindButton("JDestroy");
            if (jDestroy != null)
            {
                var index = GameController.InputManager.m_InputData.Buttons.IndexOf(jDestroy);
                GameController.InputManager.m_InputData.Buttons[index] = new Button("JDestroy", KeyCode.V);
            }
            else
            {
                jDestroy = new Button("JDestroy", KeyCode.V);
                GameController.InputManager.AddButton(jDestroy);
            }
        }
       

    }
}