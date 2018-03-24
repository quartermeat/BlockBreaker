using UnityEngine;

namespace Assets.Scripts
{
    public class LoseCollider : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            //Debug.Log("Trigger");
            LevelManager.LoadLoseLevel();
        }
    }
}
