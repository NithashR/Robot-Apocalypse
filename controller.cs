using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
using UnityEngine.UI;


public class controller : MonoBehaviour
{
    public SteamVR_Input_Sources hand;
    private LineRenderer laser;
    public SteamVR_Action_Boolean shootaction;
    public SteamVR_Action_Boolean confirm;
    public controller con;
    private RaycastHit hit;
    private bool aim = false;
    public Button buttons;
    public LayerMask layerMask;
    //private Camera sub;
    public GameObject player;
    public GameObject cam;

    public Panel startpage;
    public Panel controlspage;
    public Panel gameover;

    public Starter over; 

    public AudioListener playera;
    public AudioListener cama;

    // Start is called before the first frame update

    void Start()
    {
        //playera = player.GetComponent<AudioListener>();
        //cama = cam.GetComponent<AudioListener>();
        cama.enabled = false;
        playera.enabled = true;
        cam.SetActive(true);
        laser = this.GetComponent<LineRenderer>();
        laser.enabled = false;
        controller con = gameObject.GetComponent<controller>();
    }

    void Update()
    {
        if(shootaction.GetStateDown(hand) || aim == true)
        {
            aim = true;
            Vector3 angle1 = this.transform.forward;
            Quaternion anglespread = Quaternion.AngleAxis(30, new Vector3(1,0,0));
            Vector3 newangle = anglespread * angle1;

            laser.enabled = true;
            Ray ray = new Ray(this.transform.position, newangle);
            //laser.startColor = Color.blue;
            //laser.endColor = Color.blue;
            laser.SetPosition(0, con.transform.position);

            if (Physics.Raycast(ray, out hit, layerMask))         
            {
                buttons = hit.collider.gameObject.GetComponent<Button>();
                laser.SetPosition(1, hit.point);
                laser.startColor = Color.blue;
                laser.endColor = Color.blue;
                ChangeScreen();
            }
            else                                               
            {
                laser.startColor = Color.red;
                laser.endColor = Color.red;
                laser.SetPosition(1, ray.GetPoint(100));
            }
            
        }

        if(shootaction.GetStateUp(hand))
        {
            aim = false;
            laser.enabled = false;
        }

    }

    public void ChangeScreen()
    {
        if (buttons != null)
        {
            if(confirm.GetStateDown(hand) && buttons.name == "Start")
            {
                switchCam();
            }

            if(confirm.GetStateDown(hand) && buttons.name == "Play Again")
            {
                switchCam();
            }

            if(confirm.GetStateDown(hand) && buttons.name == "Controls")
            {  
                startpage.Hide();
                controlspage.Show();
            }

            if(confirm.GetStateDown(hand) && buttons.name == "Back")
            {  
                startpage.Show();
                controlspage.Hide();
            }

            if(confirm.GetStateDown(hand) && buttons.name == "Menu")
            {  
                startpage.Show();
                gameover.Hide();
            }

            if(confirm.GetStateDown(hand) && buttons.name == "Quit")
            {  
                print("Closing Game");
                Application.Quit();
            }

        }
    }

    public void switchCam()
    {
        cam.SetActive(false);
        //cama.enabled = false;

        //player.SetActive(true);
        //playera.enabled = true;
    }

    public void switchCamback()
    {
        cam.SetActive(true);
        //cama.enabled = false;

        player.SetActive(false);
        //playera.enabled = true;
    }

}
