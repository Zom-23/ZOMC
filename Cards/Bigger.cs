using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;

namespace ZomC_Cards.Cards
{
    class Bigger : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            foreach(var p in gun.projectiles)
            {
                p.objectToSpawn.transform.localScale = new Vector3(1.3f, 1.3f);
            }
            gun.ShootPojectileAction += increaseSize();

            System.Action<UnityEngine.GameObject> increaseSize()
            {
                foreach (var p in gun.projectiles)
                {
                    p.objectToSpawn.transform.localScale = new Vector3(p.objectToSpawn.transform.localScale.x + .1f, p.objectToSpawn.transform.localScale.y + .1f);
                }
                return null;
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Increasingly large bullets";
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
                    stat = "Mass",
                    amount = "Increasing",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        protected override string GetTitle()
        {
            return "Bigger than Big";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
