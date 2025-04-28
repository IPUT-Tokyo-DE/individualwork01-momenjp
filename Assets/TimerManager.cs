using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 表示用
    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = elapsedTime.ToString("F1") + " sec"; // ←ここ「 sec」に変更！
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTime()
    {
        return elapsedTime;
    }
}
