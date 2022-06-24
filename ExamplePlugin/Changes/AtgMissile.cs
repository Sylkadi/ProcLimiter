using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class AtgMissile
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
               if (!value) Log.LogError("Atg missile hook failed.");
            } 
        }

        public static void Change()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);

                int itemCountLocation = 0;

                Completed = c.TryGotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "Missile"));
                if(Completed)
                {
                    Completed = c.TryGotoNext(v => v.MatchStloc(out itemCountLocation));
                    if(Completed)
                    {
                        Completed = c.TryGotoNext(
                            v => v.MatchLdcR4(10),
                            v => v.MatchLdarg(1),
                            v => v.MatchLdfld<DamageInfo>("procCoefficient"),
                            v => v.MatchMul(),
                            v => v.MatchLdloc(4)
                            );
                        if(Completed)
                        {
                            c.RemoveRange(6);
                            c.Emit(OpCodes.Ldarg_1);
                            c.Emit(OpCodes.Ldloc, 4);
                            c.Emit(OpCodes.Ldloc, itemCountLocation);
                            c.EmitDelegate<Func<DamageInfo, CharacterMaster, int, bool>>((damageInfo, master, itemCount) => {
                                bool roll = Util.CheckRoll(10f * damageInfo.procCoefficient, master);
                                if (Configuration.ApplyAtgMissile.Value && Configuration.ApplyAllChanges.Value && itemCount > 0 && roll)
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
                        }
                    }
                }
            };
        }
    }
}
