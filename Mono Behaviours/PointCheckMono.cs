using UnityEngine;
using UnboundLib;
using Photon.Pun;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnboundLib.Utils;
using UnboundLib.Networking;
using UnboundLib.GameModes;
using UnboundLib.Patches;
using ZomC_Cards.Cards;
using HarmonyLib;
using System.Collections;
using System;
//A mono used to check how many points each person has
//It is used by the card Staying Ahead


namespace ZomC_Cards.MonoBehaviours
{
    class PointCheckMono : MonoBehaviour
    {
        static Player[] players = PlayerManager.instance.players.ToArray();
        static private Player player;
        static private Gun gun;
        bool happen;
        static bool winning;

        void Awake()
        {
            player = gameObject.GetComponent<Player>();
            gun = gameObject.GetComponent<Gun>();
        }

        void Start()
        {
            this.happen = false;
            winning = false;
        }

        void Update()
        {
            if (!happen)
            {
                GameModeManager.AddHook(GameModeHooks.HookPointStart, CheckWinning);
                this.happen = true;
            }
        }
        static IEnumerator CheckWinning(IGameModeHandler gm)
        {
            try
            {
                foreach (Player p in players.Where(p => p.teamID != player.teamID))
                {
                    if (GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points > GameModeManager.CurrentHandler.GetTeamScore(p.teamID).points)
                    {
                        winning = true;
                    }
                    else
                    {
                        winning = false;
                        break;
                    }
                }

                if (winning)
                {
                    gun.damage *= 1.5f;
                    gun.GetComponentInChildren<GunAmmo>().reloadTimeAdd -= 1f;
                }
                yield break;

            }
            catch (Exception e)
            { UnityEngine.Debug.LogException(e); }
        }
    }
}
