using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class StickyBomb
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
               if (!value) Log.LogError("Sticky bomb hook failed.");
            } 
        }

        public static void Change()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);
                int itemCountLocation = 0;

                Completed = c.TryGotoNext(v => v.MatchLdsfld("RoR2.RoR2Content/Items", "StickyBomb"));
                if (Completed)
                {
                    Completed = c.TryGotoNext(
                        v => v.MatchLdcR4(5),
                        v => v.MatchLdloc(out itemCountLocation),
                        v => v.MatchConvR4(),
                        v => v.MatchMul(),
                        v => v.MatchLdarg(1)
                        );
                    if (Completed)
                    {
                        c.RemoveRange(9);
                        c.Emit(OpCodes.Ldloc, itemCountLocation);
                        c.Emit(OpCodes.Ldarg_1);
                        c.Emit(OpCodes.Ldloc, 4);
                        c.EmitDelegate<Func<int, DamageInfo, CharacterMaster, bool>>((itemCount, damageInfo, master) => {
                            bool roll = Util.CheckRoll(5f * itemCount * damageInfo.procCoefficient, master);
                            if (Configuration.ApplyStickyBomb.Value && Configuration.ApplyAllChanges.Value && itemCount > 0 && roll)
                            {
                                CharacterBody body = master.GetBody();
                                if (body.GetBuffCount(Buffs.StickyBomb) < Configuration.StickyBombStack.Value)
                                {
                                    if (!body.HasBuff(Buffs.StickyBombCD)) body.AddTimedBuff(Buffs.StickyBombCD, Configuration.StickyBombCooldown.Value);
                                    body.AddBuff(Buffs.StickyBomb);
                                }
                                else { roll = false; }
                            }
                            return roll;
                        });
                    }
                }
            };
        }
    }
}
