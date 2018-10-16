using UnityEngine;
using System.Collections.Generic;

public static class Globals {
    public enum ElementType{Light,Fire, Water, Wind, Earth, Dark};

    public static List<GameObject> cubeList = new List<GameObject>(81);

    public static GameObject[] backupCube = new GameObject[9];

    public static GameObject FindCube(int row,int col) {
        if (row < 0 || row > 8 || col < 0 || col > 8)
        {
            return null;
        }
        for (int i = 0; i < cubeList.Count; i++) {
            GameObject cur = cubeList[i];

            if (cur.GetComponent<ElementCube>().col==col && cur.GetComponent<ElementCube>().row==row && !cur.GetComponent<ElementCube>().isLocked) {

                return cubeList[i];
            }
        }
        return null;
    }

    public static bool CheckCubeNotExist(int row,int col) {
        if (cubeList.Count == 0)
        {
            return true;
        }

        foreach (GameObject cube in cubeList)
        {
            if (cube.GetComponent<ElementCube>().col==col && cube.GetComponent<ElementCube>().row==row) {

                return false;
            }
        }
        return true;
    }

    public static void Swap(GameObject cube1, GameObject cube2, int a) {
        if (cube1 == null || cube2 == null || cube1.GetComponent<ElementCube>().isLocked || cube2.GetComponent<ElementCube>().isLocked)
        {
            return;
        }
        cube1.GetComponent<ElementCube>().setDestination(cube2.transform.position);
        cube2.GetComponent<ElementCube>().setDestination(cube1.transform.position);
        cube1.GetComponent<ElementCube>().setPlayerFlag(a);
        cube2.GetComponent<ElementCube>().setPlayerFlag(a);
        cube1.GetComponent<ElementCube>().isLocked = true;
        cube2.GetComponent<ElementCube>().isLocked = true;
    }

    public static Vector3 TranformPlayerToCube(Vector3 pos){
        Vector3 result = pos;
        result += new Vector3(0.5f, -1.0f, 0.5f);
        return result;
    }
        
    public static HashSet<GameObject> CheckThree(GameObject cube1){
        HashSet<GameObject> result = new HashSet<GameObject>();
        List<GameObject> row = getRow(cube1.GetComponent<ElementCube>().row);
        List<GameObject> col = getCol(cube1.GetComponent<ElementCube>().col);


        int curRow = cube1.GetComponent<ElementCube>().row;
        int curCol = cube1.GetComponent<ElementCube>().col;
        int start=-1,end=-1;

        foreach(GameObject cube in row){
            if (cube.GetComponent<ElementCube>().col == curCol)
            {
                start = row.IndexOf(cube);
                end = row.IndexOf(cube);
                break;
            }
        }

        for (int i = start; i > 0; i--)
        {
            if (row.Count <= i || row[i - 1].GetComponent<ElementCube>().col != row[i].GetComponent<ElementCube>().col - 1 || row[i - 1].GetComponent<ElementCube>().isLocked)
            {
                break;
            }
            else if (row[i - 1].GetComponent<ElementCube>().type == row[i].GetComponent<ElementCube>().type)
            {
                start -= 1;
            }
            else
            {
                break;
            }
        }
        for (int i = end; i < 8; i++)
        {
            if (row.Count<=i+1 ||row[i+1].GetComponent<ElementCube>().col!=row[i].GetComponent<ElementCube>().col+1 || row[i+1].GetComponent<ElementCube>().isLocked){
                break;
            }else if(row[i+1].GetComponent<ElementCube>().type==row[i].GetComponent<ElementCube>().type){
                end += 1;
            }else
            {
                break;
            }
        }

        if (end - start >= 2)
        {
            for (int i = start; i <= end; i++)
            {
                result.Add(row[i]);
            }
        }



        foreach(GameObject cube in col){
            if (cube.GetComponent<ElementCube>().row == curRow)
            {
                start = end = col.IndexOf(cube);
                break;
            }
        }
        for (int i = start; i > 0; i--)
        {
            if (col.Count<=i || col[i-1].GetComponent<ElementCube>().row!=col[i].GetComponent<ElementCube>().row-1 || col[i-1].GetComponent<ElementCube>().isLocked){
                break;
            }else if(col[i-1].GetComponent<ElementCube>().type==col[i].GetComponent<ElementCube>().type){
                start -= 1;
            }else
            {
                break;
            }
        }
        for (int i = end; i < 8; i++)
        {
            if (col.Count<=i+1 ||col[i+1].GetComponent<ElementCube>().row!=col[i].GetComponent<ElementCube>().row+1 || col[i+1].GetComponent<ElementCube>().isLocked){
                break;
            }else if(col[i+1].GetComponent<ElementCube>().type==col[i].GetComponent<ElementCube>().type){
                end += 1;
            }else
            {
                break;
            }
        }

        if (end - start >= 2)
        {
            for (int i = start; i <= end; i++)
            {
                result.Add(col[i]);
            }
        }


        return result;
    }




    private static List<GameObject> getRow(int row){
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].GetComponent<ElementCube>().row==row) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(GameObject x, GameObject y)
            {
                return x.GetComponent<ElementCube>().col.CompareTo(y.GetComponent<ElementCube>().col);
            });
        return result;
    }

    public static List<GameObject> getCol(int col){
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].GetComponent<ElementCube>().col==col) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(GameObject x, GameObject y)
            {
                return x.GetComponent<ElementCube>().row.CompareTo(y.GetComponent<ElementCube>().row);
            });
        return result;
    }


}
