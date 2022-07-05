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
		// left quadrant from top to bottom
        #region
        if (other.tag.Equals("Player") && _myCoordX.Equals(0) && _myCoordY.Equals(0))
        {
			DataManager.Instance.leftQuadrantCounter++;
			print(DataManager.Instance.leftQuadrantCounter);
        }
		if (other.tag.Equals("Player") && _myCoordX.Equals(1) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftQuadrantCounter++;
			print(DataManager.Instance.leftQuadrantCounter);
		}
		if (other.tag.Equals("Player") && _myCoordX.Equals(2) && _myCoordY.Equals(0))
		{
			DataManager.Instance.leftQuadrantCounter++;
			print(DataManager.Instance.leftQuadrantCounter);
		}
        #endregion
    }
}
