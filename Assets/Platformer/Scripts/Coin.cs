using Unity.VisualScripting;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class Coin : MonoBehaviour, ICollectable, ICollidable
    {
        string who;
        private CoinCollectorService coinCollectorService;

        void Start()
        {
            coinCollectorService = ServiceLocator.instance.GetService<CoinCollectorService>();
        }


        public void OnCollected()
        {
            Debug.Log($"Collected the coin by {who}");
            coinCollectorService.CollectCoin();
            Destroy(gameObject);
        }

        public void OnCollided(GameObject other)
        {
            who = other.name;
            OnCollected();
        }
    }
}
