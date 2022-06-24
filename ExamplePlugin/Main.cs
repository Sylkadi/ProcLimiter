using BepInEx;
using UnityEngine;
using BepInEx.Configuration;

namespace ProcLimiter
{
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Drinkable";
        public const string PluginName = "ProcLimiter";
        public const string PluginVersion = "1.2.2";

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
            Changes.Buff.AddBuffsOnInit();

            // Cil Changes
            Changes.StickyBomb.Change();
            Changes.AtgMissile.Change();
            Changes.Ukelele.Change();
            Changes.MeatHook.Change();
            Changes.MoltenPerforator.Change();
            Changes.ChargedPerforator.Change();
            Changes.PlasmaShrimp.Change();
            Changes.Polylute.Change();
            Changes.Nkuhana.Change();

            // Other Changes
            Changes.Buff.OnRemoveBuff();
            Changes.Buff.ShowBuffsEvents();

            // Say done
            Log.LogInfo(PluginName + ": Awake done");
        }
    }
}
