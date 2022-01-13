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
using ModdingUtils.MonoBehaviours;

namespace ZomC_Cards.Cards
{
    class PerseverancePristine : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            HealthBasedEffect effect = player.gameObject.AddComponent<HealthBasedEffect>();
            effect.characterStatModifiers.health *= 4f;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "+400% health when under 10% health";
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
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override string GetTitle()
        {
            return "Perseverance Pristine";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
