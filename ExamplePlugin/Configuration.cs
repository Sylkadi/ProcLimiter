using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using System.Reflection;
using UnityEngine;

namespace ProcLimiter
{
    internal class Configuration
    {

        public static ConfigEntry<bool> ApplyStickyBomb, ApplyAtgMissile, ApplyUkelele, ApplyMeathook, ApplyMoltenPerforator, ApplyChargedPerforator, ApplyPolylute, ApplyPlasmaShrimp, ApplyNkuhana, ApplyAllChanges;
        public static ConfigEntry<float> StickyBombCooldown, AtgMissileCooldown, UkeleleCooldown, MeathookCooldown, MoltenPerforatorCooldown, ChargedPerforatorCooldown, PolyluteCooldown, PlasmaShrimpCooldown, NkuhanaCooldown;
        public static ConfigEntry<int> StickyBombStack, AtgMissileStack, UkeleleStack, MeathookStack, MoltenPerforatorStack, ChargedPerforatorStack, PolyluteStack, PlasmaShrimpStack;

        public static void Initalize()
        {

            // Bind to config
            ApplyAllChanges = Main.Config.Bind("General", "Apply all changes?", true, "Apply all cooldown changes to items?");

            ApplyStickyBomb = Main.Config.Bind("Sticky Bomb", "Enable Changes?", true, "Give cooldown?");
            StickyBombCooldown = Main.Config.Bind("Sticky Bomb", "Cooldown time", 0.2f, "How long the cooldown is in seconds.");
            StickyBombStack = Main.Config.Bind("Sticky Bomb", "Stack count", 20, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyAtgMissile = Main.Config.Bind("Atg Missile", "Enable Changes?", true, "Give cooldown?");
            AtgMissileCooldown = Main.Config.Bind("Atg Missile", "Cooldown time", 0.25f, "How long the cooldown is in seconds.");
            AtgMissileStack = Main.Config.Bind("Atg Missile", "Stack count", 10, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyUkelele = Main.Config.Bind("Ukelele", "Enable Changes?", true, "Give cooldown?");
            UkeleleCooldown = Main.Config.Bind("Ukelele", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            UkeleleStack = Main.Config.Bind("Ukelele", "Stack count", 5, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyMeathook = Main.Config.Bind("Sentient Meat Hook", "Enable Changes?", true, "Give cooldown?");
            MeathookCooldown = Main.Config.Bind("Sentient Meat Hook", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            MeathookStack = Main.Config.Bind("Sentient Meat Hook", "Stack count", 5, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyMoltenPerforator = Main.Config.Bind("Molten Perforator", "Enable Changes?", true, "Give cooldown?");
            MoltenPerforatorCooldown = Main.Config.Bind("Molten Perforator", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            MoltenPerforatorStack = Main.Config.Bind("Molten Perforator", "Stack count", 10, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyChargedPerforator = Main.Config.Bind("Charged Perforator", "Enable Changes?", true, "Give cooldown?");
            ChargedPerforatorCooldown = Main.Config.Bind("Charged Perforator", "Cooldown time", 0.2f, "How long the cooldown is in seconds.");
            ChargedPerforatorStack = Main.Config.Bind("Charged Perforator", "Stack count", 10, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyPolylute = Main.Config.Bind("Polylute", "Enable Changes?", true, "Give cooldown?");
            PolyluteCooldown = Main.Config.Bind("Polylute", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            PolyluteStack = Main.Config.Bind("Polylute", "Stack count", 5, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyPlasmaShrimp = Main.Config.Bind("Plasma Shrimp", "Enable Changes?", true, "Give cooldown?");
            PlasmaShrimpCooldown = Main.Config.Bind("Plasma Shrimp", "Cooldown time", 0.1f, "How long the cooldown is in seconds.");
            PlasmaShrimpStack = Main.Config.Bind("Plasma Shrimp", "Stack count", 20, "How many times can this item proc before it stops procing. 1 stack is removed every cooldown interval.");

            ApplyNkuhana = Main.Config.Bind("Nkuhanas Opinion", "Enable Changes?", true, "Give cooldown?");
            NkuhanaCooldown = Main.Config.Bind("Nkuhanas Opinion", "Cooldown time", 0.15f, "How long the cooldown is in seconds.");

            // Risk of options
            ModSettingsManager.SetModIcon(Main.bundle.LoadAsset<Sprite>("Assets/Icons/Mod_Icon.png")); // Set icon

            StepSliderConfig stepSlider = new StepSliderConfig { min = 0, max = 2, increment = 0.01f, formatString = "{0}s" };
            IntSliderConfig intSlider = new IntSliderConfig { min = 1, max = 50 };

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyAllChanges));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyStickyBomb));
            ModSettingsManager.AddOption(new StepSliderOption(StickyBombCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(StickyBombStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyAtgMissile));
            ModSettingsManager.AddOption(new StepSliderOption(AtgMissileCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(AtgMissileStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyUkelele));
            ModSettingsManager.AddOption(new StepSliderOption(UkeleleCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(UkeleleStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyMeathook));
            ModSettingsManager.AddOption(new StepSliderOption(MeathookCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(MeathookStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyMoltenPerforator));
            ModSettingsManager.AddOption(new StepSliderOption(MoltenPerforatorCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(MoltenPerforatorStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyChargedPerforator));
            ModSettingsManager.AddOption(new StepSliderOption(ChargedPerforatorCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(ChargedPerforatorStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyPolylute));
            ModSettingsManager.AddOption(new StepSliderOption(PolyluteCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(PolyluteStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyPlasmaShrimp));
            ModSettingsManager.AddOption(new StepSliderOption(PlasmaShrimpCooldown, stepSlider));
            ModSettingsManager.AddOption(new IntSliderOption(PlasmaShrimpStack, intSlider));

            ModSettingsManager.AddOption(new CheckBoxOption(ApplyNkuhana));
            ModSettingsManager.AddOption(new StepSliderOption(NkuhanaCooldown, new StepSliderConfig { min = 0.1f, max = 2, increment = 0.01f, formatString = "{0}s"}));

        }

    }
}
