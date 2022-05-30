using R2API;
using RoR2;
using UnityEngine;

namespace ProcLimiter
{
    internal class Buffs
    {

        public static BuffDef StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp;

        public static void Initalize()
        {
            BuffDef[] buffs = new BuffDef[] { StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp };
            SetBuffs(ref buffs);

            StickyBomb = buffs[0];
            AtgMissile = buffs[1];
            Ukelele = buffs[2];
            MeatHook = buffs[3];
            MoltenPerforator = buffs[4];
            ChargedPerforator = buffs[5];
            PolyLute = buffs[6];
            PlasmaShrimp = buffs[7];

            AddBuffDefs(StickyBomb, AtgMissile, Ukelele, MeatHook, MoltenPerforator, ChargedPerforator, PolyLute, PlasmaShrimp);
        }

        private static void SetBuffs(ref BuffDef[] buffs)
        {
            for(int i = 0; i != buffs.Length; i++)
            {
                buffs[i] = new BuffDef
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
