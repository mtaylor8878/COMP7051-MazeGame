using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text leftScoreText;
    public Text rightScoreText;
    public Rigidbody ballPrefab;
    public Vector3 initialBallVector;
    private Rigidbody ball;

    private Player[] players;

	// Use this for initialization
	void Start () {
        players = new Player[2];
        players[0] = new Player(leftScoreText);
        players[1] = new Player(rightScoreText);

        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ball.tag = "Ball";
        ball.velocity = initialBallVector;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void endGame(int winner) {
        Destroy(ball);
        SceneManager.LoadScene(1);
    }

    public void scorePlayer(int player) {
        if(player < players.Length) {
            players[player].addToScore(1);
            if(players[player].getScore() < 5) {
                ball.position = Vector3.zero;
                ball.velocity = initialBallVector;
            } else {
                endGame(player);
            }
        }
    }

    public class Player {
        private int score;
        private Text textBox;

        public Player(Text tb) {
            textBox = tb;
            score = 0;
            updateScore();
        }

        public void resetScore() { score = 0; }

        public void addToScore(int newScore) {
            score += newScore;
            updateScore();
        }

        public int getScore() { return score; }

        private void updateScore() {
            textBox.text = score.ToString();
        }
    }
}