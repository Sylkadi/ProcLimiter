using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using BepInEx.Configuration;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions;

namespace ProcLimiter
{

    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    public class Main : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Drinkable";
        public const string PluginName = "ProcLimiter";
        public const string PluginVersion = "1.0.1";

        public static ConfigFile Config;

        public void Awake()
        {
            // Initalize
            Log.Initalize(Logger);
            Buffs.Initalize();

            // Config
            Config = new ConfigFile(Paths.ConfigPath + "\\" + PluginName + ".cfg", true);
            Configuration.Initalize();

            // Cil Changes
            CilChanges.GetMasterLocation();
            CilChanges.StickyBomb();
            CilChanges.AtgMissile();
            CilChanges.Ukelele();
            CilChanges.MeatHook();
            CilChanges.MoltenPerforator();
            CilChanges.ChargedPerforator();
            CilChanges.PolyLute();
            CilChanges.PlasmaShrimp();
            CilChanges.Nkuhana();

            // Say done
            Log.LogInfo(PluginName + ": Awake done");

        }
    }
}
