using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossController : MonoBehaviour
{
    public int maxHp = 80;
    private int currentHp;

    public GameObject bulletPrefab;
    public GameObject laserPrefab;
    public GameObject warningLaserPrefab;
    public GameObject summonEnemyPrefab;
    public Transform firePoint;
    public Transform[] summonPoints;
    public GameObject missionCompleteText;
    public Slider hpSlider; // HPバー連携用

    void Start()
    {
        currentHp = maxHp;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = currentHp;
        }

        StartCoroutine(BossRoutine());
    }

    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;

        if (hpSlider != null)
        {
            hpSlider.value = currentHp;
        }

        if (currentHp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (missionCompleteText != null)
        {
            missionCompleteText.SetActive(true);
        }

        // タイマー停止＆クリアタイム保存
        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null)
        {
            float clearTime = timer.GetTime();

            if (!PlayerPrefs.HasKey("BestTime") || clearTime < PlayerPrefs.GetFloat("BestTime"))
            {
                PlayerPrefs.SetFloat("BestTime", clearTime);
                PlayerPrefs.Save();
            }
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("TitleScene");

        Destroy(gameObject);
    }

    IEnumerator BossRoutine()
    {
        transform.position += Vector3.down * 5f;
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return Attack1_Shot();
            yield return new WaitForSeconds(3f);

            yield return Attack2_Lasers();
            yield return new WaitForSeconds(3f);

            yield return Attack3_Summon();
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator Attack1_Shot()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) yield break;

        Vector2 dir = (player.transform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<BossBullet>().SetDirection(dir);

        yield return null;
    }

    IEnumerator Attack2_Lasers()
    {
        float angleStep = 30f;
        int laserCount = 5;
        float startAngle = -60f;

        for (int i = 0; i < laserCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            Instantiate(warningLaserPrefab, firePoint.position, rot);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < laserCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            Instantiate(laserPrefab, firePoint.position, rot);
        }

        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator Attack3_Summon()
    {
        foreach (Transform point in summonPoints)
        {
            GameObject summoned = Instantiate(summonEnemyPrefab, point.position, Quaternion.identity);

            EnemyAI enemyAI = summoned.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    enemyAI.target = player.transform;
                }
            }
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 1)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
    }
}
