﻿using UnityEngine;
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
    class MSilenceMono : MonoBehaviour
    {
        static Player[] players = PlayerManager.instance.players.ToArray();
        static private Player player;
        bool happen;

        void Awake()
        {
            player = gameObject.GetComponent<Player>();
        }

        void Start()
        {
            this.happen = false;
        }

        void Update()
        {
            if(!happen)
            {
                GameModeManager.AddHook(GameModeHooks.HookRoundStart, MyHook);
                this.happen = true;
            }
        }

        static IEnumerator MyHook(IGameModeHandler gm)
        {
            foreach(Player p in players)
            {
                if(p.teamID != player.teamID)
                {
                    p.data.isSilenced = true;
                    p.data.silenceTime = 3f;
                }
            }
            yield break;
        }
    }

    
}