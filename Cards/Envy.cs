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


namespace ZomC_Cards.Cards
{
    class Envy : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Player[] players = PlayerManager.instance.players.ToArray();
            List<CardInfo> rareCards = new List<CardInfo>();
            CardInfo[] enemyCards;
            foreach(Player p in players.Where(p => p.playerID != player.playerID))
            {
                enemyCards = p.data.currentCards.ToArray();

                foreach(CardInfo c in enemyCards.Where(c => c.rarity.Equals(CardInfo.Rarity.Rare) && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                {
                    rareCards.Add(c);
                }

                if(rareCards.Count >= 1)
                {
                        var randomNum = UnityEngine.Random.Range(0, rareCards.Count);
                        var randomCard = rareCards[randomNum];
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
                }
                else
                {
                    foreach (CardInfo c in enemyCards.Where(c => c.rarity.Equals(CardInfo.Rarity.Uncommon) && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                    {
                        rareCards.Add(c);
                    }

                    if (rareCards.Count >= 1)
                    {
                            var randomNum = UnityEngine.Random.Range(0, rareCards.Count);
                            var randomCard = rareCards[randomNum];
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
                    }
                    else
                    {
                        foreach (CardInfo c in enemyCards.Where(c => c.rarity.Equals(CardInfo.Rarity.Common) && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                        {
                            rareCards.Add(c);
                        }

                        if (rareCards.Count >= 1)
                        {
                                var randomNum = UnityEngine.Random.Range(0, rareCards.Count);
                                var randomCard = rareCards[randomNum];
                                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
                        }
                    }
                }

            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
            cardInfo.categories = new CardCategory[] { CardChoiceSpawnUniqueCardPatch.CustomCategories.CustomCardCategories.instance.CardCategory("CardManipulation") };
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Copy a random valid card from each other player of the highest rarity";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return null;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Sin: Envy";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
