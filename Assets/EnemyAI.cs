using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;

    void Update()
    {
        if (target == null) return;

        // 移動方向の計算
        Vector2 direction = (target.position - transform.position).normalized;

        // 向きを変更する（進行方向に向く）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // △の向きを考慮して微調整

        // 移動処理
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
    public int hp = 3; // 3回当たったら倒れる
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // ★ 追加：GameManager に敵が倒されたことを通知
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.OnEnemyDefeated();
            }

            Destroy(gameObject); // HPが0になったら消える
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1); // 1ダメージ与える
            }
        }
    }

}