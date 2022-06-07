using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using UnityEngine;

namespace ProcLimiter
{
    internal class Configuration
    {

        public static ConfigEntry<bool> ApplyStickyBomb, ApplyAtgMissile, ApplyUkelele, ApplyMeathook, ApplyMoltenPerforator, ApplyChargedPerforator, ApplyPolylute, ApplyPlasmaShrimp, ApplyNkuhana, ApplyAllChanges;
        public static ConfigEntry<float> StickyBombCooldown, AtgMissileCooldown, UkeleleCooldown, MeathookCooldown, MoltenPerforatorCooldown, ChargedPerforatorCooldown, PolyluteCooldown, PlasmaShrimpCooldown, NkuhanaCooldown;
        public static ConfigEntry<int> StickyBombStack, AtgMissileStack, UkeleleStack, MeathookStack, MoltenPerforatorStack, ChargedPerforatorStack, PolyluteStack, PlasmaShrimpStack;
        public static ConfigEntry<bool> ShowStickyBomb, ShowAtgMissile, ShowUkelele, ShowMeathook, ShowMoltenPerforator, ShowChargedPerforator, ShowPolylute, ShowPlasmaShrimp;

        public static void Initalize()
        {
            // Setup
            StepSliderConfig stepSlider = new StepSliderConfig { min = 0, max = 2, increment = 0.01f, formatString = "{0}s" };
            IntSliderConfig intSlider = new IntSliderConfig { min = 1, max = 50 };

            void BindBasicOptions(ref ConfigEntry<bool> Apply, ref ConfigEntry<float> Cooldown, ref ConfigEntry<int> Stack, ref ConfigEntry<bool> Show, float cooldownAmount, int stackAmount, string Name)
            {
                Apply = Main.Config.Bind(Name, "Enable Changes?", true, "Give cooldown?");
                Cooldown = Main.Config.Bind(Name, "Cooldown time", cooldownAmount, "How long the cooldown is in seconds.");
                Stack = Main.Config.Bind(Name, "Stack count", stackAmount, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");
                Show = Main.Config.Bind(Name, "Buff Is Hidden?", true, "Is the buff hidden?, true = dont show buff icon, false = show buff icon.");

                ModSettingsManager.AddOption(new CheckBoxOption(Apply));
                ModSettingsManager.AddOption(new StepSliderOption(Cooldown, stepSlider));
                ModSettingsManager.AddOption(new IntSliderOption(Stack, intSlider));
                ModSettingsManager.AddOption(new CheckBoxOption(Show));
            }

            // Bind to config
            ApplyAllChanges = Main.Config.Bind("General", "Apply all changes?", true, "Apply all cooldown changes to items?");
            ModSettingsManager.AddOption(new CheckBoxOption(ApplyAllChanges));

            BindBasicOptions(ref ApplyStickyBomb, ref StickyBombCooldown, ref StickyBombStack, ref ShowStickyBomb, 0.2f, 20, "Sticky Bomb");
            BindBasicOptions(ref ApplyAtgMissile, ref AtgMissileCooldown, ref AtgMissileStack, ref ShowAtgMissile, 0.25f, 10, "Atg Missile");
            BindBasicOptions(ref ApplyUkelele, ref UkeleleCooldown, ref UkeleleStack, ref ShowUkelele, 0.3f, 5, "Ukelele");
            BindBasicOptions(ref ApplyMeathook, ref MeathookCooldown, ref MeathookStack, ref ShowMeathook, 0.3f, 5, "Sentient Meat Hook");
            BindBasicOptions(ref ApplyMoltenPerforator, ref MoltenPerforatorCooldown, ref MoltenPerforatorStack, ref ShowMoltenPerforator, 0.3f, 10, "Molten Perforator");
            BindBasicOptions(ref ApplyChargedPerforator, ref ChargedPerforatorCooldown, ref ChargedPerforatorStack, ref ShowChargedPerforator, 0.2f, 10, "Charged Perforator");
            BindBasicOptions(ref ApplyPolylute, ref PolyluteCooldown, ref PolyluteStack, ref ShowPolylute, 0.3f, 5, "Polylute");
            BindBasicOptions(ref ApplyPlasmaShrimp, ref PlasmaShrimpCooldown, ref PlasmaShrimpStack, ref ShowPlasmaShrimp, 0.1f, 20, "Plasma Shrimp");

            ApplyNkuhana = Main.Config.Bind("Nkuhanas Opinion", "Enable Changes?", true, "Give cooldown?");
            NkuhanaCooldown = Main.Config.Bind("Nkuhanas Opinion", "Cooldown time", 0.15f, "How long the cooldown is in seconds.");

            ModSettingsManager.SetModIcon(Main.bundle.LoadAsset<Sprite>("Assets/Icons/Mod_Icon.png")); // Set icon

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyNkuhana));
            ModSettingsManager.AddOption(new StepSliderOption(NkuhanaCooldown, new StepSliderConfig { min = 0.1f, max = 2, increment = 0.01f, formatString = "{0}s"}));

        }
    }
}
