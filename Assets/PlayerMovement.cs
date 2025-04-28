using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private PlayerHealth playerHealth;

    public AudioClip shootSE; // ← 追加：発射音
    private AudioSource audioSource; // ← 追加：AudioSource

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>(); // ← AudioSourceも取得！
    }

    void Update()
    {
        // 移動入力
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }

        // プレイヤーが無敵中なら弾を撃てない
        if (playerHealth != null && playerHealth.IsInvincible)
            return;

        // 弾を撃つ処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(lastMoveDirection);

        if (audioSource != null && shootSE != null)
        {
            audioSource.PlayOneShot(shootSE); // ← 弾を撃つたびに発射音！
        }
    }
}
