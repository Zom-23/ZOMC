using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;
using UnboundLib.GameModes;
//Increases damage and decreases relod if you have more points than all your opponents

namespace ZomC_Cards.Cards
{
    class StayingAhead : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PointCheckMono winEffect = player.gameObject.GetOrAddComponent<PointCheckMono>();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<DoubleViSpawner>();
            UnityEngine.GameObject.Destroy(mono);
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "More damage and faster reload if you are winning!";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
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
            return "Staying Ahead";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
