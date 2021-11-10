﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;
using ZomC_Cards.Cards;
using System.Collections;

//Increases the maximum ammo after emptying your magazine, resets between rounds

namespace ZomC_Cards.Cards
{
    class AdaptiveMags : CustomCard
    {

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
            gunAmmo.maxAmmo = 3;
            gun.reloadTime *= .5f;
            GameModeManager.AddHook(GameModeHooks.HookPointStart, MyHook);
            IEnumerator MyHook(IGameModeHandler gm)
            {
                gunAmmo.maxAmmo = 3;
                yield break;
            }
            /*
            characterStats.OutOfAmmpAction += IncreaseAmmo;
            void IncreaseAmmo(int i)
            {
                gunAmmo.maxAmmo++;
            }
            */
        }

        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            GunAmmo gunAmmo = new GunAmmo();
            statModifiers.OutOfAmmpAction += IncreaseAmmo;
            void IncreaseAmmo(int i)
            {
                gunAmmo.maxAmmo++;
            }
            
        }
        

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Your magazine grows";
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
                    stat = "Reload",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "ammo",
                    amount = "3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override string GetTitle()
        {
            return "Adaptive Magazines";
        }

        
    }
}
