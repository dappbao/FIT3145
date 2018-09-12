using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ElementCube : MonoBehaviour {
    public Globals.ElementType type;

    private Vector3 destination;
    public float speed;
    public bool isLocked;
    public int row;
    public int col;
	// Use this for initialization
	void Start () {
        isLocked = false;
	}
	
	// Update is called once per frame
	void Update () {
//        if (this.destination != null && this.gameObject.transform.position != this.destination) {
//            Vector3 direction = Vector3.Normalize(this.destination - this.gameObject.transform.position);
//            this.gameObject.transform.position += direction * speed;
//            
//        }
        row=Mathf.RoundToInt((float)(this.gameObject.transform.position.z+3));
        col=Mathf.RoundToInt((float)(this.gameObject.transform.position.x+3));

	}

    public void ChangeColor(){
        switch (type)
        {
            case Globals.ElementType.Light:
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case Globals.ElementType.Fire:
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case Globals.ElementType.Water:
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case Globals.ElementType.Wind:
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case Globals.ElementType.Earth:
                this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case Globals.ElementType.Dark:
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;
        }

    }



    public void setDestination(Vector3 dest) {
        this.destination = dest;
    }



}
