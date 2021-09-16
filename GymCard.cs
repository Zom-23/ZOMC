using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;

namespace Zom.Cards
{
    public class GymCard : CustomCard
    {
        public GymCard()
        {
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.damage *= 1.5f;
            characterStats.sizeMultiplier *= 1.5f;
            data.maxHealth += 50;
            characterStats.movementSpeed *= .7f;
            gun.reloadTime += .5f;
        }

        public override void OnRemoveCard()
        {
            throw new NotImplementedException();
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            throw new NotImplementedException();
        }

        protected override UnityEngine.GameObject GetCardArt()
        {
            throw new NotImplementedException();
        }

        protected override string GetDescription()
        {
            return "Do you even lift bro?";
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
                    stat = "Damage",
                    amount = "1.5x",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Size",
                    amount = "1.5x",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Health",
                    amount = "+50",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Speed",
                    amount = ".7",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "+ .5s",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        protected override string GetTitle()
        {
            return "Gym";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
