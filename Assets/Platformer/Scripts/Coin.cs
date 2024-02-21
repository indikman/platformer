using UnityEngine;

namespace Spatialminds.Platformer
{
    public class Coin : MonoBehaviour, ICollectable, ICollidable
    {
        string who;
        public void OnCollected()
        {
            Debug.Log($"Collected the coin by {who}");
        }

        public void OnCollided(GameObject other)
        {
            who = other.name;
            OnCollected();
        }
    }
}
