using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace ProcLimiter
{
    internal class CilChanges
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

                c.GotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "StickyBomb") );
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
                    if(Configuration.ApplyStickyBomb.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.StickyBomb)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.StickyBomb, Configuration.StickyBombCooldown.Value);

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
                    if(Configuration.ApplyAtgMissile.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.AtgMissile)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.AtgMissile, Configuration.AtgMissileCooldown.Value);
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
                    if(Configuration.ApplyUkelele.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.Ukelele)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.Ukelele, Configuration.UkeleleCooldown.Value);
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
                    if(Configuration.ApplyMeathook.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.MeatHook)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.MeatHook, Configuration.MeathookCooldown.Value);
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
                    if(Configuration.ApplyMoltenPerforator.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.MoltenPerforator)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.MoltenPerforator, Configuration.MoltenPerforatorCooldown.Value);
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
                    if(Configuration.ApplyChargedPerforator.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.ChargedPerforator)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.ChargedPerforator, Configuration.ChargedPerforatorCooldown.Value);
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
                    if(Configuration.ApplyPolylute.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.PolyLute)) roll = false;
                        if (roll) body.AddTimedBuff(Buffs.PolyLute, Configuration.PolyluteCooldown.Value);

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
                    int roll = 0;
                    if (itemCount > 0) roll = 1;
                    if(Configuration.ApplyPlasmaShrimp.Value && Configuration.ApplyAllChanges.Value)
                    {
                        CharacterBody body = master.GetBody();
                        if (body.HasBuff(Buffs.PlasmaShrimp)) roll = 0;
                        if (roll >= 1) body.AddTimedBuff(Buffs.PlasmaShrimp, Configuration.PlasmaShrimpCooldown.Value);
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
                    if(Configuration.ApplyNkuhana.Value && Configuration.ApplyAllChanges.Value) return Configuration.NkuhanaCooldown.Value;
                    return 0.1f;
                });
            };
        }


    }
}
