using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;
//Causes bullets to "jump" every half second

namespace ZomC_Cards
{
    class FlappyBullets : CustomCard
    {

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {  
        }

        public override void OnRemoveCard()
        {  
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

            gun.gravity = .5f;
            gun.speedMOnBounce = 0f;
            gun.randomBounces = 1;

            ObjectsToSpawn flapBulletObj = new ObjectsToSpawn() { };
            flapBulletObj.AddToProjectile = new GameObject("FlappyMono", typeof(FlappyMono));
            List<ObjectsToSpawn> objectsToSpawn = gun.objectsToSpawn.ToList();
            objectsToSpawn.Add(flapBulletObj);
            gun.objectsToSpawn = objectsToSpawn.ToArray();

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
