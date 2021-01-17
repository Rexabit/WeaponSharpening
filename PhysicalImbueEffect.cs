using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSharpening
{
    public class PhysicalImbueEffect : MonoBehaviour
    {
        public static ImbueEffectPreset imbueEffect;

        /// <summary>
        /// Creates a Sideloader Physical Imbue Effect in code.
        /// </summary>
        public static void Setup() {
            SL_Effect[] effects = new SL_Effect[]
            {
                new SL_WeaponDamage()
                {
                    Damage = new List<SL_Damage>() {
                        new SL_Damage() { Damage = 5, Type = DamageType.Types.Physical }
                    },
                    OverrideType = DamageType.Types.Physical,
                },
                new SL_AddStatusEffect()
                {
                    StatusEffect = "Bleeding",
                    ChanceToContract = 15
                }
            };

            SL_ImbueEffect effect = new SL_ImbueEffect
            {
                TargetStatusID = 252, // Shatter Bullet Imbue
                NewStatusID = 260,
                SLPackName = "WeaponSharpening",
                SubfolderName = "PhysicalImbue",
                Name = "Sharpened Weapon",
                Description = "Adds physical damage to attacks.",
                EffectBehaviour = EditBehaviours.Destroy,
                Effects = new SL_EffectTransform[] {
                    new SL_EffectTransform()
                    {
                        TransformName = "HitEffects",
                        Effects = effects
                    }
                }
            };

            effect.OnTemplateApplied += Effect_OnTemplateApplied;
            effect.Apply();
        }

        /// <summary>
        /// Modifies the visual effects of the Physical Imbue and sets a proper status icon
        /// </summary>
        public static void Initialize()
        {
            ImbueEffectPreset physicalImbue = ResourcesPrefabManager.Instance.GetEffectPreset(260) as ImbueEffectPreset;

            // Get the smoke effect and disable it
            physicalImbue.ImbueFX.Find("Smoke").GetComponent<ParticleSystemRenderer>().enabled = false;
        }

        private static void Effect_OnTemplateApplied(ImbueEffectPreset iep)
        {
            imbueEffect = iep;
        }
    }
}
