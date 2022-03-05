using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Utils;
using UnboundLib.GameModes;
using System.Collections;
//Blocking next to an opponent forces the opponent to reload and temporarily adds the bullets they had remaining to owner's gun

namespace ZomC_Cards
{
    class Greed : CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            bool ready = true;
            float range = 5f;
            int ammoToSteal = 0;
            Vector2 pos = block.transform.position;
            Player[] players = PlayerManager.instance.players.ToArray();
            ReversibleEffect extraAmmo = player.gameObject.AddComponent<ReversibleEffect>();

            block.BlockAction += StealBullets(player, block);

            Action<BlockTrigger.BlockTriggerType> StealBullets(Player _player, Block _block)
            {
                if (ready)
                {
                    return delegate (BlockTrigger.BlockTriggerType trigger)
                    {
                        ready = false;

                        for (int i = 0; i < players.Length; i++)
                        {
                            if (players[i].playerID == player.playerID) { continue; }

                            // apply to players within range, that are within line-of-sight
                            if (Vector2.Distance(pos, players[i].transform.position) < range && PlayerManager.instance.CanSeePlayer(player.transform.position, players[i]).canSee)
                            {
                                ammoToSteal = players[i].data.weaponHandler.gun.ammo;
                                players[i].data.weaponHandler.gun.isReloading = true;
                                extraAmmo.gunAmmoStatModifier.currentAmmo_add = ammoToSteal;
                            }
                        }

                        characterStats.ExecuteAfterSeconds(1f, () =>
                        {
                            ready = true;
                        });
                    };
                }
                else
                    return null;
            }
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.ammo = -1;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Block near an enemy to steal their bullets";
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
                positive = false,
                stat = "Ability Cooldown",
                amount = "1s",
                simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "-1",
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
            return "Sin: Greed";
        }

        public override string GetModName()
        {
            return "ZOMC";
        }
    }
}
