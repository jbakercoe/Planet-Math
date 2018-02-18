using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] Text targetWeightText;
    [SerializeField] Text playerWeightText;
    [SerializeField] Text timerText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject[] targets;
    //[SerializeField] Planet[] dragPlanets;
    [SerializeField] PlanetKey[] planetKeys;
    //[SerializeField] Button startButton;
    [SerializeField] float maxTime = 45f;
    [SerializeField] Image endGameMenu;
    [SerializeField] Planet[] planets;

    private int targetWeight;
    private int userWeight;
    private int numPlanets;
    private int score;
    private bool isHighScore;
    private float timeLeft;
    private float minutes;
    private float seconds;
    private bool timerIsRunning = false;

    public int Score
    {
        get { return score; }
    }

    public int HighScore
    {
        get { return PlayerPrefs.GetInt("Highscore", 0); }
    }

    public bool IsHighScore
    {
        get { return isHighScore; }
    }

    private NumberGen[] scenarios = { new NumberGen(7, 25, new int[] { 2, 4, 5, 10 }, new int[] { 21, 23 }),
                                        new NumberGen(6, 24, new int[] { 2, 3, 5, 8 }, new int[] {17, 20, 22, 23 }),
                                        new NumberGen(7, 27, new int[] { 2, 5, 6, 9 }, new int[] {22, 25, 26 }),
                                        new NumberGen(7, 18, new int[] { 1, 3, 4, 6 }, new int[] {17})};

    // Use this for initialization
    void Start()
    {
        //toggleGameObjects(false);
        StartGame();
        endGameMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            timeLeft -= Time.deltaTime;
            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;
            if (seconds > 59) seconds = 59;
            if(minutes < 0)
            {
                timerIsRunning = false;
                minutes = 0;
                seconds = 0;
            }
            if (minutes == 0 && seconds == 0) endGame();
        }
    }

    //private void toggleGameObjects(bool isActive)
    //{
    //    foreach (var planet in dragPlanets)
    //    {
    //        planet.gameObject.SetActive(isActive);
    //    }
    //    foreach (var planetKey in planetKeys)
    //    {
    //        planetKey.gameObject.SetActive(isActive);
    //    }
    //    foreach (var target in targets)
    //    {
    //        target.gameObject.SetActive(isActive);
    //    }
    //    targetWeightText.gameObject.SetActive(isActive);
    //    playerWeightText.gameObject.SetActive(isActive);
    //    scoreText.gameObject.SetActive(isActive);
    //}

    public void StartGame()
    {
        generateData();
        UpdateScore();
        StartTimer();
        //startButton.gameObject.SetActive(false);
        //toggleGameObjects(true);
        score = 0;
        scoreText.text = "Score: 0";
        endGameMenu.gameObject.SetActive(false);
        isHighScore = false;
        clearCells();
    }

    private void StartTimer()
    {
        timerIsRunning = true;
        timeLeft = maxTime;
        StartCoroutine(UpdateTimerText());
    }

    private IEnumerator UpdateTimerText()
    {
        while (timerIsRunning)
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void generateData()
    {
        // pick a random scenario
        int randIndex = Random.Range(0, scenarios.Length);
        NumberGen scenario = scenarios[randIndex];
        print("RandIndex: " + randIndex);
        do
        {
            // generate a target weight that is not in the set's exceptions
            // i.e. pick a number that has at least one solution
            targetWeight = Random.Range(scenario.LowBound, scenario.UpBound + 1);
        } while (scenario.Exceptions.Contains(targetWeight));
        targetWeightText.text = "" + targetWeight;
        playerWeightText.text = "0";

        for(int i = 0; i < planets.Length; i++)
        {
            // sets the planet container to active if in the given scenario
            planets[i].SetUseable(scenario.Numbers.Contains(i+1));
        }

        //for(int i = 0; i < dragPlanets.Length; i++)
        //{
        //    dragPlanets[i].Weight = scenario.Numbers[i];
        //    planetKeys[i].Weight = scenario.Numbers[i];
        //}

    }

    public void UpdateScore()
    {
        userWeight = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].transform.childCount > 0)
            {
                // get item most recently dropped item in cell
                var planet = targets[i].gameObject.transform.GetChild(targets[i].transform.childCount - 1);
                userWeight += planet.GetComponent<Planet>().Weight;
            }
        }
        //print("userWeight: " + userWeight);
        playerWeightText.text = "" + userWeight;

        if (userWeight == targetWeight)
        {
            generateData();
            score++;
            scoreText.text = "Score: " + score;
            clearCells();
        }

    }

    private void clearCells()
    {
        foreach (GameObject target in targets)
        {
            for (int i = 0; i < target.transform.childCount; i++)
            {
                Destroy(target.gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void ClearCells()
    {
        clearCells();
        playerWeightText.text = "0";
    }

    private void endGame()
    {
        if(PlayerPrefs.GetInt("Highscore", 0) < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
            isHighScore = true;
        }
        //endGameMenu.gameObject.SetActive(true);
        Animator anim = endGameMenu.GetComponent<Animator>();
        anim.SetTrigger("Endgame");
    }
        
}
