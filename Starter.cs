using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent<Collider> onTriggerEnter;
    public bool begin = false;
    public TextMeshPro start;
    public Material red;
    public Material green;

    public static int hp = 100;
    private int points =0;
    private int holdsecs = 0;
    private int holdmins = 0;

    public TextMeshPro score;
    public TextMeshPro time;
    public TextMeshPro health;

    public TextMeshPro scoreV;
    public TextMeshPro timeV;
    public TextMeshPro healthV;

    public GameObject player;
    public GameObject cam;

    public Camera cams;
    public Camera players;


    public Panel startpage;
    public Panel controlspage;
    public Panel gameover;

    public TextMeshProUGUI overscore;

    //public controller con;

    float timer =0.0f;

    private void Update() 
    {
        if(begin == true)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            start.fontSize = 1.7f;
            start.SetText("Survive");
            this.GetComponent<MeshRenderer>().material = red;

            score.SetText("Score");
            time.SetText("Time");
            health.SetText("Health");

            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer/60f);
            int seconds = Mathf.FloorToInt(timer-minutes * 60);
            string outt = string.Format("{0:00}:{1:00}", minutes, seconds);
            timeV.SetText(outt);

            healthV.SetText(Healthmanager.Health.ToString());

            if(Healthmanager.Health<=0)
            {
                gameObject.GetComponent<Collider>().enabled = true;
                begin = false;
                GameDone();
            }

            scoreV.SetText(pointsmanager.totalp.ToString());
            
            if(seconds == holdsecs+10)
            {
                pointsmanager.totalp+=5;
                holdsecs = seconds;
            }

            if(minutes == holdmins+1)
            {
                pointsmanager.totalp+=10;
                holdmins = minutes;
            }

        } 

    }

    void OnTriggerEnter(Collider col)
    {
        if( col.gameObject.tag == "bullet")
        {
            begin = true;
            print("Let the Game Begin");
            Healthmanager.Health = 100;
            pointsmanager.totalp=0;
            timer =0.0f;
        }
    }

    public void GameDone()
    {
        start.fontSize = 2.5f;
        start.SetText("Start");
        this.GetComponent<MeshRenderer>().material = green;

        score.SetText("");
        time.SetText("");
        health.SetText("");

        scoreV.SetText("");
        timeV.SetText("");
        healthV.SetText("");

        switchCamback();
        controlspage.Hide();
        startpage.Hide();
        gameover.Show();

        overscore.SetText("Score: "+pointsmanager.totalp.ToString());

        GameObject[] robots;
        robots = GameObject.FindGameObjectsWithTag("robot");

        foreach (GameObject robot in robots)
        {
            Destroy(robot);
        }
    }

    public void switchCamback()
    {
        //player.SetActive(false);
        cam.SetActive(true);
        //players.enabled = false;
        //cams.enabled = true;

    }

}
