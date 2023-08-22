using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject suffleButton;

    public RectTransform bunny;
    public List<RectTransform> hats;

    public float duration = 1.7f;

    private bool gameStarted = false;

    private int bestScore = 0;
    private int score = 0;

    public Text scoreText;
    public Text goScoreText;
    public Text goBestScoreText;

    public GameObject gameOverCanvas;
    private bool paused = false;
    public GameObject pauseCanvas;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best score");

        gameOverCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);

        StartCoroutine(GameStart());
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";
        goScoreText.text = $"Score: {score}";
        goBestScoreText.text = $"Record: {bestScore}";
    }

    private IEnumerator GameStart()
    {
        yield return new WaitUntil(() => gameStarted == true);
        suffleButton.gameObject.SetActive(false);

        // Ground all hats
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].pivot = new Vector2(0.5f, 0.5f);
            StartCoroutine(MoveToTarget(hats[i], new Vector2(hats[i].anchoredPosition.x, -420f)));
        }

        yield return new WaitForSeconds(duration + 0.15f);

        // 327, 0, -327
        bunny.gameObject.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            // 10 times replace hats
            int randomIndex1 = Random.Range(0, hats.Count);
            int randomIndex2 = Random.Range(0, hats.Count);

            if (randomIndex1 != randomIndex2)
            {
                StartCoroutine(MoveToTarget(hats[randomIndex1], new Vector2(hats[randomIndex2].anchoredPosition.x, -420f)));
                StartCoroutine(MoveToTarget(hats[randomIndex2], new Vector2(hats[randomIndex1].anchoredPosition.x, -420f)));

                //StartCoroutine(SwapObjects(hats[randomIndex1], hats[randomIndex2], duration));

                yield return new WaitForSeconds(duration + 0.2f);
            }
            else
            {
                i++;
            }
        }

        // Set main hat (center)
        // Add Action Listeners
        hats[0].gameObject.GetComponent<Button>().onClick.AddListener(GameLose);
        hats[1].gameObject.GetComponent<Button>().onClick.AddListener(GameWin);
        hats[2].gameObject.GetComponent<Button>().onClick.AddListener(GameLose);

        bunny.transform.position = hats[1].transform.position;
        bunny.gameObject.SetActive(true);

        gameStarted = false;

    }

    private void GameWin()
    {
        StartCoroutine(UngroundHats());
        score++;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Best score", score);
        }

        // conitue game;
        StartCoroutine(GameStart());
    }

    private void GameLose()
    {
        StartCoroutine(UngroundHats());
        // game over canvas set active true;
        // game over;

        gameOverCanvas.gameObject.SetActive(true);

    }

    private IEnumerator UngroundHats()
    {
        // Unground all hats && remove all action listeners
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].pivot = new Vector2(0.5f, -1.5f);
            hats[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }

        yield return new WaitForSeconds(duration + 0.25f);

        gameStarted = true;

    }


    private IEnumerator MoveToTarget(RectTransform currentPosition, Vector2 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startPosition = currentPosition.anchoredPosition;

        while (Vector3.Distance(startPosition, targetPosition) > 0.01f)
        {
            currentPosition.anchoredPosition = Vector3.Lerp(currentPosition.anchoredPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentPosition.anchoredPosition = targetPosition;
    }

    public void Shuffle()
    {
        gameStarted = true;
    }

    public void SetPause()
    {
        paused = !paused;
        Time.timeScale = (paused == true) ? 0.0f : 1.0f;
        pauseCanvas.SetActive(paused);
    }

}
