using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;
    private bool isInvincible = false;
    public float invincibleTime = 2f; // 無敵時間（秒）
    public float flashInterval = 0.1f; // 点滅間隔

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log("Player took damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene("TitleScene"); // やられたらタイトルに戻る
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        while (elapsed < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    public bool IsInvincible
    {
        get { return isInvincible; }
    }
}
