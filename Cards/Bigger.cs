using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace ZomC_Cards.Cards
{
    class Bigger : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int shootCount = 1;
            foreach(var p in gun.projectiles)
            {
                p.objectToSpawn.transform.localScale = new Vector3(1.1f, 1.1f);
            }
            characterStats.OnReloadDoneAction += increaseSize; //Make sure the bursts is always equal to the max ammo by checking it after each reload

            void increaseSize(int i)
            {
                shootCount++;
                foreach (var p in gun.projectiles)
                {
                    p.objectToSpawn.transform.localScale = new Vector3(shootCount * 1.1f, shootCount * 1.1f);
                }
            }

            GameModeManager.AddHook(GameModeHooks.HookPointEnd, MyHook);
            IEnumerator MyHook(IGameModeHandler gm)
            {
                shootCount = 1;
                yield break;
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
            return "Reload with a larger caliber each time";
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
