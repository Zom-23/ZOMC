using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using UnboundLib.GameModes;
using System.Collections;
using Sonigon;

namespace ZomC_Cards.MonoBehaviours
{
    class CatMono : RayHitEffect
    {
        static private Player player;
        static private HitInfo hit;


        private void Awake()
        {
            player = gameObject.GetComponent<Player>();
            hit = gameObject.GetComponent<HitInfo>();
        }

        void Update()
        {
            if (gameObject.transform.parent == null)
                return;
            if (player.data.currentCards.Where(card => card.cardName == "Nine Lives").Count() == 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
            
        }

        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (!hit.transform)
            {
                return HasToReturn.canContinue;
            }

            ProjectileHit projectileHit = this.GetComponentInParent<ProjectileHit>();
            DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
            if (component)
            {
                component.TakeDamageOverTime(0f * this.transform.forward, this.transform.position, this.time, this.interval, new Color(0, 0, 0), this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
            }

            return HasToReturn.canContinue;
        }
        

        [Header("Sounds")]
        public SoundEvent soundEventDamageOverTime;

        [Header("Settings")]
        public float time = .1f;
        public float interval = 0.1f;

    }
}
