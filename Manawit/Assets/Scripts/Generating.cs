using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour {

    public Rigidbody cubePrefab;



	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        bool restart = Input.GetKeyDown(KeyCode.G);
        bool destroy = Input.GetKeyDown(KeyCode.H);
        bool test = Input.GetKeyDown(KeyCode.O);
        if (restart){
            GenerateAll();
        }
        if (destroy)
        {
            DestroyAll();
        }
        if (test) {
            Transform trans = cubePrefab.transform;
            int randomInt = (int)Mathf.Round(Random.Range(0, 6));
            Rigidbody instance = Instantiate(cubePrefab, trans.position + new Vector3(0.0f, 0.0f, 0.0f), trans.rotation) as Rigidbody;
            (instance.gameObject.GetComponent("ElementCube") as ElementCube).type = (Globals.ElementType)randomInt;
            (instance.gameObject.GetComponent("ElementCube") as ElementCube).ChangeColor();
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
                Rigidbody instance = Instantiate(cubePrefab, trans.position+new Vector3((float)j,0.0f,-(float)i), trans.rotation) as Rigidbody;
                (instance.gameObject.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
                (instance.gameObject.GetComponent("ElementCube") as ElementCube).ChangeColor();
                Globals.cubeList.Add(instance);
            }
        }
        this.CheckRepeat();
       
    }

    void DestroyAll(){


        for (int i = 0; i < Globals.cubeList.Count; i++)
        {
            if (Globals.cubeList[i] != null)
            {
                GameObject cur = Globals.cubeList[i].gameObject;
                Globals.cubeList.RemoveAt(i);
                Destroy(cur);
            }
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
                if ((Globals.cubeList[i*9+j].gameObject.GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[i*9+j-1].gameObject.GetComponent("ElementCube") as ElementCube).type)
                {
                    RepeatCount += 1;
                }
                else
                {
                    RepeatCount = 0;
                }
                if (RepeatCount == 2)
                {
                    current = (Globals.cubeList[i*9+j-1].gameObject.GetComponent("ElementCube") as ElementCube);
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
                if ((Globals.cubeList[i*9+j].gameObject.GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[(i-1)*9+j].gameObject.GetComponent("ElementCube") as ElementCube).type)
                {
                    RepeatCount += 1;
                }
                else
                {
                    RepeatCount = 0;
                }
                if (RepeatCount == 2)
                {
                    current = (Globals.cubeList[(i-1)*9+j].gameObject.GetComponent("ElementCube") as ElementCube);
                    current.type =(Globals.ElementType) ( ((int)current.type+1)%6);
                    current.ChangeColor();
                    RepeatCount = 0;

                }

            }
        }
    }

    public void playerSwapped(int row,int col,int desRow,int desCol){
        HashSet<Rigidbody> destroy = Globals.CheckThree(Globals.FindCube(row,col));
        destroy.UnionWith(Globals.CheckThree(Globals.FindCube(desRow,desCol)));
        Debug.Log(destroy.Count);
        foreach (Rigidbody cube in destroy)
        {
            Globals.cubeList.Remove(cube);
            int targetCol = cube.gameObject.GetComponent<ElementCube>().getCol();
            respawn(targetCol);
            Destroy(cube.gameObject);
        }
    }

    private void respawn(int col){
        Transform trans = cubePrefab.transform;
        int randomInt = (int)Mathf.Round(Random.Range(0, 6));
        Rigidbody instance = Instantiate(cubePrefab, trans.position+new Vector3((float)col,0.0f,10.0f), trans.rotation) as Rigidbody;
        (instance.gameObject.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
        (instance.gameObject.GetComponent("ElementCube") as ElementCube).ChangeColor();
        Globals.cubeList.Add(instance);
    }


}
