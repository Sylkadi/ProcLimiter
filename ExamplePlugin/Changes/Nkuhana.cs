using System;
using RoR2;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using BepInEx.Configuration;

namespace ProcLimiter.Changes
{
    internal class Nkuhana
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
               if (!value) Log.LogError("Nkuhana hook failed.");
            } 
        }

        public static void Change()
        {
            IL.RoR2.HealthComponent.ServerFixedUpdate += (il) =>
            {
                ILCursor c = new ILCursor(il);

                Completed = c.TryGotoNext(
                    v => v.MatchLdfld<HealthComponent>("devilOrbTimer"),
                    v => v.MatchLdcR4(0.1f),
                    v => v.MatchAdd()
                    );
                if(Completed)
                {
                    c.Index++;
                    c.Remove();
                    c.EmitDelegate<Func<float>>(() => {
                        if (Configuration.ApplyNkuhana.Value && Configuration.ApplyAllChanges.Value) return Configuration.NkuhanaCooldown.Value;
                        return 0.1f;
                    });
                }
            };
        }
    }
}
