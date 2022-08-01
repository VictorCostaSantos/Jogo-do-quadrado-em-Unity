using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private RectTransform scoreRectTransform;

    public void Start()
    {
        scoreRectTransform.anchoredPosition = new Vector2(scoreRectTransform.anchoredPosition.x,14);

        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale
            (new Vector3(1.2f, 1.2f), 0.9f).setLoopPingPong();
    }
    public void menu()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f)
            .setOnComplete(onComplete);
       
    }
    private void onComplete()
    {
        scoreRectTransform.LeanMoveY(-43, 0.75f).setEaseInBounce();

        gameManager.Enable();
        Destroy(gameObject);
    }
}
