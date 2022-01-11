using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;

namespace ZomC_Cards.Cards
{
    class LighterAmmo : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.gravity = .7f;
            gun.damage = .9f;
            statModifiers.movementSpeed = 1.15f;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Did your bullets get lighter?";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullet Gravity",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        protected override string GetTitle()
        {
            return "Lighter Ammo";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
