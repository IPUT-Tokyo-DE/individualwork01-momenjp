using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText;

    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            bestTimeText.text = $"Best Time: {bestTime:F1} sec"; // ←ここ「sec」になってる！
        }
        else
        {
            bestTimeText.text = "Best Time: --.- sec"; // ←ここも「sec」
        }
    }
}
