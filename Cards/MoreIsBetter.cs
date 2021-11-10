using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;
using ZomC_Cards.Cards;
using System.Collections;
//Increase all stats even bad ones


namespace ZomC_Cards.Cards
{
    class MoreIsBetter : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.attackSpeed *= 1.5f;
            gun.bodyRecoil *= 1.5f;
            gun.bulletDamageMultiplier *= 1.5f;
            gun.damage *= 1.5f;
            gun.evenSpread *= 1.5f;
            gun.gravity *= 1.5f;
            gun.knockback *= 1.5f;
            gun.projectileSize *= 1.5f;
            gun.projectileSpeed *= 1.5f;
            gun.recoil *= 1.5f;
            gun.reloadTime *= 1.5f;
            gun.size *= 1.5f;
            gun.spread *= 1.5f;
            gunAmmo.maxAmmo = (int)(gunAmmo.maxAmmo * .5);
            block.cdAdd *= 1.5f;
            characterStats.health *= 1.5f;
            characterStats.gravity *= 1.5f;
            characterStats.lifeSteal *= 1.5f;
            characterStats.movementSpeed *= 1.5f;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Wait more isn't always better!?";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Everything",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        protected override string GetTitle()
        {
            return "More is better?";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
