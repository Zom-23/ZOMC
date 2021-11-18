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
//makes you fire your entire clip in a single burst with one bullet following the other
//Bursts = number of times it fires per attack actions
//number of projectiles = number of bullets per burst

namespace ZomC_Cards.Cards
{
    public class TrainBullets : CustomCard
    {

        public TrainBullets()
        {
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo += 3;
            gun.timeBetweenBullets = .05f;
            gun.spread = 0;
            gun.evenSpread = 0;
            gun.numberOfProjectiles = 1;
            gun.bursts = gunAmmo.maxAmmo;
            characterStats.OnReloadDoneAction += setBursts; //Make sure the bursts is always equal to the max ammo by checking it after each reload
            
            void setBursts(int i)
            {
                gun.bursts = gunAmmo.maxAmmo;
            }


            //Persistence bug fix
            GameModeManager.AddHook(GameModeHooks.HookGameEnd, MyHook);
            IEnumerator MyHook(IGameModeHandler gm)
            {
                characterStats.OnReloadDoneAction -= setBursts;
                gun.bursts = 1;
                yield break;
            }
        }

        public override void OnRemoveCard()
        { }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            gun.reloadTimeAdd = .75f;
        }

        protected override UnityEngine.GameObject GetCardArt()
        { return null; }

        protected override string GetDescription()
        {
            return "Bullets follow one another";
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
                    stat = "Spread",
                    amount = "No",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "ammo",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Reload",
                    amount = "+.75s",
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
            return "Train Bullets";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
