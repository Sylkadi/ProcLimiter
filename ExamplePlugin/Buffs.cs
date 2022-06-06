using R2API;
using RoR2;
using UnityEngine;
using System.Reflection;

namespace ProcLimiter
{
    internal class Buffs
    {

        public static BuffDef
            StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp,
            StickyBombCD, AtgMissileCD, UkeleleCD, MeatHookCD, MoltenPerforatorCD, ChargedPerforatorCD, PolyLuteCD, PlasmaShrimpCD;

        public static void Initalize()
        {

            BuffDef[]
                buffsNoCooldown = new BuffDef[] { StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp },
                buffsCooldown = new BuffDef[] { StickyBombCD, AtgMissileCD, UkeleleCD, MeatHookCD, MoltenPerforatorCD, ChargedPerforatorCD, PolyLuteCD, PlasmaShrimpCD };

            SetBuffs(ref buffsNoCooldown, ref buffsCooldown);

            StickyBomb = buffsNoCooldown[0]; StickyBombCD = buffsCooldown[0];
            AtgMissile = buffsNoCooldown[1]; AtgMissileCD = buffsCooldown[1];
            Ukelele = buffsNoCooldown[2]; UkeleleCD = buffsCooldown[2];
            MeatHook = buffsNoCooldown[3]; MeatHookCD = buffsCooldown[3];
            MoltenPerforator = buffsNoCooldown[4]; MoltenPerforatorCD = buffsCooldown[4];
            ChargedPerforator = buffsNoCooldown[5]; ChargedPerforatorCD = buffsCooldown[5];
            PolyLute = buffsNoCooldown[6]; PolyLuteCD = buffsCooldown[6];
            PlasmaShrimp = buffsNoCooldown[7]; PlasmaShrimpCD = buffsCooldown[7];

            AddBuffDefs(StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp, StickyBombCD, AtgMissileCD, UkeleleCD, MeatHookCD, MoltenPerforatorCD, ChargedPerforatorCD, PolyLuteCD, PlasmaShrimpCD);
        }

        private static void SetBuffs(ref BuffDef[] buffsNoCooldown, ref BuffDef[] buffsCooldown)
        {
            for(int i = 0; i != buffsNoCooldown.Length; i++)
            {
                buffsNoCooldown[i] = new BuffDef
                {
                    name = i.ToString(),
                    canStack = true,
                    isCooldown = false,
                    isDebuff = false,
                    isHidden = false
                };
            }
            for (int i = 0; i != buffsCooldown.Length; i++)
            {
                buffsCooldown[i] = new BuffDef
                {
                    name = i.ToString(),
                    canStack = false,
                    isCooldown = true,
                    isDebuff = false,
                    isHidden = true
                };
            }
        }

        private static void AddBuffDefs(params BuffDef[] buffs)
        {
            foreach (BuffDef buff in buffs) ContentAddition.AddBuffDef(buff);
        }
    }
}
