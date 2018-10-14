using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text gameMessage;

    public GameObject marblePlayerPrefab;
    private GameObject marblePlayer;

    public MarblesGenerator marblesGenerator;
    public GroundDetection groundDetection;

    public float startDelay = 3f;
    public float endDelay = 3f;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;

    private bool win;
    private bool gameOver;

    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(Starting());
        yield return StartCoroutine(Playing());
        yield return StartCoroutine(Ending());

        if (groundDetection.EndGame())
        {
            SceneManager.LoadScene("miniGame_2");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    IEnumerator Starting()
    {
        SetMarblePlayer();
        gameMessage.text = "START";
        yield return startWait;
    }

    IEnumerator Playing()
    {
        EnableAllMarbles();
        gameMessage.text = string.Empty;

        while (!groundDetection.EndGame())
        {
            //Debug.Log("Game Continues");
            yield return null;
        }
    }

    IEnumerator Ending()
    {
        DisableAllMarbles();

        if (groundDetection.GetAllTargets())
        {
            gameMessage.text = "WIN";
        }
        else if (groundDetection.EndGame())
        {
            gameMessage.text = "Game Over ";
        }

        yield return endWait;
    }

    void SetMarblePlayer()
    {
        marblePlayer = Instantiate(marblePlayerPrefab);
        marblePlayer.SetActive(false);
    }

    void EnableAllMarbles()
    {
        marblePlayer.SetActive(true);
        marblesGenerator.GenerateMarbles();
    }

    void DisableAllMarbles()
    {
        marblePlayer.SetActive(false);
        marblesGenerator.DisableAllMarbles();
    }
}
