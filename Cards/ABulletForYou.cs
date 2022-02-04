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
            float healthToAdd = 0;
            int ammoToAdd = 0;
            Balancing(ref healthToAdd, ref ammoToAdd, player);
            data.health *= healthToAdd;
            gunAmmo.maxAmmo += ammoToAdd;
            
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
        }

        public void Balancing(ref float healthToAdd, ref int ammoToAdd, Player owner)
        {
            //1-3 opponents 10% health & +1 ammo for each                                   max: 30% health, +3 ammo total: 30% health, +3 ammo
            //^4-5 opponents 8% health & +1 ammo for each                                   max: 16% health, +2 ammo total: 46% health, +5 ammo
            //^6-7 opponents 6% health & +1 ammo for each                                   max: 12% health, +2 ammo total: 58% health, +7 ammo
            //^8-10 opponents 5% health for each & +1 ammo at the 8th and 10th              max: 15% health, +2 ammo total: 73% health, +9 ammo
            //^11-15 opponents 3% health for each & +1 ammo at the 11th, 13th, and 15th     max: 15% health, +3 ammo total: 88% health, +12 ammo

            int playernum = 0;
            Player[] players = PlayerManager.instance.players.ToArray();

            foreach (Player p in players)
            {
                if (p.teamID != owner.teamID)
                    playernum++;
            }

            if (playernum <= 3)
            {
                healthToAdd = 1 + (playernum / 10);
                ammoToAdd = playernum;
            }
            else if (playernum <= 5)
            {
                //stats from previous tier
                healthToAdd = 1.3f;
                ammoToAdd = 3;
                //stats from opponents in current tier
                healthToAdd += ((playernum - 3) * 8) / 100;
                ammoToAdd += (playernum - 3);
            }
            else if (playernum <= 7)
            {
                //stats from previous tiers
                healthToAdd = 1.46f;
                ammoToAdd = 5;
                //stats from opponents in current tier
                healthToAdd += ((playernum - 3) * 6) / 100;
                ammoToAdd += (playernum - 5);
            }
            else if (playernum <= 10)
            {
                //stats from previous tier
                healthToAdd = 1.58f;
                ammoToAdd = 7;
                //stats from opponents in current tier
                healthToAdd += ((playernum - 7) * 5) / 100;
                if (playernum == 10)
                    ammoToAdd += 2;
                else
                    ammoToAdd += 1;
            }
            else if (playernum <= 15)
            {
                //stats from previous tier
                healthToAdd = 1.73f;
                ammoToAdd = 9;
                //stats from opponents in current tier
                healthToAdd += ((playernum - 10) * 3) / 100;
                if (playernum == 15)
                    ammoToAdd += 3;
                else if (playernum == 13)
                    ammoToAdd += 2;
                else
                    ammoToAdd += 1;
            }
        }

        

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {

            return "For each opponent get: ";
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
                    stat = "Health",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Decreasing returns",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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
