using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererScript : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    public GameObject panel;
    public Image img;
    public Button btn;

    public GameObject player;
    public GameObject cam;

    Vector3[] points;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        cam.SetActive(true);
        Vector3 pos = new Vector3(0.48f, 7.8f, 0f);
        Vector3 rot = new Vector3(0f, 0f, 0f);

        img = panel.GetComponent<Image>();

        rend = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0] = Vector3.zero;

        points[1] = transform.position + new Vector3(0,0,20);
        rend.SetPositions(points);
        rend.enabled = true; 
    }
    
    public LayerMask layerMask;

    public bool AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool btnhit = false;
        if(Physics.Raycast(ray, out hit, layerMask))
        {
            btn = hit.collider.gameObject.GetComponent<Button>();
            points[1] = transform.forward + new Vector3(0,0,hit.distance);
            rend.startColor = Color.blue;
            rend.endColor = Color.blue;
            btnhit = true;

        }
        else
        {
            points[1] = transform.forward + new Vector3(0,0,20);
            rend.startColor = Color.white;
            rend.startColor = Color.white;
            btnhit = false;
        }
        rend.material.color = rend.startColor;
        rend.SetPositions(points);
        return btnhit;
    }
    // Update is called once per frame
    void Update()
    {
        AlignLineRenderer(rend);
    }

    public void Changecolor()
    {
        if (btn != null)
        {
            img.color = Color.green;
        }
    }
}
