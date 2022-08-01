using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private LTDescr recebeltwenn ;

    [SerializeField]
    private TMPro.TextMeshProUGUI highscore;
    private void OnEnable()
    {
        highscore.text = $"Hish Score: {GameManager.Instance.HighScore}";


        var rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, rectTransform.rect.height);

        rectTransform.LeanMoveY(0, 1f).setEaseInOutElastic().delay = 0.5f;

        if(recebeltwenn is null)
        {
            
            recebeltwenn = GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale
               (new Vector3(1.2f, 1.2f), 0.9f).setLoopPingPong();

            
        }

        recebeltwenn.resume();

    }
    public void Restart()
    {
        Debug.Log("cliquei");


        recebeltwenn.pause();
        gameObject.SetActive(false);

        GameManager.Instance.Enable();

       
    }

    public void Quit()
    {
        
        Application.Quit();
    }
}
