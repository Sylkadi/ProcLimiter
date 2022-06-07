using R2API;
using RoR2;
using UnityEngine;

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
            Sprite[] sprites = new Sprite[] { Main.bundle.LoadAsset<Sprite>("Assets/Icons/Sticky_Bomb.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/AtG_Missile_Mk._1.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Ukulele.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Sentient_Meat_Hook.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Molten_Perforator.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Charged_Perforator.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Polylute.png"), Main.bundle.LoadAsset<Sprite>("Assets/Icons/Plasma_Shrimp.png") };
            bool[] isHidden = new bool[] { Configuration.ShowStickyBomb.Value, Configuration.ShowAtgMissile.Value, Configuration.ShowUkelele.Value, Configuration.ShowMeathook.Value, Configuration.ShowMoltenPerforator.Value, Configuration.ShowChargedPerforator.Value, Configuration.ShowPolylute.Value, Configuration.ShowPlasmaShrimp.Value };

            SetBuffs(ref buffsNoCooldown, sprites, ref buffsCooldown, isHidden);

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

        private static void SetBuffs(ref BuffDef[] buffsNoCooldown, Sprite[] sprites, ref BuffDef[] buffsCooldown, bool[] isHidden)
        {
            for(int i = 0; i != buffsNoCooldown.Length; i++)
            {
                buffsNoCooldown[i] = ScriptableObject.CreateInstance<BuffDef>();
                buffsNoCooldown[i].name = i.ToString();
                buffsNoCooldown[i].iconSprite = sprites[i];
                buffsNoCooldown[i].canStack = true;
                buffsNoCooldown[i].isCooldown = false;
                buffsNoCooldown[i].isDebuff = false;
                buffsNoCooldown[i].isHidden = isHidden[i];
            }
            for (int i = 0; i != buffsCooldown.Length; i++)
            {
                buffsCooldown[i] = ScriptableObject.CreateInstance<BuffDef>();
                buffsCooldown[i].name = i.ToString();
                buffsCooldown[i].canStack = false;
                buffsCooldown[i].isCooldown = true;
                buffsCooldown[i].isDebuff = false;
                buffsCooldown[i].isHidden = true;
            }
        }

        private static void AddBuffDefs(params BuffDef[] buffs)
        {
            foreach (BuffDef buff in buffs) ContentAddition.AddBuffDef(buff);
        }
    }
}
