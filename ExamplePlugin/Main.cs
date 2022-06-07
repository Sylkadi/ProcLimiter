using BepInEx;
using R2API;
using R2API.Utils;
using UnityEngine;
using BepInEx.Configuration;

namespace ProcLimiter
{

    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI), nameof(ContentAddition))]
    public class Main : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Drinkable";
        public const string PluginName = "ProcLimiter";
        public const string PluginVersion = "1.2.0";

        public static ConfigFile Config;
        public static AssetBundle bundle;

        public void Awake()
        {
            // Initalize
            Log.Initalize(Logger);

            // Load Bundle
            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ProcLimiter.icons")) bundle = AssetBundle.LoadFromStream(stream);

            // Config
            Config = new ConfigFile(Paths.ConfigPath + "\\" + PluginName + ".cfg", true);
            Configuration.Initalize();

            // Buffs
            Buffs.Initalize();

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
            Changes.ShowBuffsEvents();

            // Say done
            Log.LogInfo(PluginName + ": Awake done");

        }
    }
}
