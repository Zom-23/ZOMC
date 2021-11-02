using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using System.Linq;
using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnboundLib.Networking;
using SoundImplementation;
using HarmonyLib;
using System.Reflection;
using ModdingUtils.AIMinion.Extensions;
using ModdingUtils.AIMinion;
using UnboundLib.GameModes;
using ModdingUtils;
using ModdingUtils.Extensions;
using ZomC_Cards.MonoBehaviours;

//Put on hold indefinitely

namespace ZomC_Cards.Cards
{
    public class Turret : MinionCardBase
    {
        public class TurretSpawner : MonoBehaviour
        {
            public Color GetBandanaColor(Player player)
            {
                return new Color(0f, 0f, 0.3f, 1f);
            }
            public AIMinionHandler.AIAggression GetAIAggression(Player player)
            {
                return AIMinionHandler.AIAggression.Suicidal;
            }
            public List<CardInfo> GetCards(Player player)
            {
                CardInfo defender = ModdingUtils.Utils.Cards.all.Where(card => card.cardName.ToLower() == "defender").First();
                CardInfo radarShot = ModdingUtils.Utils.Cards.all.Where(card => card.cardName.ToLower() == "radar shot").First();

                return new List<CardInfo>() { radarShot, radarShot, defender };
            }
            public AIMinionHandler.SpawnLocation GetAISpawnLocation(Player player)
            {
                return AIMinionHandler.SpawnLocation.Owner_Back;
            }
            public CharacterStatModifiersModifier GetCharacterStats(Player player)
            {
                return new CharacterStatModifiersModifier()
                {
                    movementSpeed_mult = 0f,
                    jump_mult = 0f
                };
            }
            public GunStatModifier GetGunStats(Player player)
            {
                return new GunStatModifier()
                {
                    damage_mult = 1.5f
                };
            }
            public GunAmmoStatModifier GetGunAmmoStats(Player player)
            {
                return new GunAmmoStatModifier()
                {
                    reloadTimeMultiplier_mult = 0f,
                    currentAmmo_mult = 0
                };
            }
            public BlockModifier GetBlockStats(Player player)
            {
                return new BlockModifier()
                {
                    cdMultiplier_mult = 0.5f
                };
            }
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
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        protected override string GetTitle()
        {
            return "Turret";
        }

        protected override string GetDescription()
        {
            return "Spawn a Turret upon blocking (1 max)";
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }
    }
}