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
        public const string PluginVersion = "1.1.0";

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
            Changes.Cil.GetMasterLocation();
            Changes.Cil.StickyBomb();
            Changes.Cil.AtgMissile();
            Changes.Cil.Ukelele();
            Changes.Cil.MeatHook();
            Changes.Cil.MoltenPerforator();
            Changes.Cil.ChargedPerforator();
            Changes.Cil.PolyLute();
            Changes.Cil.PlasmaShrimp();
            Changes.Cil.Nkuhana();

            // Other Changes
            Changes.OnRemoveBuff();

            // Say done
            Log.LogInfo(PluginName + ": Awake done");

        }
    }
}
