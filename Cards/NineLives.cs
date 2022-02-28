using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using Sonigon;
using ZomC_Cards.Extensions;

namespace ZomC_Cards.Cards
{
    class NineLives : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int OneTimeUseRespawns = 8;
            data.maxHealth *= .75f;
            characterStats.respawns += 8;
            player.RPCA_SetFace(27, new Vector2(0.0f, 0.0f), 56, new Vector2(-.2f, -.2f), 31, new Vector2(.1f, -.7f), 32, new Vector2(0.0f, 0.0f));
            characterStats.GetAdditionalData().useNewRespawnTime = true;
            characterStats.GetAdditionalData().newRespawnTime = 0.2f;

            health.reviveAction += removeRespawn;

            void removeRespawn()
            {
                if (OneTimeUseRespawns == 0)
                    return;
                else
                {
                    characterStats.respawns--;
                    OneTimeUseRespawns--;
                }
            }

            //audioSource.PlayOneShot()
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Become a cat";
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
                    stat = "One Time Use Lives",
                    amount = "+8",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Health",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Respawn Time",
                    amount = "Shorter",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.NatureBrown;
        }

        protected override string GetTitle()
        {
            return "Nine Lives";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
