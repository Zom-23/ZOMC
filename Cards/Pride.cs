using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.Utils;
using UnboundLib.GameModes;
using System.Collections;
using ZomC_Cards.Extensions;
//Grants 70% damage resistance when above an opponent
//Player takes 30% more damage when below an opponent

namespace ZomC_Cards.Cards
{
    class Pride : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            InConeEffect belowEffect = player.gameObject.AddComponent<InConeEffect>();
            belowEffect.SetCenterRay(new Vector2(0f, 1f));
            belowEffect.SetAngle(90f);
            belowEffect.SetEffectFunc(belowDamageEffect);

            InConeEffect aboveEffect = player.gameObject.AddComponent<InConeEffect>();
            aboveEffect.SetCenterRay(new Vector2(0f, -1f));
            aboveEffect.SetAngle(90f);
            aboveEffect.SetEffectFunc(aboveDamageEffect);


        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Those below me are not worthy to damage me";
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
                    stat = "When Above:",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage Resistance",
                    amount = "70%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "When Below:",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Extra Damage Taken",
                    amount = "30%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Sin: Pride";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }

        //Below Other players
        public List<MonoBehaviour> belowDamageEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<MonoBehaviour> effects = new List<MonoBehaviour>();
            ReversibleColorEffect colorEffect = player.gameObject.AddComponent<ReversibleColorEffect>();
            ReversibleEffect effect = player.gameObject.AddComponent<ReversibleEffect>();

            effect.characterStatModifiers.GetAdditionalData().DamageReduction = -.3f;
            colorEffect.SetColor(Color.gray);
            
            effects.Add(effect);
            effects.Add(colorEffect);

            return effects;

        }
        //Above other Players
        public List<MonoBehaviour> aboveDamageEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<MonoBehaviour> effects = new List<MonoBehaviour>();

            ReversibleEffect effect = player.gameObject.AddComponent<ReversibleEffect>();
            ReversibleColorEffect colorEffect = player.gameObject.AddComponent<ReversibleColorEffect>();

            effect.characterStatModifiers.GetAdditionalData().DamageReduction = .7f;
            colorEffect.SetColor(Color.red);

            effects.Add(effect);
            effects.Add(colorEffect);

            return effects;

        }
    }
}
