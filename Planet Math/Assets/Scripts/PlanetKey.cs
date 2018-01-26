using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetKey : MonoBehaviour {

    [SerializeField] Planet idPlanet;

    [SerializeField] Text keyText;
    private int weight;
    
    public int Weight
    {
        set {
            weight = value;
            //if(keyText == null)
            //{
            //    print("oh yeah");
            //    print("x: " + weight);
            //}
            keyText.text = " = " + weight;
            print("" + weight);
        }
    }

	// Use this for initialization
	void Start () {
        //print("Start successful");
        keyText = this.gameObject.GetComponentInChildren<Text>();
        keyText.text = " = " + idPlanet.Weight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
