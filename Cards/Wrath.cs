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
using ZomC_Cards.MonoBehaviours;
//Grants more damage and bullets that cause enemies to take 15% over 5 seconds
//Removes 1 ammo

namespace ZomC_Cards.Cards
{
    class Wrath : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
            list.Add(new ObjectsToSpawn
            {
                AddToProjectile = new GameObject("FireMono", new Type[]
                    {
                        typeof(FireMono)
                    })
            });

            gun.objectsToSpawn = list.ToArray();
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage = 1.5f;
            gun.ammo = -2;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<FireMono>();
            UnityEngine.GameObject.Destroy(mono);
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Attack your enemies with the anger of Hell";
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
                    stat = "Damage",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "ammo",
                    amount = "-2",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Fire Bullets",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Sin: Wrath";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
