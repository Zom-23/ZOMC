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
//A Mono to silence all players not on the team of the person activating this
//Used by the card Mass Silence

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
            if (!happen)
            {
                GameModeManager.AddHook(GameModeHooks.HookBattleStart, MyHook);
                this.happen = true;
            }
        }

        void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }

        public void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, MyHook);
        }

        void Update()
        {
            if (gameObject.transform.parent == null)
                return;
            if (player.data.currentCards.Where(card => card.cardName == "Mass Silence").Count() == 0)
            {
                Destroy();
            }
        }

        static IEnumerator MyHook(IGameModeHandler gm)
        {
            foreach (Player p in players)
            {
                if (p.teamID != player.teamID)
                {
                    p.gameObject.GetComponent<SilenceHandler>().RPCA_AddSilence(3f);
                    p.data.input.silencedInput = true;
                }
            }
            yield break;
        }
    }


}
