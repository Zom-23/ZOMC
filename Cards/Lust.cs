using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using Photon.Pun;
using Photon;
using UnboundLib;
using UnboundLib.GameModes;
using System.Collections;
using ZomC_Cards.MonoBehaviours;
//Gives bullets a chance to silence for 2 seconds upon hit

namespace ZomC_Cards.Cards
{
    class Lust : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
            list.Add(new ObjectsToSpawn
            {
                AddToProjectile = new GameObject("LustMono", new Type[]
                    {
                        typeof(LustMono)
                    })
            });

            gun.objectsToSpawn = list.ToArray();


            /*System.Random random = new System.Random();
            int chanceNeed = data.currentCards.Where(card => card.cardName == "Sin: Lust").Count() * 50;

            characterStats.DealtDamageAction += charm;
            void charm(Vector2 damage, bool selfDamage)
            {
                int chance = 0;
                chance = random.Next(1, 100);
                if (chance <= chanceNeed)
                {
                    RPCA_Charm(chance, player.data.lastDamagedPlayer);
                }
            }*/
        }
        /*
        private void RPCA_Charm(int roll, Player p)
        {
            p.data.gameObject.GetComponent<SilenceHandler>().RPCA_AddSilence(2f);
        }
        */

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<LustMono>();
            UnityEngine.GameObject.Destroy(mono);
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
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
                    amount = "+50%",
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
