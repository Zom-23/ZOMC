using System;
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
    class AdaptGun : Gun
    { };

    class AdaptiveMags : CustomCard
    {

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int reloadCount = 0;
            gunAmmo.maxAmmo += 3;

            GameModeManager.AddHook(GameModeHooks.HookPointStart, MyHook);
            IEnumerator MyHook(IGameModeHandler gm)
            {
                gunAmmo.maxAmmo -= reloadCount;
                reloadCount = 0;
                yield break;
            }

            characterStats.OnReloadDoneAction += IncreaseAmmo;
            void IncreaseAmmo(int i)
            {
                reloadCount++;
                gunAmmo.maxAmmo++;
                gun.ammo++;
                gunAmmo.ReDrawTotalBullets();
            }
        }



        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            gun.reloadTimeAdd = (gun.reloadTime * -.25f);
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
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "ammo",
                    amount = "+3",
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

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
