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

        private void Awake()
        {
            player = gameObject.GetComponent<Player>();
        }

        private void Start()
        {
            player.data.stats.WasDealtDamageAction += OnWasDealtDamage;
            Unbound.Instance.ExecuteAfterSeconds(3f, () =>
            {
                Destroy();
            });
        }

        void Update()
        {
            if(player.data.currentCards.Where(card => card.cardName == "Sin: Gluttony").Count() == 0)
            {
                Destroy();
            }
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
