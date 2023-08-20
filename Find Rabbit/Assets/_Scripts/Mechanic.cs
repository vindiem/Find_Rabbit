using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject suffleButton;

    public RectTransform bunny;
    public List<RectTransform> hats;

    public float duration = 1.0f;

    private bool gameStarted = false;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitUntil(() => gameStarted == true);
        suffleButton.gameObject.SetActive(false);

        for (int i = 0; i < hats.Count; i++)
        {
            //hats[i].anchoredPosition = new Vector2(hats[i].anchoredPosition.x, -420f);
            StartCoroutine(MoveToTarget(hats[i], new Vector2(hats[i].anchoredPosition.x, -420f)));
        }

        // Set main hat (center)

        for (int i = 0; i < 10; i++)
        {
            // 10 times replace hats
        }

    }

    private IEnumerator MoveToTarget(RectTransform currentPosition, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startPosition = currentPosition.anchoredPosition;

        while (Vector3.Distance(currentPosition.anchoredPosition, targetPosition) > 0.01f)
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
