using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ty.ProjectSubak.Game;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class Score : MonoBehaviour
    {
        private int score;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Awake()
        {
            score = -1;
            if (PlayerPrefs.HasKey("HighScore")) highScoreText.text = ("High Score: " + PlayerPrefs.GetInt("HighScore"));
            else highScoreText.text = ("High Score: 0");
        }

        void Update()
        {
            int tmp = GameManager.Instance.Score;
            if (score != tmp)
            {
                score = tmp;
                scoreText.text = ("Score: " + tmp);
            }
        }
    }
}

