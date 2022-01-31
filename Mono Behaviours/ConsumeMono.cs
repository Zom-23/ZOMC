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
        bool ready;

        public void Awake()
        {
            player = gameObject.GetComponent<Player>();
            ready = true;
        }

        private void Start()
        {
        }

        void Update()
        {
            if(ready)
            {
                player.data.block.BlockAction += OnBlock(player, player.data.block);
            }
        }

        Action<BlockTrigger.BlockTriggerType> OnBlock(Player _player, Block _block)
        {
            return delegate (BlockTrigger.BlockTriggerType trigger)
            {
                ready = false;
                player.data.block.BlockAction -= OnBlock(player, player.data.block);
                Unbound.Instance.ExecuteAfterSeconds(5f, () =>
                { ready = true; });

                player.data.stats.WasDealtDamageAction += OnWasDealtDamage;
                Unbound.Instance.ExecuteAfterSeconds(3f, () =>
                {
                    player.data.stats.WasDealtDamageAction -= OnWasDealtDamage;
                });
            };  
        }

        void OnWasDealtDamage(Vector2 damage, bool selfDamage)
        {
            player.data.healthHandler.Heal(damage.magnitude + (damage.magnitude * .1f));
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }
    }
}
