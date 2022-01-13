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
//Gives an extra bullet and +10% health for each opponent in the game

namespace ZomC_Cards.Cards
{
    class ABulletForYou : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int playernum = 0;
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach (Player p in players)
            {
                if (p.teamID != gun.player.teamID)
                    playernum++;
            }


            gunAmmo.maxAmmo += playernum;
            data.maxHealth *= 1 + (playernum / 10);
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            //1-3 opponents 10% health & +1 ammo for each                                   max: 30% health, +3 ammo total: 30% health, +3 ammo
            //^4-5 opponents 8% health & +1 ammo for each                                   max: 16% health, +2 ammo total: 46% health, +5 ammo
            //^6-7 opponents 6% health & +1 ammo for each                                   max: 12% health, +2 ammo total: 58% health, +7 ammo
            //^8-10 opponents 5% health for each & +1 ammo at the 8th and 10th              max: 15% health, +2 ammo total: 73% health, +9 ammo
            //^11-15 opponents 3% health for each & +1 ammo at the 11th, 13th, and 15th     max: 15% health, +3 ammo total: 88% health, +12 ammo
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "For each opponent you get: ";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "health",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        protected override string GetTitle()
        {
            return "A Bullet For You";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
