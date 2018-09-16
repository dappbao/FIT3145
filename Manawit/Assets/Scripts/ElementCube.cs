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
    public GameObject generatingManager;

    private bool player1Flag=false;
    private bool player2Flag = false;
	// Use this for initialization
	void Start () {
        isLocked = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.transform.position, this.destination) < 0.1)
        {
            this.transform.position = this.destination;

            this.isLocked = false;
            if (this.player1Flag || this.player2Flag)
            {
                this.generatingManager.GetComponent<Generating>().playerSwapped(this.row, this.col);
                this.player1Flag = false;
                this.player2Flag = false;
            }

        }
        else
        {
            Vector3 direction = Vector3.Normalize(this.destination - this.transform.position);
            this.transform.position += direction * speed* Time.deltaTime;

        }

//        if (!isLocked && row != 0 && Globals.FindCube(row - 1, col) == null)
//        {
//            this.destination += new Vector3(0.0f, 0.0f, -1.0f);
//            this.isLocked = true;
//        }

        row=Mathf.RoundToInt((float)(this.transform.position.z+3));
        col=Mathf.RoundToInt((float)(this.transform.position.x+3));

	}

    public void ChangeColor(){
        switch (type)
        {
            case Globals.ElementType.Light:
                this.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case Globals.ElementType.Fire:
                this.GetComponent<Renderer>().material.color = Color.red;
                break;
            case Globals.ElementType.Water:
                this.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case Globals.ElementType.Wind:
                this.GetComponent<Renderer>().material.color = Color.green;
                break;
            case Globals.ElementType.Earth:
                this.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case Globals.ElementType.Dark:
                this.GetComponent<Renderer>().material.color = Color.black;
                break;
        }

    }



    public void setDestination(Vector3 dest) {
        this.destination = dest;
    }
    public Vector3 getDestination(){
        return this.destination;
    }

    public void setPlayerFlag(int a){
        switch (a)
        {
            case 1:
                this.player1Flag = true;
                break;
            case 2:
                this.player2Flag = true;
                break;
        }
    }

}
