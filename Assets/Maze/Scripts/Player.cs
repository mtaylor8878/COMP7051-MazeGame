﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private bool toggle = true;
    public static int winCondition = 0;
    public static int loseCondition = 0;
    public GameObject pov;
    public Shader mainShader;
    public GameObject sun;
    public GameObject ballPrefab;
    public Text scoreHUD;
    public static int score;

    public bool musicPlaying = true;
    public static AudioSource bgm;
    public AudioClip daytime;
    public AudioClip nightTime;
    public AudioClip hit;
    public static float volumeMax;

    private bool day = true;
    private bool fog = true;
    private bool flashLight = false;

    // Use this for initialization
    void Start() {
        pov.GetComponent<Camera>().SetReplacementShader(mainShader, null);
        sun = GameObject.Find("Sun");
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        playDay();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Phase")) {
            toggle = !toggle;
            gameObject.layer = toggle ? 9 : 10;
        }
        if (Input.GetButtonDown("Fog")) {
            toggleFog();
        }
        if (Input.GetButtonDown("Day")) {
            setTime(!day);
        }
        if (Input.GetButtonDown("Flashlight")) {
            toggleFlashLight();
        }

        if (Input.GetButtonDown("Music"))
        {
            toggleMusic();
        }

        if (Input.GetButtonDown("Throw"))
        {
            GameObject ball = Instantiate(ballPrefab, GameObject.Find("HandPosition").transform);
            ball.transform.localPosition = Vector3.zero;
            ball.GetComponent<Rigidbody>().AddForce((pov.transform.forward) * 250);
            ball.transform.parent = GameObject.Find("GameController").transform;
            Destroy(ball, 5.0f);
        }
        scoreHUD.text = "Score: " + score;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.tag == "Goal")
        {
            winCondition++;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().clip = hit;
            GetComponent<AudioSource>().Play();
        }

        if (other.gameObject.tag == "Door") {
            SceneManager.LoadScene(3);
        }
    }

    public void setTime(bool day) {
        this.day = day;
        Shader.SetGlobalInt("_Day", day ? 1 : 0);
        if(day) {
            sun.GetComponent<Light>().intensity = 1;
            sun.transform.localRotation = Quaternion.Euler(50f,-30f,0f);
            playDay();
        } else {
            sun.GetComponent<Light>().intensity = 0.2f;
            sun.transform.localRotation = Quaternion.Euler(195f, -30f, 0f);
            playNight();
        }
    }

    public void toggleFog() {
        fog = !fog;
        Shader.SetGlobalInt("_Fog", fog ? 1 : 0);
        if (fog)
            bgm.volume /= 2;
        else
            bgm.volume *= 2;
    }

    public void toggleFlashLight() {
        flashLight = !flashLight;
        Shader.SetGlobalInt("_Light", flashLight ? 1 : 0);
    }

    public void toggleMusic()
    {
        musicPlaying = !musicPlaying;
        Debug.Log("Music " + musicPlaying);
        if (musicPlaying) {
            bgm.Play();
        } else {
            bgm.Stop();
        }
    }

    public void playDay()
    {
        bgm.clip = daytime;
        if(musicPlaying)
            bgm.Play();
    }

    public void playNight()
    {
        bgm.clip = nightTime;
        if (musicPlaying)
            bgm.Play();
    }

    public void stopMusic()
    {
        bgm.Stop();
    }
}
