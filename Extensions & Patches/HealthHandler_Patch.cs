using HarmonyLib;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.Cards;
using ZomC_Cards.MonoBehaviours;
using ZomC_Cards.Extensions;

namespace ZomC_Cards.patches
{
    [HarmonyPatch(typeof(HealthHandler))]
    class HealthHandler_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("DoDamage")]
        static void ApplyDamageReduction(HealthHandler __instance, ref Vector2 damage, Player ___player)
        {
            if (___player.data.stats.GetAdditionalData().DamageReduction != 0f)
            {
                var originalDamage = damage.magnitude;
                if (___player.data.stats.GetAdditionalData().DamageReduction < 1f)
                {
                    damage *= (1f - ___player.data.stats.GetAdditionalData().DamageReduction);

                    if (damage.magnitude < ___player.data.maxHealth * 0.05f && originalDamage > ___player.data.maxHealth * 0.05f)
                    {
                        damage = damage.normalized * ___player.data.maxHealth * 0.05f;
                    }
                }
                else
                {
                    if (damage.magnitude > ___player.data.maxHealth * 0.05f)
                    {
                        damage = damage.normalized * ___player.data.maxHealth * 0.05f;
                    }
                }
            }
        }
    }
}
