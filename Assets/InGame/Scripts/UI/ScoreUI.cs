using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ty.ProjectSubak.Game;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;

    [SerializeField] TextMeshProUGUI textUI;

    private void Awake()
    {
        score = -1;
    }

    void Update()
    {
        int tmp = GameManager.Instance.Score;
        if (score != tmp)
        {
            score = tmp;
            textUI.text = ("Score: " + tmp);
        }
    }
}
