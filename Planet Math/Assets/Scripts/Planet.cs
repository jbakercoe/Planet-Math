using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    [SerializeField] int weight;

    private bool isActive;
    private DragAndDropItem dragScript;

    public int Weight
    {
        get { return weight; }
        set { weight = value; }
    }
    
    public bool IsActive
    {
        set { isActive = value; }
        get { return isActive; }
    }

    // Use this for initialization
    void Start () {
        dragScript = gameObject.GetComponent<DragAndDropItem>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetDraggable(bool active)
    {
        dragScript.enabled = active;
    }

}
