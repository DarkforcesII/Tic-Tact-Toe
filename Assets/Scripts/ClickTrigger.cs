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
		// _references player turn bool
		// otherwise player can select more than one cell in a single turn
		if (_ai._isPlayerTurn.Equals(true)) {
			if (canClick == true)
			{
				_ai.PlayerSelects(_myCoordX, _myCoordY);
				canClick = false;
			}
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
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant2Counter++;
			DataManager.Instance.tieConditionCounter++;

		}
		#endregion

		// middle vertical quadrant for player
		#region
		if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.topHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant1Counter++;
			DataManager.Instance.diagonalQuadrant2Counter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(1))
		{
			DataManager.Instance.middleVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		#endregion

		// right vertical quadrant for player
		#region
		if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant2Counter++;
			DataManager.Instance.topHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.middleHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(2))
		{
			DataManager.Instance.rightVerticalQuadrantCounter++;
			DataManager.Instance.bottomHorizontalQuadrantCounter++;
			DataManager.Instance.diagonalQuadrant1Counter++;
			DataManager.Instance.tieConditionCounter++;
		}
		#endregion

		// left vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(0))
		{
			DataManager.Instance.aiLeftVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
			DataManager.Instance.tieConditionCounter++;

		}
		#endregion

		// middle vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(1))
		{
			DataManager.Instance.aiMiddleVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		#endregion

		// right vertical quadrant for ai
		#region
		if (other.tag.Equals("AI") && _myCoordX.Equals(0) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant2Counter++;
			DataManager.Instance.aiTopHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(1) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiMiddleHorizontalQuadrantCounter++;
			DataManager.Instance.tieConditionCounter++;
		}
		if (other.tag.Equals("AI") && _myCoordX.Equals(2) && _myCoordY.Equals(2))
		{
			DataManager.Instance.aiRightVerticalQuadrantCounter++;
			DataManager.Instance.aiBottomHorizontalQuadrantCounter++;
			DataManager.Instance.aiDiagonalQuadrant1Counter++;
			DataManager.Instance.tieConditionCounter++;
		}
		#endregion
	}
}
