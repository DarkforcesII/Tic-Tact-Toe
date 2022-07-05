using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            GameObject managerObj = new GameObject("DataManager");
            instance = managerObj.AddComponent<DataManager>();
            return instance;
        }
    }

    public void PrintToScreen()
    {
        Debug.Log("Data Manager");
    }

    public int leftQuadrantCounter;
}
