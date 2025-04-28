using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 通常の敵にダメージ
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            // ボスにもダメージ
            BossController boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(1);
            }

            // 弾は消える
            Destroy(gameObject);
        }
    }
}
