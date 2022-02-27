using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnboundLib;

namespace ZomC_Cards.MonoBehaviours
{
    class ConsumeMono : MonoBehaviour
    {
        Player player;
        bool active;
        bool effectReady;

        public void Awake()
        {
            player = gameObject.GetComponent<Player>();
            active = false;
            effectReady = true;
            player.data.block.BlockAction += OnBlock(player, player.data.block);
            player.data.stats.WasDealtDamageAction += OnWasDealtDamage;
        }

        private void Start()
        {
            
        }

        void Update()
        {

        }

        
        Action<BlockTrigger.BlockTriggerType> OnBlock(Player _player, Block _block)
        {
            return delegate (BlockTrigger.BlockTriggerType trigger)
            {
                effectReady = false;
                active = true;

                Unbound.Instance.ExecuteAfterSeconds(3f, () =>
                {
                    active = false;
                    Unbound.Instance.ExecuteAfterSeconds(2f, () => { effectReady = true; });
                });
            };  
        }

        void OnWasDealtDamage(Vector2 damage, bool selfDamage)
        {
            if(active)
            { player.data.healthHandler.Heal(damage.magnitude + (damage.magnitude * .1f)); } 
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }
    }
}
