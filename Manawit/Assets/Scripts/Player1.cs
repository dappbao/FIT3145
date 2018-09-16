using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
    public GameObject pipePrefab;
    private bool isSelect = false;
    private GameObject[] selection;
    private int row;
    private int col;
    public GameObject generateManager;
	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        this.selection = new GameObject[4];
	}
	
	// Update is called once per frame
	void Update () {
        
        bool pressUp = Input.GetKeyDown(KeyCode.W);
        bool pressDown = Input.GetKeyDown(KeyCode.S);
        bool pressLeft = Input.GetKeyDown(KeyCode.A);
        bool pressRight = Input.GetKeyDown(KeyCode.D);
        bool pressSelect = Input.GetKeyDown(KeyCode.Space);


        if(!isSelect)
        {
            if (pressUp&&row<8)
            {
                this.gameObject.transform.Translate(this.gameObject.transform.rotation*new Vector3(0.0f, 0.0f, 1.0f));
            }
            if (pressDown&&row>0)
            {
                this.gameObject.transform.Translate(this.gameObject.transform.rotation*new Vector3(0.0f, 0.0f, -1.0f));
            }
            if (pressLeft&&col>0)
            {
                this.gameObject.transform.Translate(this.gameObject.transform.rotation*new Vector3(-1.0f, 0.0f, 0.0f));
            }
            if (pressRight&&col<8)
            {
                this.gameObject.transform.Translate(this.gameObject.transform.rotation*new Vector3(1.0f, 0.0f, 0.0f));
            }
            if (pressSelect)
            {
                isSelect = true;
                GameObject instance1 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(0.0f,0.0f,1.0f), this.gameObject.transform.rotation);
                GameObject instance2 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(0.0f,0.0f,-1.0f), this.gameObject.transform.rotation);
                GameObject instance3 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(1.0f,0.0f,0.0f), this.gameObject.transform.rotation);
                GameObject instance4 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(-1.0f,0.0f,0.0f), this.gameObject.transform.rotation);
                instance1.GetComponent<Player1>().enabled= false;
                instance2.GetComponent<Player1>().enabled= false;
                instance3.GetComponent<Player1>().enabled= false;
                instance4.GetComponent<Player1>().enabled= false;
                selection[0] = instance1;
                selection[1] = instance2;
                selection[2] = instance3;
                selection[3] = instance4;

            }
        }else
        {
            int tmpRow = 0;
            int tmpCol = 0;
            if (pressUp)
            {
                tmpRow += 1;
            }else if (pressDown)
            {
                tmpRow += -1;
            }else if (pressLeft)
            {
                tmpCol += -1;
            }else if (pressRight)
            {
                tmpCol += 1;
            }
            if ((pressUp&&row<8)||(pressDown&&row>0)||(pressLeft&&col>0)||(pressRight&&col<8))
            {
                int desRow = row + tmpRow;
                int desCol = col + tmpCol;
                Globals.Swap(Globals.FindCube(row,col),Globals.FindCube(desRow,desCol), 1);
            }
            if (pressSelect||pressUp||pressDown||pressLeft||pressRight)
            {
                isSelect = false;
                for (int i = 0; i < 4; i++)
                {
                    Destroy(selection[i]);
                }
            }
        }
        col = Mathf.RoundToInt((float)(this.gameObject.transform.position.x+3.5));
        row=Mathf.RoundToInt((float)(this.gameObject.transform.position.z+3.5));
	}

}
