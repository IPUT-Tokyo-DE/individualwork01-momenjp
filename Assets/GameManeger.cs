using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class PhaseData
    {
        public GameObject[] enemies; // ���̃t�F�[�Y�ŏo���G�̃v���n�u
    }

    public PhaseData[] phases;            // �t�F�[�Y�̃f�[�^
    public Transform[] spawnPoints;       // �G���o������ꏊ
    public TextMeshProUGUI phaseText;     // �t�F�[�Y����\������UI
    public float phaseDisplayTime = 2f;   // �\�����ԁi�b�j

    public GameObject bossPrefab;         // �{�X�̃v���n�u
    public Transform bossSpawnPoint;      // �{�X�̏o���ʒu

    private int currentPhase = 0;         // ���݂̃t�F�[�Y�ԍ�

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
            Debug.Log("�S�t�F�[�Y�N���A�I");
            yield break;
        }

        // �t�F�[�Y���\��
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

        // �\�����ԑ҂�
        yield return new WaitForSeconds(phaseDisplayTime);

        // �\��������
        if (phaseText != null)
        {
            phaseText.gameObject.SetActive(false);
        }

        // �ŏI�t�F�[�Y�Ȃ�{�X���o��
        if (phaseIndex == phases.Length - 1)
        {
            GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss.tag = "Enemy";
            yield break;
        }

        // �ʏ�̓G�𐶐�
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
        // �G���S�����Ȃ��Ȃ����玟�̃t�F�[�Y��
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1)
        {
            currentPhase++;
            StartPhase(currentPhase);
        }
    }
}
