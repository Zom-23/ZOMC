using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using UnboundLib.GameModes;
using System.Collections;
//Gives bullets a chance to silence for 2 seconds upon hit

namespace ZomC_Cards.Cards
{
    class Lust : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            System.Random random = new System.Random();
            int chance = 0;
            int chanceNeed = data.currentCards.Where(card => card.cardName == "Sin: Lust").Count() * 20;

            characterStats.DealtDamageAction += charm;

            void charm(Vector2 damage, bool selfDamage)
            {
                chance = random.Next(1, 100);
                if(chance <= chanceNeed)
                {
                    data.lastDamagedPlayer.data.isSilenced = true;
                    data.lastDamagedPlayer.data.silenceTime = 2f;
                }
            }
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Charm (silence) your opponents by hitting them";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Charm Chance",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Charm Time",
                    amount = "2s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Sin: Lust";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
