using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using BepInEx.Configuration;

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
        public const string PluginVersion = "1.0";

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
            if (Configuration.ApplyStickyBomb.Value) CilChanges.StickyBomb();
            if (Configuration.ApplyAtgMissile.Value) CilChanges.AtgMissile();
            if (Configuration.ApplyUkelele.Value) CilChanges.Ukelele();
            if (Configuration.ApplyMeathook.Value) CilChanges.MeatHook();
            if (Configuration.ApplyMoltenPerforator.Value) CilChanges.MoltenPerforator();
            if (Configuration.ApplyChargedPerforator.Value) CilChanges.ChargedPerforator();
            if (Configuration.ApplyPolylute.Value) CilChanges.PolyLute();
            if (Configuration.ApplyPlasmaShrimp.Value) CilChanges.PlasmaShrimp();
            if (Configuration.ApplyNkuhana.Value) CilChanges.Nkuhana();

            // Say done
            Log.LogInfo(PluginName + ": Awake done");
        }
    }
}
