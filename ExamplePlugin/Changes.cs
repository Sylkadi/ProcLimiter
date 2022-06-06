using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;
using System.Linq;

namespace ProcLimiter
{
    internal class Changes
    {

        public class Cil
        {

            private static int masterLocation = 0;

            public static void GetMasterLocation()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);
                    c.GotoNext(v => v.MatchCallOrCallvirt<CharacterBody>("get_master"), v => v.MatchStloc(out masterLocation));
                };
            }

            public static void StickyBomb()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);
                    int num = 0;

                    c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "StickyBomb"));
                    c.GotoNext(
                        v => v.MatchLdcR4(5),
                        v => v.MatchLdloc(out num),
                        v => v.MatchConvR4(),
                        v => v.MatchMul(),
                        v => v.MatchLdarg(1)
                        );

                    c.RemoveRange(9);
                    c.Emit(OpCodes.Ldloc, num);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<int, DamageInfo, CharacterMaster, bool>>((itemCount, damageInfo, master) => {
                        bool roll = Util.CheckRoll(5f * itemCount * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyStickyBomb.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if(body.GetBuffCount(Buffs.StickyBomb) < Configuration.StickyBombStack.Value)
                            {
                                if (!body.HasBuff(Buffs.StickyBombCD)) body.AddTimedBuff(Buffs.StickyBombCD, Configuration.StickyBombCooldown.Value);
                                body.AddBuff(Buffs.StickyBomb);
                            } else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void AtgMissile()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "Missile"));
                    c.GotoNext(
                        v => v.MatchLdcR4(10),
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );

                    c.RemoveRange(6);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<DamageInfo, CharacterMaster, bool>>((damageInfo, master) => {
                        bool roll = Util.CheckRoll(10f * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyAtgMissile.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.AtgMissile) < Configuration.AtgMissileStack.Value)
                            {
                                if (!body.HasBuff(Buffs.AtgMissileCD)) body.AddTimedBuff(Buffs.AtgMissileCD, Configuration.AtgMissileCooldown.Value);
                                body.AddBuff(Buffs.AtgMissile);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void Ukelele()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "ChainLightning"));
                    c.GotoNext(
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );
                    c.Index--;

                    c.RemoveRange(6);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<DamageInfo, CharacterMaster, bool>>((damageInfo, master) => {
                        bool roll = Util.CheckRoll(25f * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyUkelele.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.Ukelele) < Configuration.UkeleleStack.Value)
                            {
                                if (!body.HasBuff(Buffs.UkeleleCD)) body.AddTimedBuff(Buffs.UkeleleCD, Configuration.UkeleleCooldown.Value);
                                body.AddBuff(Buffs.Ukelele);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void MeatHook()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);
                    int num = 0;

                    c.GotoNext(
                        v => v.MatchLdsfld("RoR2.RoR2Content/Items", "BounceNearby"),
                        v => v.MatchCallOrCallvirt<Inventory>("GetItemCount"),
                        v => v.MatchStloc(out num)
                        );
                    c.GotoNext(
                        v => v.MatchLdcR4(1),
                        v => v.MatchLdcR4(100),
                        v => v.MatchLdcR4(100),
                        v => v.MatchLdcR4(20),
                        v => v.MatchLdloc(num)
                        );
                    c.RemoveRange(13);
                    c.GotoNext(
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );
                    c.Index--;
                    c.RemoveRange(6);

                    c.Emit(OpCodes.Ldloc, num);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<int, DamageInfo, CharacterMaster, bool>>((itemCount, damageInfo, master) => {
                        float chance = (1f - 100f / (100f + 20f * itemCount)) * 100f;
                        bool roll = Util.CheckRoll(chance * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyMeathook.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.MeatHook) < Configuration.MeathookStack.Value)
                            {
                                if (!body.HasBuff(Buffs.MeatHookCD)) body.AddTimedBuff(Buffs.MeatHookCD, Configuration.MeathookCooldown.Value);
                                body.AddBuff(Buffs.MeatHook);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void MoltenPerforator()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "FireballsOnHit"));
                    c.GotoNext(
                        v => v.MatchLdcR4(10),
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );

                    c.RemoveRange(6);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<DamageInfo, CharacterMaster, bool>>((damageInfo, master) => {
                        bool roll = Util.CheckRoll(10f * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyMoltenPerforator.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.MoltenPerforator) < Configuration.MoltenPerforatorStack.Value)
                            {
                                if (!body.HasBuff(Buffs.MoltenPerforatorCD)) body.AddTimedBuff(Buffs.MoltenPerforatorCD, Configuration.MoltenPerforatorCooldown.Value);
                                body.AddBuff(Buffs.MoltenPerforator);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void ChargedPerforator()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "LightningStrikeOnHit"));
                    c.GotoNext(
                        v => v.MatchLdcR4(10),
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );

                    c.RemoveRange(6);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<DamageInfo, CharacterMaster, bool>>((damageInfo, master) => {
                        bool roll = Util.CheckRoll(10f * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyChargedPerforator.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.ChargedPerforator) < Configuration.ChargedPerforatorStack.Value)
                            {
                                if (!body.HasBuff(Buffs.ChargedPerforatorCD)) body.AddTimedBuff(Buffs.ChargedPerforatorCD, Configuration.ChargedPerforatorCooldown.Value);
                                body.AddBuff(Buffs.ChargedPerforator);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void PolyLute()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(v => v.MatchLdsfld("RoR2.DLC1Content/Items", "ChainLightningVoid"));
                    c.GotoNext(
                        v => v.MatchLdarg(1),
                        v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                        v => v.MatchMul(),
                        v => v.MatchLdloc(masterLocation)
                        );
                    c.Index--;

                    c.RemoveRange(6);
                    c.Emit(OpCodes.Ldarg_1);
                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<DamageInfo, CharacterMaster, bool>>((damageInfo, master) => {
                        bool roll = Util.CheckRoll(25f * damageInfo.procCoefficient, master);
                        if (Configuration.ApplyPolylute.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if (body.GetBuffCount(Buffs.PolyLute) < Configuration.PolyluteStack.Value)
                            {
                                if (!body.HasBuff(Buffs.PolyLuteCD)) body.AddTimedBuff(Buffs.PolyLuteCD, Configuration.PolyluteCooldown.Value);
                                body.AddBuff(Buffs.PolyLute);
                            }
                            else { roll = false; }
                        }
                        return roll;
                    });

                };
            }

            public static void PlasmaShrimp()
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new ILCursor(il);
                    int num = 0;

                    c.GotoNext(v => v.MatchLdsfld("RoR2.DLC1Content/Items", "MissileVoid"));
                    c.GotoNext(
                        v => v.MatchLdloc(out num),
                        v => v.MatchLdcI4(0)
                        );
                    c.Index++;

                    c.Emit(OpCodes.Ldloc, masterLocation);
                    c.EmitDelegate<Func<int, CharacterMaster, int>>((itemCount, master) => {
                        int roll = itemCount;
                        if (Configuration.ApplyPlasmaShrimp.Value && Configuration.ApplyAllChanges.Value)
                        {
                            CharacterBody body = master.GetBody();
                            if(body.GetBuffCount(Buffs.PlasmaShrimp) < Configuration.PlasmaShrimpStack.Value)
                            {
                                if (!body.HasBuff(Buffs.PlasmaShrimpCD)) body.AddTimedBuff(Buffs.PlasmaShrimpCD, Configuration.PlasmaShrimpCooldown.Value);
                                body.AddBuff(Buffs.PlasmaShrimp);
                            } else { roll = 0; }
                        }
                        return roll;
                    });

                };
            }

            public static void Nkuhana()
            {
                IL.RoR2.HealthComponent.ServerFixedUpdate += (il) =>
                {
                    ILCursor c = new ILCursor(il);

                    c.GotoNext(
                        v => v.MatchLdfld<HealthComponent>("devilOrbTimer"),
                        v => v.MatchLdcR4(0.1f),
                        v => v.MatchAdd()
                        );
                    c.Index++;
                    c.Remove();
                    c.EmitDelegate<Func<float>>(() => {
                        if (Configuration.ApplyNkuhana.Value && Configuration.ApplyAllChanges.Value) return Configuration.NkuhanaCooldown.Value;
                        return 0.1f;
                    });
                };
            }

        }


        public static void OnRemoveBuff()
        {
            On.RoR2.CharacterBody.UpdateBuffs += (orig, body, deltaTime) =>
            {
                for (int i = body.timedBuffs.Count - 1; i >= 0; i--)
                {
                    CharacterBody.TimedBuff timedBuff = body.timedBuffs[i];
                    if(timedBuff.timer - deltaTime <= 0f)
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

        private static void UpdateBuff(ref BuffDef buff, bool isHidden)
        {
            foreach (BuffDef Buff in BuffCatalog.buffDefs)
            {
                if (Buff.buffIndex == buff.buffIndex)
                {
                    Buff.isHidden = isHidden;
                }
            }
            buff.isHidden = isHidden;

        }


    }
}
