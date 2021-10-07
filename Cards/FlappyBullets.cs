using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;


namespace ZomC_Cards
{
    class FlappyBullets : CustomCard
    {
        private Camera mainCam;
        private Transform parent;


        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {  
        }

        public override void OnRemoveCard()
        {  
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            Vector3 pos = this.mainCam.WorldToScreenPoint(this.transform.position);
            pos.x /= (float)Screen.width;
            pos.y /= (float)Screen.height;


            gun.gravity = .5f;
            gun.speedMOnBounce = 0f;
            gun.ExecuteAfterSeconds(1, () =>
            {
                this.parent.transform.position = this.mainCam.ScreenToWorldPoint(new Vector3(pos.x, pos.y + 1, pos.z));
            });
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "The bullets shall flap";
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
                    positive = false,
                    stat = "Bounce",
                    amount = "Lose all Speed on",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "50%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.NatureBrown;
        }

        protected override string GetTitle()
        {
            return "Flappy Bullets";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
