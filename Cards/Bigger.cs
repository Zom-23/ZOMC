using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace ZomC_Cards.Cards
{
    class Bigger : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int shootCount = 1;
            foreach (ProjectilesToSpawn projectile in gun.projectiles)
                projectile.objectToSpawn.transform.localScale = new Vector3(1.3f, 1.3f);
            characterStats.OnReloadDoneAction += new Action<int>(increaseSize);
            GameModeManager.AddHook("PointEnd", new Func<IGameModeHandler, IEnumerator>(MyHook));

            void increaseSize(int i)
            {
                ++shootCount;
                foreach (ProjectilesToSpawn projectile in gun.projectiles)
                    projectile.objectToSpawn.transform.localScale = new Vector3((float)shootCount * 1.1f, (float)shootCount * 1.1f);
            }

            IEnumerator MyHook(IGameModeHandler gm)
            {
                shootCount = 1;
                yield break;
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Reload with a larger caliber each time";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Mass",
                    amount = "Increasing",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        protected override string GetTitle()
        {
            return "Bigger than Big";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
