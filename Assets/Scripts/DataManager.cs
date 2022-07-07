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

    // tracks vertical quadrants for player
    #region
    public int leftVerticalQuadrantCounter = 0;
    public int middleVerticalQuadrantCounter = 0;
    public int rightVerticalQuadrantCounter = 0;
    #endregion

    // tracks horizontal quadrants for player
    #region
    public int topHorizontalQuadrantCounter = 0;
    public int middleHorizontalQuadrantCounter = 0;
    public int bottomHorizontalQuadrantCounter = 0;
    #endregion

    // tracks diagonal quadrants for player
    #region
    public int diagonalQuadrant1Counter = 0;
    public int diagonalQuadrant2Counter = 0;
    #endregion

    // tracks vertical quadrants for ai
    #region
    public int aiLeftVerticalQuadrantCounter = 0;
    public int aiMiddleVerticalQuadrantCounter = 0;
    public int aiRightVerticalQuadrantCounter = 0;
    #endregion

    // tracks horizontal quadrants for ai
    #region
    public int aiTopHorizontalQuadrantCounter = 0;
    public int aiMiddleHorizontalQuadrantCounter = 0;
    public int aiBottomHorizontalQuadrantCounter = 0;
    #endregion

    // tracks diagonal quadrants for ai
    #region
    public int aiDiagonalQuadrant1Counter = 0;
    public int aiDiagonalQuadrant2Counter = 0;
    #endregion

    // tracks all spaces to determine a tie
    #region
    public int tieConditionCounter = 0;
    #endregion
}
