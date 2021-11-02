using UnityEngine;
using UnboundLib;
using Photon.Pun;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnboundLib.Utils;
using ZomC_Cards.Cards;
using HarmonyLib;

namespace ZomC_Cards
{
    class AdaptMagMono : MonoBehaviour
    {
        private int increaseCount;
        private int reloadCount;
        private Player player;
        bool ready;
        private Gun gun;

        void Awake()
        {
            player = gameObject.GetComponent<Player>();
            gun = gameObject.GetComponent<Gun>();
        }

        void Start()
        {
            this.increaseCount = 0;
            this.reloadCount = 0;
            this.ready = false;
        }

        void Update()
        {
            if(!ready && gun.isReloading)
            {
                this.ready = true;
                if (reloadCount == increaseCount)
                    reloadCount++;
            }
            if(ready && (increaseCount < reloadCount))
            {
                this.gun.ammo += 1;
                this.increaseCount++;
            }
            if(!gun.isReloading)
            {
                this.ready = false;
            }
        }
    }
}
