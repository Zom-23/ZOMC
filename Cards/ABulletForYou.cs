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


namespace ZomC_Cards.Cards
{
    class ABulletForYou : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            int playernum = 0;
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach(Player p in players)
            {
                if (p.teamID != gun.player.teamID)
                    playernum++;
            }


            gun.ammo = playernum;
            statModifiers.health = 1 + (playernum / 10);
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
