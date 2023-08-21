using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject suffleButton;

    public RectTransform bunny;
    public List<RectTransform> hats;

    public float duration = .5f;

    private bool gameStarted = false;
    private bool correctAnswer = false;

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitUntil(() => gameStarted == true);
        suffleButton.gameObject.SetActive(false);

        // Ground all hats
        for (int i = 0; i < hats.Count; i++)
        {
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

    }

    private void GameWin()
    {
        correctAnswer = true;
        StartCoroutine(UngroundHats());
        // score++;
        // conitue game;

        StartCoroutine(GameStart());
    }

    private void GameLose()
    {


        // game over canvas set active true;
        // game over;

        StartCoroutine(GameStart());
    }

    private IEnumerator UngroundHats()
    {
        yield return new WaitUntil(() => correctAnswer == true);
        correctAnswer = false;

        // Unground all hats && remove all action listeners
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }

        StartCoroutine(MoveToTarget(hats[0], new Vector2(-327f, -100f)));
        StartCoroutine(MoveToTarget(hats[1], new Vector2(0f, -100f)));
        StartCoroutine(MoveToTarget(hats[2], new Vector2(327f, -100f)));

        yield return new WaitForSeconds(duration + 0.25f);
    }


    private IEnumerator MoveToTarget(RectTransform currentPosition, Vector3 targetPosition)
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

}
