using ZomC_Cards.Extensions;
using ZomC_Cards.MonoBehaviours;
using ZomC_Cards.patches;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnboundLib;
using ModdingUtils.Utils;
using UnboundLib.Utils;

namespace ZomC_Cards.Extensions
{
    class GunAmmoPatch
    {
        [HarmonyPatch(typeof(GunAmmo), "Shoot")]
        class Patch_Shoot
        {
            private static void Postfix(GunAmmo __instance, GameObject projectile, Gun ___gun)
            {
                // DoRecoil
                if (___gun.holdable && ___gun.holdable.holder.view.IsMine && ___gun.holdable.holder.stats.GetAdditionalData().recoil != 0)
                {
                    var holdable = ___gun.holdable;
                    var healthHandler = holdable.holder.healthHandler;
                    var player = holdable.holder.player;
                    var direction = ___gun.player.data.aimDirection;
                    var recoil = holdable.holder.stats.GetAdditionalData().recoil;
                    var damage = ___gun.damage;

                    healthHandler.CallTakeForce(-new Vector2(1000 * direction.x, 1000 * direction.y) * (recoil * 2.5f * damage));
                }
            }
        }
    }
}
