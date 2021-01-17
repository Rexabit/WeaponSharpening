using System;
using UnityEngine;

namespace WeaponSharpening
{
    public class SharpeningStoneEffect : Effect
    {
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            Character c = _affectedCharacter;

            if (!c.IsLocalPlayer) { return; }

            if (c.CurrentWeapon == null)
            {
                SendUIMessage(c, "You need a weapon equipped to do that!");

                return;
            }

            bool broken = true;
            Item lowestDurability = null;
            float currentDurability = float.MaxValue;

            foreach (Item item in c.Inventory.GetOwnedItems(WeaponSharpening.SHARPENING_STONE_ID))
            {
                if (item.CurrentDurability > 0 && item.CurrentDurability < currentDurability)
                {
                    broken = false;
                    lowestDurability = item;
                    currentDurability = item.CurrentDurability;
                }
            }

            if (broken)
            {
                SendUIMessage(c, string.Format("Your Sharpening Stone is worn!"));
                return;
            }

            try
            {
                ImbueEffectPreset p = ResourcesPrefabManager.Instance.GetEffectPreset(260) as ImbueEffectPreset;
                c.CurrentWeapon.AddImbueEffect(p, 120f);

                if (p == null)
                {
                    SendUIMessage(c, "Failed to load preset");
                }

                lowestDurability.ReduceDurability(WeaponSharpening.DURABILITY_COST);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                SendUIMessage(c, "Failed to sharpen!");
            }
        }
        private void SendUIMessage(Character c, string s)
        {
            c.CharacterUI.ShowInfoNotification(s);
        }
    }
}
