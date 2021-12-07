using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib.GameModes;
using System.Collections;

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
                    positive = false,
                    stat = "Damage Resistance",
                    amount = "70%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "When Below:",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
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

        public List<MonoBehaviour> belowDamageEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<MonoBehaviour> effects = new List<MonoBehaviour>();

            ReversibleEffect effect = player.gameObject.AddComponent<ReversibleEffect>();

            characterStats.WasDealtDamageAction += OnWasDealtDamageBelow;

            void OnWasDealtDamageBelow(Vector2 damage, bool selfDamage)
            {
                Vector2 extraDamage = damage;
                extraDamage.SetPropertyValue("magnitude", damage.magnitude * .3);
                health.TakeDamage(extraDamage, damage);
            }

            effects.Add(effect);

            return effects;

        }
        public List<MonoBehaviour> aboveDamageEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<MonoBehaviour> effects = new List<MonoBehaviour>();

            ReversibleEffect effect = player.gameObject.AddComponent<ReversibleEffect>();

            characterStats.WasDealtDamageAction += OnWasDealtDamageAbove;

            void OnWasDealtDamageAbove(Vector2 damage, bool selfDamage)
            {
                data.healthHandler.Heal(damage.magnitude * .7f);
            }

            effects.Add(effect);

            return effects;

        }
    }
}
