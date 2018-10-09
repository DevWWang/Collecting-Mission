using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text gameMessage;

    public GameObject marblePlayerPrefab;
    private GameObject marblePlayer;

    public MarblesGenerator marblesGenerator;

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

        if (win)
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
        gameMessage.text = "Start!";
        yield return startWait;
    }

    IEnumerator Playing()
    {
        EnableAllMarbles();
        gameMessage.text = string.Empty;

        while (!win || !gameOver)
        {
            yield return null;
        }
    }

    IEnumerator Ending()
    {
        DisableAllMarbles();

        if (win)
        {
            gameMessage.text = "Win!";
        }
        else if (gameOver)
        {
            gameMessage.text = "Game Over";
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
        marblesGenerator.GenerateMarbles();
        marblePlayer.SetActive(true);
    }

    void DisableAllMarbles()
    {
        marblePlayer.SetActive(false);
        marblesGenerator.DisableAllMarbles();
    }
}
