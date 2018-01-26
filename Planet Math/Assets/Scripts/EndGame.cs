using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text message;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}//2358

    public void showText()
    {
        this.gameObject.SetActive(true);
        if (GameManager.Instance.IsHighScore)
        {
            message.text = "New High Score!";
        } else
        {
            message.text = "Great Job!";
        }
        scoreText.text = "Your Score: " + GameManager.Instance.Score;
    }

}
