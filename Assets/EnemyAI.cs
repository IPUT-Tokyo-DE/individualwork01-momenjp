using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;

    void Update()
    {
        if (target == null) return;

        // �ړ������̌v�Z
        Vector2 direction = (target.position - transform.position).normalized;

        // ������ύX����i�i�s�����Ɍ����j
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // ���̌������l�����Ĕ�����

        // �ړ�����
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
    public int hp = 3; // 3�񓖂�������|���
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // �� �ǉ��FGameManager �ɓG���|���ꂽ���Ƃ�ʒm
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.OnEnemyDefeated();
            }

            Destroy(gameObject); // HP��0�ɂȂ����������
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1); // 1�_���[�W�^����
            }
        }
    }

}