using BepInEx.Configuration;

namespace ProcLimiter
{
    internal class Configuration
    {

        public static ConfigEntry<bool> ApplyStickyBomb, ApplyAtgMissile, ApplyUkelele, ApplyMeathook, ApplyMoltenPerforator, ApplyChargedPerforator, ApplyPolylute, ApplyPlasmaShrimp, ApplyNkuhana;
        public static ConfigEntry<float> StickyBombCooldown, AtgMissileCooldown, UkeleleCooldown, MeathookCooldown, MoltenPerforatorCooldown, ChargedPerforatorCooldown, PolyluteCooldown, PlasmaShrimpCooldown, NkuhanaCooldown;

        public static void Initalize()
        {
            ApplyStickyBomb = Main.Config.Bind("Sticky Bomb", "Enable Changes?", true, "Give cooldown?");
            StickyBombCooldown = Main.Config.Bind("Sticky Bomb", "Cooldown time", 0.2f, "How long the cooldown is in seconds.");
            ApplyAtgMissile = Main.Config.Bind("Atg Missle", "Enable Changes?", true, "Give cooldown?");
            AtgMissileCooldown = Main.Config.Bind("Atg Missle", "Cooldown time", 0.25f, "How long the cooldown is in seconds.");
            ApplyUkelele = Main.Config.Bind("Ukelele", "Enable Changes?", true, "Give cooldown?");
            UkeleleCooldown = Main.Config.Bind("Ukelele", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            ApplyMeathook = Main.Config.Bind("Sentient Meat Hook", "Enable Changes?", true, "Give cooldown?");
            MeathookCooldown = Main.Config.Bind("Sentient Meat Hook", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            ApplyMoltenPerforator = Main.Config.Bind("Molten Perforator", "Enable Changes?", true, "Give cooldown?");
            MoltenPerforatorCooldown = Main.Config.Bind("Molten Perforator", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            ApplyChargedPerforator = Main.Config.Bind("Charged Perforator", "Enable Changes?", true, "Give cooldown?");
            ChargedPerforatorCooldown = Main.Config.Bind("Charged Perforator", "Cooldown time", 0.2f, "How long the cooldown is in seconds.");
            ApplyPolylute = Main.Config.Bind("Polylute", "Enable Changes?", true, "Give cooldown?");
            PolyluteCooldown = Main.Config.Bind("Polylute", "Cooldown time", 0.3f, "How long the cooldown is in seconds.");
            ApplyPlasmaShrimp = Main.Config.Bind("Plasma Shrimp", "Enable Changes?", true, "Give cooldown?");
            PlasmaShrimpCooldown = Main.Config.Bind("Plasma Shrimp", "Cooldown time", 0.1f, "How long the cooldown is in seconds.");
            ApplyNkuhana = Main.Config.Bind("Nkuhanas Opinion", "Enable Changes?", true, "Give cooldown?");
            NkuhanaCooldown = Main.Config.Bind("Nkuhanas Opinion", "Cooldown time", 0.15f, "How long the cooldown is in seconds.");
        }

    }
}
