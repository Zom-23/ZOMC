using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;

namespace ZomC_Cards.Cards
{
    class DoubleVision : CustomCard
    {
        DoubleVisionMono doubleVision = null;
        System.Random random = new System.Random();

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.AddComponent<DoubleVisionMono>();
        }

        public override void OnRemoveCard()
        {
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            gun.reloadTimeAdd = .25f;
            gun.spread = 1.5f;
            gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(gun.ShootPojectileAction, doubleVision.shootAction);
        }

     

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "You got drunk";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectiles",
                    amount = "Double",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Spread",
                    amount = "150%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload",
                    amount = "+.25s",
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
            return "Double Vision";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
