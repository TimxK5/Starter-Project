using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public GameObject Img1, Img2, Img3, Img4, Img5, Img6, Img7, Img8, Img9, Img10;
    public GameObject Inp1, Inp2, Inp3, Inp4, Inp5, Inp6, Inp7, Inp8, Inp9, Inp10;
    public GameObject IO1, IO2, IO3, IO4, IO5, IO6, IO7, IO8, IO9, IO10;
    public GameObject IntroText, EndText, TimerText, RestartText;

    public AudioSource Music;
    public AudioSource Sound;

    public AudioClip IntroS, PlayingM, LoseM, WinM, PositiveS, NegativeS;

    public Sprite Wicon, Aicon, Sicon, Dicon, Fail;

    int j = 0;

    float timeLeft = 10.0f;

    private bool Delay = true;
    private bool restart;

    List<string> Keys = new List<string>()
    {
        "W",
        "A",
        "S",
        "D"
    };
    List<string> Pattern = new List<string>();

    List<GameObject> Images = new List<GameObject>();

    List<GameObject> InputDisplay = new List<GameObject>();

    List<GameObject> InputOverlay = new List<GameObject>();

    List<string> PInput = new List<string>();

    void Start()
    {
        Time.timeScale = 1;
        restart = false;

        //Music = GetComponent<AudioSource>();
        //Sound = GetComponent<AudioSource>();

        Images.Add(Img1);
        Images.Add(Img2);
        Images.Add(Img3);
        Images.Add(Img4);
        Images.Add(Img5);
        Images.Add(Img6);
        Images.Add(Img7);
        Images.Add(Img8);
        Images.Add(Img9);
        Images.Add(Img10);

        InputDisplay.Add(Inp1);
        InputDisplay.Add(Inp2);
        InputDisplay.Add(Inp3);
        InputDisplay.Add(Inp4);
        InputDisplay.Add(Inp5);
        InputDisplay.Add(Inp6);
        InputDisplay.Add(Inp7);
        InputDisplay.Add(Inp8);
        InputDisplay.Add(Inp9);
        InputDisplay.Add(Inp10);

        InputOverlay.Add(IO1);
        InputOverlay.Add(IO2);
        InputOverlay.Add(IO3);
        InputOverlay.Add(IO4);
        InputOverlay.Add(IO5);
        InputOverlay.Add(IO6);
        InputOverlay.Add(IO7);
        InputOverlay.Add(IO8);
        InputOverlay.Add(IO9);
        InputOverlay.Add(IO10);



        Invoke("Intro", 0);

        Invoke("Generate", 2);

        StartCoroutine("DelayInput");

    }

    void Intro()
    {
        IntroText.GetComponent<Text>().text = "Input the combo correctly before time runs out!";
        Music.clip = IntroS;
        Music.Play();
    }
    void Generate()
    {
        IntroText.GetComponent<Text>().text = "";
        for (int i = 0; i < 10; i++)
        {
            int r = UnityEngine.Random.Range(0, 4);
            Pattern.Add(Keys[r]);
            if (r == 0)
            {
                Images[i].GetComponent<Image>().sprite = Wicon;
            }
            if (r == 1)
            {
                Images[i].GetComponent<Image>().sprite = Aicon;
            }
            if (r == 2)
            {
                Images[i].GetComponent<Image>().sprite = Sicon;
            }
            if (r == 3)
            {
                Images[i].GetComponent<Image>().sprite = Dicon;
            }

        }
        Music.clip = PlayingM;
        Music.Play();
    }

    IEnumerator DelayInput()
    {
        yield return new WaitForSeconds(2);

        Delay = false;
    }

    void Update()
    {
        if (Delay == false)
        {
            StartCoroutine("UserInput");
        }

        StartCoroutine("Timer");

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Scene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    IEnumerator UserInput()
    {

        if (j == 10)
        {
            Invoke("WinState", 0.1f);
            StopCoroutine("UserInput");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PInput.Add(Keys[0]);
            InputDisplay[j].GetComponent<Image>().sprite = Wicon;
            if (PInput[j] != Pattern[j])
            {
                InputOverlay[j].GetComponent<Image>().sprite = Fail;
                Sound.clip = NegativeS;
                Sound.Play();
                Invoke("LoseState", 0.1f);
            }
            else
            {
                Sound.clip = PositiveS;
                Sound.Play();
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PInput.Add(Keys[1]);
            InputDisplay[j].GetComponent<Image>().sprite = Aicon;
            if (PInput[j] != Pattern[j])
            {
                InputOverlay[j].GetComponent<Image>().sprite = Fail;
                Sound.clip = NegativeS;
                Sound.Play();
                Invoke("LoseState", 0.1f);
            }
            else
            {
                Sound.clip = PositiveS;
                Sound.Play();
                j++;
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PInput.Add(Keys[2]);
            InputDisplay[j].GetComponent<Image>().sprite = Sicon;
            if (PInput[j] != Pattern[j])
            {
                InputOverlay[j].GetComponent<Image>().sprite = Fail;
                Sound.clip = NegativeS;
                Sound.Play();
                Invoke("LoseState", 0.1f);
            }
            else
            {
                Sound.clip = PositiveS;
                Sound.Play();
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PInput.Add(Keys[3]);
            InputDisplay[j].GetComponent<Image>().sprite = Dicon;
            if (PInput[j] != Pattern[j])
            {
                InputOverlay[j].GetComponent<Image>().sprite = Fail;
                Sound.clip = NegativeS;
                Sound.Play();
                Invoke("LoseState", 0.1f);
            }
            else
            {
                Sound.clip = PositiveS;
                Sound.Play();
                j++;
            }
        }
        yield return null;
    }
    void WinState()
    {
        StopAllCoroutines();
        EndText.GetComponent<Text>().color = Color.green;
        EndText.GetComponent<Text>().text = "You Win!";
        RestartText.GetComponent<Text>().text = "Press R to Restart!";
        Music.clip = WinM;
        Music.Play();
        Time.timeScale = 0;
        restart = true;
    }
    void LoseState()
    {
        StopAllCoroutines();
        EndText.GetComponent<Text>().color = Color.red;
        EndText.GetComponent<Text>().text = "You Lose!";
        RestartText.GetComponent<Text>().text = "Press R to Restart!";
        Music.clip = LoseM;
        Music.Play();
        Time.timeScale = 0;
        restart = true;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        timeLeft -= Time.deltaTime;
        TimerText.GetComponent<Text>().text = "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            Invoke("LoseState", 0.1f);
            timeLeft = 0;
        }
        yield return null;
    }
}
