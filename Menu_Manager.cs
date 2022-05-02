using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Menu_Manager : MonoBehaviour
{
    public Panel current = null;

    private List<Panel> past = new List<Panel>();

    private LineRenderer laser;
    public SteamVR_Action_Source right;
    public SteamVR_Action_Source left;
    public SteamVR_Action_Boolean shootaction;
    public SteamVR_Action_Boolean confirm;


    private void Start()
    {
        Setups();
    }

    private void Setups()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
        {
            panel.Setup(this);
        }
        current.Show();
    }

    private void Update()
    {
        if(shootaction.GetStateDown(SteamVR_Input_Sources.Any) && (confirm.GetStateDown(SteamVR_Input_Sources.Any)))
        {
            back();
        }
    }

    public void back()
    {
        if(past.Count == 0)
        {
            print("no");
        }

        int last = past.Count -1; 

        SetCurrent(past[last]);
        past.RemoveAt(last);
    }

    public void setpast(Panel newPanel)
    {
        past.Add(current);
        SetCurrent(newPanel);
    }

    private void SetCurrent(Panel newPanel)
    {
        current.Hide();
        current = newPanel;
        current.Show();
    }
}
