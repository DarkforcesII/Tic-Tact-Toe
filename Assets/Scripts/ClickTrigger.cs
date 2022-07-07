using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
	TicTacToeAI _ai;

	[SerializeField]
	private int _myCoordX = 0;
	[SerializeField]
	private int _myCoordY = 0;

	[SerializeField]
	public bool canClick = false;

	private void Awake()
	{
		_ai = FindObjectOfType<TicTacToeAI>();
	}

	private void Start(){

		_ai.onGameStarted.AddListener(AddReference);
		_ai.onGameStarted.AddListener(() => SetInputEndabled(true));
		_ai.onPlayerWin.AddListener((win) => SetInputEndabled(false));
	}

	private void SetInputEndabled(bool val){
		canClick = val;
	}

	private void AddReference()
	{
		_ai.RegisterTransform(_myCoordX, _myCoordY, this);
	}

	private void OnMouseDown()
	{
		if(canClick == true){
			_ai.PlayerSelects(_myCoordX, _myCoordY);
			canClick = false;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		// left vertical quadrant for player
        #region
        if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(0))
        {
			DataManager.Instance.leftVerticalQuadrantCounter++;
			DataManager.Instance.topHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant1Counter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant2Counter++;

		}
		#endregion

		// middle vertical quadrant for player
		#region
		if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.topHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant1Counter++;
			DataManager.Instance.diagonalQuadrant2Counter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
		}
		#endregion

		// right vertical quadrant for player
		#region
		if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant2Counter++;
			DataManager.Instance.topHorizontalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant1Counter++;
		}
		#endregion

		// top horizontal quadrant for player
		#region
		if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			//DataManager.Instance.topHorizontalQuadrantCounter++;
			//DataManager.Instance.middleHorizontalQuadrantCounter++;
		}
		#endregion

		// left vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
			print(DataManager.Instance.aiLeftVerticalQuadrantCounter);
			print(DataManager.Instance.aiBottomHorizontalQuadrantCounter);
			print(DataManager.Instance.aiDiagonalQuadrant2Counter);

		}
		#endregion

		// middle vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
		}
		#endregion

		// right vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
		}
		#endregion

		// top horizontal quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			//DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			//DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
			//print(DataManager.Instance.aiTopHorizontalQuadrantCounter);
			//print(DataManager.Instance.aiMiddleHorizontalQuadrantCounter);
		}
		#endregion
	}
}
