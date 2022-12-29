using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public GameObject rank;
    public GameObject scoreName;
    public GameObject score;

    public void SetScore(int rank, string name, int score)
    {
        this.rank.GetComponent<TextMeshProUGUI>().text = rank.ToString();
        this.scoreName.GetComponent<TextMeshProUGUI>().text = name;
        this.score.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
