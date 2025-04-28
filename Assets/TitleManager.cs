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
            bestTimeText.text = $"Best Time: {bestTime:F1} sec"; // �������usec�v�ɂȂ��Ă�I
        }
        else
        {
            bestTimeText.text = "Best Time: --.- sec"; // ���������usec�v
        }
    }
}
