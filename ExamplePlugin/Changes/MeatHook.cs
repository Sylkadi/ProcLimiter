using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class MeatHook
    {

        private static bool completed = false;
        private static bool Completed 
        {
            get
            {
                return completed;
            }
            set 
            {
               completed = value;
               if (!value) Log.LogError("Meat Hook hook failed.");
            } 
        }

        public static void Change()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);
                int itemCountLocation = 0;

                Completed = c.TryGotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "BounceNearby"));
                if(Completed)
                {
                    Completed = c.TryGotoNext(v => v.MatchStloc(out itemCountLocation));
                    if(Completed)
                    {
                        Completed = c.TryGotoNext(
                            v => v.MatchLdcR4(1),
                            v => v.MatchLdcR4(100),
                            v => v.MatchLdcR4(100),
                            v => v.MatchLdcR4(20),
                            v => v.MatchLdloc(itemCountLocation)
                            );
                        if(Completed)
                        {
                            c.RemoveRange(13);
                            Completed = c.TryGotoNext(
                                v => v.MatchLdarg(1),
                                v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                                v => v.MatchMul(),
                                v => v.MatchLdloc(4)
                                );
                            if(Completed)
                            {
                                c.Index--;
                                c.RemoveRange(6);
                                c.Emit(OpCodes.Ldloc, itemCountLocation);
                                c.Emit(OpCodes.Ldarg_1);
                                c.Emit(OpCodes.Ldloc, 4);
                                c.EmitDelegate<Func<int, DamageInfo, CharacterMaster, bool>>((itemCount, damageInfo, master) => {
                                    float chance = (1f - 100f / (100f + 20f * itemCount)) * 100f;
                                    bool roll = Util.CheckRoll(chance * damageInfo.procCoefficient, master);
                                    if (Configuration.ApplyMeathook.Value && Configuration.ApplyAllChanges.Value && itemCount > 0 && roll)
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
                            }
                        }
                    }
                }
            };
        }
    }
}
