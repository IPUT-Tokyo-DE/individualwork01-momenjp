using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class PhaseData
    {
        public GameObject[] enemies; // このフェーズで出す敵のプレハブ
    }

    public PhaseData[] phases;            // フェーズのデータ
    public Transform[] spawnPoints;       // 敵が出現する場所
    public TextMeshProUGUI phaseText;     // フェーズ名を表示するUI
    public float phaseDisplayTime = 2f;   // 表示時間（秒）

    public GameObject bossPrefab;         // ボスのプレハブ
    public Transform bossSpawnPoint;      // ボスの出現位置

    private int currentPhase = 0;         // 現在のフェーズ番号

    void Start()
    {
        StartPhase(currentPhase);
    }

    void StartPhase(int phaseIndex)
    {
        StartCoroutine(ShowPhaseAndSpawn(phaseIndex));
    }

    IEnumerator ShowPhaseAndSpawn(int phaseIndex)
    {
        if (phaseIndex >= phases.Length)
        {
            Debug.Log("全フェーズクリア！");
            yield break;
        }

        // フェーズ名表示
        if (phaseText != null)
        {
            if (phaseIndex == phases.Length - 1)
            {
                phaseText.text = "Final Phase";
            }
            else
            {
                phaseText.text = $"Phase {phaseIndex + 1}";
            }

            phaseText.gameObject.SetActive(true);
        }

        // 表示時間待つ
        yield return new WaitForSeconds(phaseDisplayTime);

        // 表示を消す
        if (phaseText != null)
        {
            phaseText.gameObject.SetActive(false);
        }

        // 最終フェーズならボスを出す
        if (phaseIndex == phases.Length - 1)
        {
            GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss.tag = "Enemy";
            yield break;
        }

        // 通常の敵を生成
        for (int i = 0; i < phases[phaseIndex].enemies.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            GameObject enemy = Instantiate(phases[phaseIndex].enemies[i], spawnPoint.position, Quaternion.identity);

            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    enemyAI.target = player.transform;
                }
            }

            enemy.tag = "Enemy";
        }
    }

    public void OnEnemyDefeated()
    {
        // 敵が全部いなくなったら次のフェーズへ
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1)
        {
            currentPhase++;
            StartPhase(currentPhase);
        }
    }
}
