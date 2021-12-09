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
//Blocking gives 3 seconds of time where the person heals 110% of damage done to them

namespace ZomC_Cards.Cards
{
    class Gluttony : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            block.BlockAction += OnBlock(player, block);

            Action<BlockTrigger.BlockTriggerType> OnBlock(Player _player, Block _block)
            {
                return delegate (BlockTrigger.BlockTriggerType trigger)
                {
                    characterStats.WasDealtDamageAction += OnWasDealtDamage;

                    characterStats.ExecuteAfterSeconds(3f, () =>
                    {
                        characterStats.WasDealtDamageAction -= OnWasDealtDamage;
                    });
                };
            }

            void OnWasDealtDamage(Vector2 damage, bool selfDamage)
            {
                data.healthHandler.Heal(damage.magnitude + damage.magnitude * .1f);
            }

        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdAdd = 1f;
            statModifiers.health = 1.25f;
            statModifiers.sizeMultiplier = 1.5f;
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Consume bullets after blocking";
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
                    stat = "Consume Time",
                    amount = "+3s",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "health",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Size",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Block cd",
                    amount = "+1s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Sin: Gluttony";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}