using UnityEngine;
using System.Collections.Generic;

public static class Globals {
    public enum ElementType{Light,Fire, Water, Wind, Earth, Dark};

    public static List<Rigidbody> cubeList = new List<Rigidbody>(81);


    public static Rigidbody FindCube(Vector3 position) {
        for (int i = 0; i < cubeList.Count; i++) {
            if (ComparePosition(cubeList[i].transform.position, position)) {
                return cubeList[i];
            }
        }
        return null;
    }

    public static Rigidbody FindCube(int row,int col) {
        for (int i = 0; i < cubeList.Count; i++) {
            GameObject cur = cubeList[i].gameObject;

            if (cur.GetComponent<ElementCube>().col==col && cur.GetComponent<ElementCube>().row==row) {

                return cubeList[i];
            }
        }
        return null;
    }

    public static void Swap(Rigidbody cube1, Rigidbody cube2) {
        Vector3 tmp = cube1.transform.position;
        cube1.transform.position = cube2.transform.position;
        cube2.transform.position = tmp;
        int tmprow = cube1.GetComponent<ElementCube>().row;
        int tmpcol = cube1.GetComponent<ElementCube>().col; 
        cube1.GetComponent<ElementCube>().row = cube2.GetComponent<ElementCube>().row;
        cube1.GetComponent<ElementCube>().col = cube2.GetComponent<ElementCube>().col;
        cube2.GetComponent<ElementCube>().row = tmprow;
        cube2.GetComponent<ElementCube>().col = tmpcol;
    }

    public static Vector3 TranformPlayerToCube(Vector3 pos){
        Vector3 result = pos;
        result += new Vector3(0.5f, -1.0f, 0.5f);
        return result;
    }

    public static bool ComparePosition(Vector3 a, Vector3 b){
        if (Vector3.Distance(a, b) <= 0.05)
        {
            return true;
        }
        return false;
    }

    public static HashSet<Rigidbody> CheckThree(Rigidbody cube1){
        HashSet<Rigidbody> result = new HashSet<Rigidbody>();
        List<Rigidbody> row = getRow(cube1);
        List<Rigidbody> col = getCol(cube1);

        int countRow = 0;
        int countCol = 0;
        for (int i = 1; i < 9; i++)
        {
            if (row[i].gameObject.GetComponent<ElementCube>().type == row[i-1].gameObject.GetComponent<ElementCube>().type)
            {
                countRow += 1;
            }
            else
            {
                if (countRow >= 2)
                {
                    for (int j = 0; j <= countRow; j++)
                    {
                        result.Add(row[i - 1 - j]);
                    }
                }
                countRow = 0;
            }

            if ((col[i].gameObject.GetComponent("ElementCube") as ElementCube).type == (col[i-1].gameObject.GetComponent("ElementCube") as ElementCube).type)
            {
                countCol += 1;
            }
            else
            {
                if (countCol >= 2)
                {
                    for (int j = 0; j <= countCol; j++)
                    {
                        result.Add(col[i - 1 - j]);
                    }
                }
                countCol = 0;
            }


        }

        if (countCol >= 2)
        {
            for (int j = 0; j <= countCol; j++)
            {
                result.Add(col[8 - j]);
            }
        }
        if (countRow >= 2)
        {
            for (int j = 0; j <= countRow; j++)
            {
                result.Add(row[8 - j]);
            }
        }

        return result;
    }




    private static List<Rigidbody> getRow(Rigidbody cube){
        List<Rigidbody> result = new List<Rigidbody>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].gameObject.GetComponent<ElementCube>().row==cube.gameObject.GetComponent<ElementCube>().row) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(Rigidbody x, Rigidbody y)
            {
                return x.gameObject.GetComponent<ElementCube>().col.CompareTo(y.gameObject.GetComponent<ElementCube>().col);
            });
        return result;
    }

    private static List<Rigidbody> getCol(Rigidbody cube){
        List<Rigidbody> result = new List<Rigidbody>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].gameObject.GetComponent<ElementCube>().col==cube.gameObject.GetComponent<ElementCube>().col) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(Rigidbody x, Rigidbody y)
            {
                return x.gameObject.GetComponent<ElementCube>().row.CompareTo(y.gameObject.GetComponent<ElementCube>().row);
            });
        return result;
    }
}
