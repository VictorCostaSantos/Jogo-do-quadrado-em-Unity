using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject inimigoPrefab;
    public TMPro.TextMeshProUGUI ScoreText;



    public int score;
    public float timer;
    public  bool gameOver;

    private Coroutine SpawInimigo;

    public Image image;

    public GameObject mainVCam;
    public GameObject zoomVcam;

    public GameObject Player;

    public GameObject GameOverMenu;

    public static GameManager instance;

    private int highScore;
    private const string highScorePreferencekey = "HighScore";

    public static GameManager Instance => instance;
    public int HighScore => highScore;
    void Start()
    {
        instance = this;

        highScore = PlayerPrefs.GetInt(highScorePreferencekey);
        Debug.Log(highScore);

    }

    private void OnEnable()
    {

        Player.SetActive(true);

        zoomVcam.SetActive(false);
        mainVCam.SetActive(true);

        gameOver = false;
        ScoreText.text = "0";
        score = 0;
        timer = 0;

        StartCoroutine(SpawnInimigo());
    }


    private void Update()
    {
        print(image.gameObject);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.01f));
                image.gameObject.SetActive(false);
            }
            if(Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.01f));
                image.gameObject.SetActive(true);
            }
        }

        timer += Time.deltaTime;

        if (gameOver)
            return;

        if (timer >= 1f)
        {
            score++;
            ScoreText.text = score.ToString();
            timer = 0;
        }
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.1f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }
        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
    
    }




    private IEnumerator SpawnInimigo()
    {
        var SpawnToInimigo = Random.Range(1,2);

        for (int i = 0; i < SpawnToInimigo; i++)
        {
            var x = Random.Range(-9, 9);
            Instantiate(inimigoPrefab, new Vector3(x, 210, 0), Quaternion.identity);
        }

        

        yield return new WaitForSeconds(1f);

        yield return SpawnInimigo();
    }
    public void GameOver()
    {
        StopCoroutine(SpawnInimigo());
        gameOver = true;

       mainVCam.SetActive(false);
        zoomVcam.SetActive(true);
        
        gameObject.SetActive(false);
        GameOverMenu.SetActive(true);

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScorePreferencekey, highScore);
            Debug.Log(highScore);
        }

    }
    public void Enable()
    {
        gameObject.SetActive(true);

    }
}
