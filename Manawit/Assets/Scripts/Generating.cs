using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour {

    public GameObject cubePrefab;
    private HashSet<GameObject> destroy;
    private bool startFlag;

	// Use this for initialization
	void Start () {
        destroy = new HashSet<GameObject>();
        InvokeRepeating("generateBackup", 0, 1);
        startFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool restart = Input.GetKeyDown(KeyCode.G);
        bool destroy = Input.GetKeyDown(KeyCode.H);
        bool test = Input.GetKeyDown(KeyCode.O);

        if (startFlag)
        {
            for (int i = 0; i < 9; i++)
            {
                
                if (Globals.getCol(i).Count < 9)
                {
                    if (Globals.backupCube[i]!=null){
                        GameObject current = Globals.backupCube[i];
                        Globals.backupCube[i] = null;
                        Globals.cubeList.Add(current);
                        current.GetComponent<BackupCube>().enabled = false;
                        current.GetComponent<ElementCube>().enabled = true;

                    }
                }
            }
        }
        if (restart){
            GenerateAll();
            startFlag = true;
        }
        if (destroy)
        {
            DestroyAll();
        }
        if (test) {
            Transform trans = cubePrefab.transform;
            int randomInt = (int)Mathf.Round(Random.Range(0, 6));
            GameObject instance = Instantiate(cubePrefab, trans.position, trans.rotation);
            instance.GetComponent<ElementCube>().setDestination(trans.position + new Vector3(1.0f, 0.0f, 1.0f));
            instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
            (instance.GetComponent("ElementCube") as ElementCube).type = (Globals.ElementType)randomInt;
            (instance.GetComponent("ElementCube") as ElementCube).ChangeColor();
        }
        if (this.destroy.Count != 0)
        {
            foreach (GameObject cube in this.destroy)
            {
                Globals.cubeList.Remove(cube);
//                int targetCol = cube.GetComponent<ElementCube>().col;
//                int targetRow = cube.GetComponent<ElementCube>().row;
//                respawn(targetRow,targetCol);

                Destroy(cube);
            }
            this.destroy.Clear();
        }

        for (int i = 0; i < 9; i++)
        {
            List<GameObject> currCol = Globals.getCol(i);
            for(int j=0;j<currCol.Count;j++){
                int gap = currCol[j].GetComponent<ElementCube>().row - j;
                if (!currCol[j].GetComponent<ElementCube>().isLocked && gap >= 1)
                {
                    for (int k = j; k < currCol.Count; k++)
                    {
                        currCol[k].GetComponent<ElementCube>().setDestination(currCol[k].GetComponent<ElementCube>().getDestination()+new Vector3(0.0f,0.0f,(float)(-gap)));
                        currCol[k].GetComponent<ElementCube>().isLocked = true;
                    }
                }
            }
        }

	}
    void GenerateAll(){
        
        DestroyAll();
        Transform trans = cubePrefab.transform;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int randomInt = (int)Mathf.Round(Random.Range(0, 6));
                GameObject instance = Instantiate(cubePrefab, trans.position+new Vector3((float)j,0.0f,-(float)i), trans.rotation);
                instance.GetComponent<ElementCube>().setDestination(trans.position + new Vector3((float)j, 0.0f, -(float)i));
                instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
                instance.GetComponent<ElementCube>().col = j;
                (instance.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
                (instance.GetComponent("ElementCube") as ElementCube).ChangeColor();
                Globals.cubeList.Add(instance);
            }

        }
        this.CheckRepeat();
       
    }

    void DestroyAll(){

        int count = Globals.cubeList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject cur = Globals.cubeList[0];
            Globals.cubeList.RemoveAt(0);
            Destroy(cur);

        }
    }

    void CheckRepeat(){
        int RepeatCount = 0;
        ElementCube current;
        for (int i = 0; i < 9; i++)
        {
            RepeatCount = 0;
            for (int j = 1; j < 9; j++)
            {
                if ((Globals.cubeList[i*9+j].GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[i*9+j-1].GetComponent("ElementCube") as ElementCube).type)
                {
                    RepeatCount += 1;
                }
                else
                {
                    RepeatCount = 0;
                }
                if (RepeatCount == 2)
                {
                    current = (Globals.cubeList[i*9+j-1].GetComponent("ElementCube") as ElementCube);
                    current.type =(Globals.ElementType) ( ((int)current.type+1)%6);
                    current.ChangeColor();
                    RepeatCount = 0;

                }

            }
        }

        for (int j = 0; j < 9; j++)
        {
            RepeatCount = 0;
            for (int i = 1; i < 9; i++)
            {
                if ((Globals.cubeList[i*9+j].GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[(i-1)*9+j].GetComponent("ElementCube") as ElementCube).type)
                {
                    RepeatCount += 1;
                }
                else
                {
                    RepeatCount = 0;
                }
                if (RepeatCount == 2)
                {
                    current = (Globals.cubeList[(i-1)*9+j].GetComponent("ElementCube") as ElementCube);
                    current.type =(Globals.ElementType) ( ((int)current.type+1)%6);
                    current.ChangeColor();
                    RepeatCount = 0;

                }

            }
        }
    }

    public void playerSwapped(int row,int col){
        destroy.UnionWith(Globals.CheckThree(Globals.FindCube(row,col)));
    }

    private void respawn(int row, int col){
        
        Transform trans = cubePrefab.transform;
        int randomInt = (int)Mathf.Round(Random.Range(0, 6));
        GameObject instance = Instantiate(cubePrefab, trans.position+new Vector3((float)col,0.0f,3.0f), trans.rotation);
        instance.GetComponent<ElementCube>().setDestination(trans.position + new Vector3((float)col, 0.0f, 2.0f));
        instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
        (instance.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
        (instance.GetComponent("ElementCube") as ElementCube).ChangeColor();
        Globals.cubeList.Add(instance);
    }

    private void generateBackup(){
        for (int i = 0; i < 9; i++)
        {
            if (Globals.backupCube[i] == null)
            {
                int randomInt = (int)Mathf.Round(Random.Range(0, 6));
                GameObject instance = Instantiate(cubePrefab, cubePrefab.transform.position+new Vector3((float)i,0.0f,3.0f), cubePrefab.transform.rotation);
                instance.GetComponent<ElementCube>().setDestination(cubePrefab.transform.position + new Vector3((float)i, 0.0f, 3.0f));
                instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
                instance.GetComponent<ElementCube>().type=(Globals.ElementType)randomInt;
                instance.GetComponent<ElementCube>().ChangeColor();
                instance.GetComponent<BackupCube>().type=(Globals.ElementType)randomInt;
                instance.GetComponent<BackupCube>().ChangeColor();
                instance.GetComponent<ElementCube>().enabled = false;
                instance.GetComponent<BackupCube>().enabled = true;

                Globals.backupCube[i]=instance;
            }
        }
    }


}
