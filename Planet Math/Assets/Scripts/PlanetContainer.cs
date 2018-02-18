using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetContainer : MonoBehaviour {
    
    [SerializeField] byte alpha = 100;
    
    private bool isActive;

    public bool IsActive
    {
        set { isActive = value; }
        get { return isActive; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetUseable(bool active)
    {
        if (active)
        {
            alpha = 255;
        } else
        {
            alpha = 50;
        }
        transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, alpha);
        transform.GetChild(0).GetComponent<Planet>().SetDraggable(active);
        transform.GetChild(1).GetComponent<Text>().color = new Color32(0, 0, 0, alpha);
    }
}
