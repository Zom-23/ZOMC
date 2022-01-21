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
//Increases everyone's gravity and supposed to slow
//Reduces movement speed of owner, increases lifesteal, takes damage over 3 seconds

namespace ZomC_Cards.Cards
{
    class Sloth : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Player[] players = PlayerManager.instance.players.ToArray();
            foreach(Player p in players)
            {
                p.data.stats.gravity *= 1.3f;
                p.data.weaponHandler.gun.gravity *= 1.2f;    
            }
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, SlowAll);

            IEnumerator SlowAll(IGameModeHandler gm)
            {
                foreach (Player p in players)
                {
                    if (p.teamID != player.teamID)
                    {
                        p.data.stats.RPCA_AddSlow(2);
                    }
                }
                yield break;
            }
        }

        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.movementSpeed = .8f;
            statModifiers.lifeSteal = 1.5f;
            statModifiers.secondsToTakeDamageOver = 3f;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Everyone should just slow down";
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
                    positive = false,
                    stat = "Speed",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Life Steal",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Take Damage Over Time",
                    amount = "3s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Everyone",
                    amount = "Heavier",
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
            return "Sin: Sloth";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
