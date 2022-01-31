using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace ZomC_Cards.Cards
{
    class SpareMag : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int reloads = 0;
            characterStats.OutOfAmmpAction += spareCheck;

            gun.reloadTime *= 1.3f;

            void spareCheck(int i)
            {
                if(reloads % 2 == 0)
                {
                    gunAmmo.ReDrawTotalBullets();
                    reloads++;
                }
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "No reload time every other magazine";
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
                    positive = false,
                    stat = "Reload Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        protected override string GetTitle()
        {
            return "Spare Magazine";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
