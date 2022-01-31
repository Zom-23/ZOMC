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
        System.Random random = new System.Random();

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            DoubleViSpawner doubleVision = player.gameObject.GetOrAddComponent<DoubleViSpawner>();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<DoubleViSpawner>();
            UnityEngine.GameObject.Destroy(mono);
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

            List<ObjectsToSpawn> objectsToSpawns = new List<ObjectsToSpawn>();
            ObjectsToSpawn doubleVi = new ObjectsToSpawn { };
            doubleVi.AddToProjectile = new GameObject("DoubleViSpawner", typeof(DoubleViSpawner));
            objectsToSpawns.Add(doubleVi);
            gun.objectsToSpawn = objectsToSpawns.ToArray();

            gun.multiplySpread = .1f;
            gun.spread = .5f;
            //gun.evenSpread = .5f;
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
                    amount = "+50%",
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
