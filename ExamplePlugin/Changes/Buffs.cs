using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class Buff
    {
        public static void OnRemoveBuff()
        {
            On.RoR2.CharacterBody.UpdateBuffs += (orig, body, deltaTime) =>
            {
                for (int i = body.timedBuffs.Count - 1; i >= 0; i--)
                {
                    CharacterBody.TimedBuff timedBuff = body.timedBuffs[i];
                    if (timedBuff.timer - deltaTime <= 0f)
                    {

                        void updateBuff(BuffDef buff, BuffDef buffCD, float cooldown)
                        {
                            if (timedBuff.buffIndex == buffCD.buffIndex && body.GetBuffCount(buff) > 0)
                            {
                                body.RemoveBuff(buff);
                                body.AddTimedBuff(buffCD, cooldown);
                            }
                        }

                        updateBuff(Buffs.StickyBomb, Buffs.StickyBombCD, Configuration.StickyBombCooldown.Value);
                        updateBuff(Buffs.AtgMissile, Buffs.AtgMissileCD, Configuration.AtgMissileCooldown.Value);
                        updateBuff(Buffs.Ukelele, Buffs.UkeleleCD, Configuration.UkeleleCooldown.Value);
                        updateBuff(Buffs.MeatHook, Buffs.MeatHookCD, Configuration.MeathookCooldown.Value);
                        updateBuff(Buffs.MoltenPerforator, Buffs.MoltenPerforatorCD, Configuration.MoltenPerforatorCooldown.Value);
                        updateBuff(Buffs.ChargedPerforator, Buffs.ChargedPerforatorCD, Configuration.ChargedPerforatorCooldown.Value);
                        updateBuff(Buffs.PolyLute, Buffs.PolyLuteCD, Configuration.PolyluteCooldown.Value);
                        updateBuff(Buffs.PlasmaShrimp, Buffs.PlasmaShrimpCD, Configuration.PlasmaShrimpCooldown.Value);
                    }

                }

                orig.Invoke(body, deltaTime);
            };
        }

        private static void UpdateBuffVisibility(BuffDef buff, bool value)
        {
            BuffDef[] newBuffDefs = BuffCatalog.buffDefs;
            newBuffDefs[(int)buff.buffIndex].isHidden = value;
            BuffCatalog.SetBuffDefs(newBuffDefs);
        }

        public static void ShowBuffsEvents()
        {
            Configuration.ShowStickyBomb.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.StickyBomb, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowAtgMissile.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.AtgMissile, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowUkelele.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.Ukelele, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowMeathook.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.MeatHook, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowMoltenPerforator.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.MoltenPerforator, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowChargedPerforator.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.ChargedPerforator, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowPolylute.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.PolyLute, (obj as ConfigEntry<bool>).Value);
            Configuration.ShowPlasmaShrimp.SettingChanged += (obj, e) => UpdateBuffVisibility(Buffs.PlasmaShrimp, (obj as ConfigEntry<bool>).Value);
        }

        public static void AddBuffsOnInit()
        {
            On.RoR2.BuffCatalog.Init += (orig) =>
            {
                orig.Invoke();
                Buffs.Initalize();
            };
        }
    }
}
