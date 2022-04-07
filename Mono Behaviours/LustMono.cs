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
    class LustMono : RayHitEffect
    {
        private System.Random rand;
        private int chance;

        static private Player player;
        static private HitInfo hit;


        private void Awake()
        {
            player = gameObject.GetComponent<Player>();
            hit = gameObject.GetComponent<HitInfo>();
        }

        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (!hit.transform)
            {
                return HasToReturn.canContinue;
            }

            //ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
            SilenceHandler component = hit.transform.GetComponent<SilenceHandler>();
            this.rand = new System.Random();
            this.chance = rand.Next(100);
            base.GetComponent<PhotonView>().RPC("RPCA_SetChance", RpcTarget.All, new object[] { chance });
            if (chance <= 50)
            {
                component.RPCA_AddSilence(2f);
            }

            return HasToReturn.canContinue;
        }

        void Update()
        {
            if (gameObject.transform.parent == null)
                return;
            if (player.data.currentCards.Where(card => card.cardName == "Sin: Lust").Count() == 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }

        [PunRPC]
        public void RPCA_SetChance(int chance)
        {
            this.chance = chance;
        }
    }
}
