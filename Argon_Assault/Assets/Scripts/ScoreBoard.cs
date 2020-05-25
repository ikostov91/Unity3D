using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.InitializeScore();
    }

    public void ScoreHit(int score)
    {
        this._score += score;
        this._scoreText.text = this._score.ToString();
    }

    private void InitializeScore()
    {
        this._scoreText = GetComponent<Text>();
        this._scoreText.text = this._score.ToString();
    }
}
