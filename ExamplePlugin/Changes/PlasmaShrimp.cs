using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class PlasmaShrimp
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
               if (!value) Log.LogError("Plasma shrimp hook failed.");
            } 
        }

        public static void Change()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);
                int itemCountLocation = 0;

                Completed = c.TryGotoNext(v => v.MatchLdsfld("RoR2.DLC1Content/Items", "MissileVoid"));
                if(Completed)
                {
                    Completed = c.TryGotoNext(
                            v => v.MatchLdloc(out itemCountLocation),
                            v => v.MatchLdcI4(0)
                            );
                    if(Completed)
                    {
                        c.Index++;
                        c.Emit(OpCodes.Ldloc, 4);
                        c.EmitDelegate<Func<int, CharacterMaster, int>>((itemCount, master) => {
                            int roll = itemCount;
                            if (Configuration.ApplyPlasmaShrimp.Value && Configuration.ApplyAllChanges.Value && itemCount > 0)
                            {
                                CharacterBody body = master.GetBody();
                                if (body.GetBuffCount(Buffs.PlasmaShrimp) < Configuration.PlasmaShrimpStack.Value)
                                {
                                    if (!body.HasBuff(Buffs.PlasmaShrimpCD)) body.AddTimedBuff(Buffs.PlasmaShrimpCD, Configuration.PlasmaShrimpCooldown.Value);
                                    body.AddBuff(Buffs.PlasmaShrimp);
                                }
                                else { roll = 0; }
                            }
                            return roll;
                        });
                    }
                }
            };
        }
    }
}
