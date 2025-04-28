using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null && !player.IsInvincible)
            {
                player.TakeDamage(1);
            }
        }
    }
}
