using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null && !player.IsInvincible)
            {
                player.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
