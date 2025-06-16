using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Gameobject Emo")]
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource flap;
    [SerializeField] private AudioSource abuchear;

    [Header("Vidas player")]
    [SerializeField] private GameObject[] vidas;
    [SerializeField] private int HitPlayer = 1;

    public bool HayPanelIntro = false;
    public GameObject PanelIntro;

    public GameObject panelPause;
    public AudioSource wititisound;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (HayPanelIntro)
        {
            PanelIntro.SetActive(true);
        }
        panelPause.SetActive(false);
    }
   
    public void audioHit()
    {
        hit.Play();
    }
    public void audioCoin()
    {
        coin.Play();
    }
    public void audioflap()
    {
        flap.Play();
    }
    public void audioAbuchear()
    {
        if (!abuchear.isPlaying)
        {
            abuchear.Play();
        }
    }
    public void VidasPlayer()
    {
        ResetGameobjectVida();
        print("hit: " + HitPlayer);
        if (HitPlayer == 1)
        {
            print("golpe 1");
            vidas[1].SetActive(true);
            HitPlayer = HitPlayer + 1;
        }
        else if (HitPlayer == 2)
        {
            print("golpe 2");
            vidas[2].SetActive(true);
            HitPlayer = HitPlayer + 1;
        }
        else if (HitPlayer == 3)
        {
            print("golpe 3");
            vidas[0].SetActive(true);
            HitPlayer = 1;
        }
    }
    void ResetGameobjectVida()
    {
        for(int y=0; y <=2; y++)
        {
            vidas[y].SetActive(false);
        }
    }

    public void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
        wititisound.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        wititisound.Play();
    }
    public void SalirJuego()
    {
        Application.Quit();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
