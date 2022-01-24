using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ZomC_Cards.MonoBehaviours;

//Silences others for 3 seconds upon point start

namespace ZomC_Cards.Cards
{
    public class MassSilence : CustomCard
    {
        public MassSilence()
        {
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            MSilenceMono silenceEffect = player.gameObject.GetOrAddComponent<MSilenceMono>();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetComponent<DoubleViSpawner>();
            UnityEngine.GameObject.Destroy(mono);
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }

        protected override UnityEngine.GameObject GetCardArt()
        { return null; }

        protected override string GetDescription()
        {
            return "Silence everyone else for 3 seconds on point start!";
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

                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        protected override string GetTitle()
        {
            return "Mass Silence";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
