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
//Copies a card from each other player choosing from the highest valid rarity
//Card effect is subject to change

namespace ZomC_Cards.Cards
{
    class Envy : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Player[] players = PlayerManager.instance.players.ToArray();
            List<CardInfo> rareCards = new List<CardInfo>(); //viable cards
            List<CardInfo> cardsToAdd = new List<CardInfo>();
            List<CardInfo> enemyCards;
            foreach (Player p in players.Where(p => p.playerID != player.playerID && p.teamID != player.teamID))
            {
                enemyCards = p.data.currentCards.ToList();

                foreach (CardInfo c in enemyCards.Where(c => /*c.rarity.Equals(CardInfo.Rarity.Rare) &&*/ ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                {
                    rareCards.Add(c);
                }

                if (rareCards.Count >= 1)
                {
                    var randomNum = UnityEngine.Random.Range(0, rareCards.Count);
                    cardsToAdd.Add(rareCards[randomNum]);
                    //var randomCard = rareCards[randomNum];
                    //ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                    //ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
                }/*
                else
                {
                    foreach (CardInfo c in enemyCards.Where(c => c.rarity.Equals(CardInfo.Rarity.Uncommon) && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)))
                    {
                        rareCards.Add(c);
                    }

                    if (rareCards.Count >= 1)
                    {
                        var randomNum = UnityEngine.Random.Range(0, rareCards.Count);
                        cardsToAdd.Add(rareCards[randomNum]);
                        //var randomCard = rareCards[randomNum];
                        //ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                        //ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
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
                            cardsToAdd.Add(rareCards[randomNum]);
                            //var randomCard = rareCards[randomNum];
                            //ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randomCard, false, "", 0, 0, true);
                            //ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, randomCard);
                        }
                    }
                }*/
                enemyCards.Clear();
                rareCards.Clear();
            }

            CardInfo[] _cardsToAdd = cardsToAdd.ToArray();
            Unbound.Instance.ExecuteAfterFrames(5, () =>
            {
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, _cardsToAdd, false, null, null, null, true);
            });
            cardsToAdd.Clear();
            
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
            cardInfo.categories = new CardCategory[] { CardChoiceSpawnUniqueCardPatch.CustomCategories.CustomCardCategories.instance.CardCategory("CardManipulation") };
            statModifiers.health = .75f;
            statModifiers.movementSpeed = .75f;
            gun.damage = .8f;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Copy a random valid card from each other opponent";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-20%",
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
            return "Sin: Envy";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
