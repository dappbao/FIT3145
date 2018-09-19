using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {
    
    public GameObject pipePrefab;
    private bool isSelect = false;
    private GameObject[] selection;
    private int row;
    private int col;
    public GameObject generateManager;
    private int id;
    public int hp;
    public int[] inventory;
    // Use this for initialization
    void Start () {
        id = 2;
        this.gameObject.GetComponent<Renderer>().material.color = new Color((float)102/255,(float)204/255,1.0f,1.0f);
        this.selection = new GameObject[4];
        hp = 20;
        inventory = new int[]{ 0, 0, 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update () {

        bool pressUp = Input.GetKeyDown(KeyCode.UpArrow);
        bool pressDown = Input.GetKeyDown(KeyCode.DownArrow);
        bool pressLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool pressRight = Input.GetKeyDown(KeyCode.RightArrow);
        bool pressSelect = Input.GetKeyDown(KeyCode.L);


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

                selection[0] = instance1;
                selection[1] = instance2;
                selection[2] = instance3;
                selection[3] = instance4;
                foreach (GameObject pipe in selection)
                {
                    pipe.GetComponent<Renderer>().material.color = new Color((float)102/255,(float)204/255,1.0f,1.0f);
                }
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
                Globals.Swap(Globals.FindCube(row,col),Globals.FindCube(desRow,desCol), id);
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


    public int getId(){
        return this.id;
    }
    public void getAnElement(Globals.ElementType element){
        this.inventory[(int)element] += 1;
    }
}
