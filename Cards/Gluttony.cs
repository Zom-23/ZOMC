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
//Blocking gives 3 seconds of time where the person heals 110% of damage done to them

namespace ZomC_Cards.Cards
{

    class Gluttony : CustomCard
    {

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, destroyMonos);
            ConsumeMono consume = player.gameObject.GetOrAddComponent<ConsumeMono>();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<ConsumeMono>();
            UnityEngine.GameObject.Destroy(mono);
        }

        IEnumerator destroyMonos(IGameModeHandler gm)
        {
            DestroyAll<ConsumeMono>();
            yield break;
        }

        void DestroyAll<type>() where type : UnityEngine.Object
        {
            var objects = GameObject.FindObjectsOfType<type>();

            foreach (var mono in objects)
            {
                UnityEngine.Debug.Log($"Attempting to Destroy a {mono.GetType().Name}");
                UnityEngine.Object.Destroy(mono);
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdAdd = 1f;
            statModifiers.health = 1.25f;
            statModifiers.sizeMultiplier = 1.5f;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Consume bullets after blocking to heal";
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
                    stat = "Consume Time",
                    amount = "+3s",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "health",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Size",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Block cooldown",
                    amount = "+1s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Ability cooldown",
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
            return "Sin: Gluttony";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}