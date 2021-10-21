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
                GameModeManager.AddHook(GameModeHooks.HookRoundStart, MyHook);
                this.happen = true;
            }
        }

        static IEnumerator MyHook(IGameModeHandler gm)
        {
            foreach (Player p in players)
            {
                if (GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points > GameModeManager.CurrentHandler.GetTeamScore(p.teamID).points)
                {
                    winning = true;
                }
                else
                {
                    winning = false;
                }
            }

            if(winning)
            {
                gun.damage *= 1.5f;
                gun.reloadTimeAdd = -1f;
            }

            yield break;
        }
    }


}
