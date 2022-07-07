using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]
public class WinnerEvent : UnityEvent<int>
{
}

public class TicTacToeAI : MonoBehaviour
{

	int _aiLevel;

	TicTacToeState[,] boardState;

	[SerializeField]
	private bool _isPlayerTurn;

	[SerializeField]
	private int _gridSize = 3;
	
	[SerializeField]
	private TicTacToeState playerState = TicTacToeState.circle;
	TicTacToeState aiState = TicTacToeState.cross;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;

	public UnityEvent onGameStarted;

	public EndMessage message;

	//Call This event with the player number to denote the winner
	public WinnerEvent onPlayerWin;

	ClickTrigger[,] _triggers;
	
	private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}

	}

	public void StartAI(int AILevel){
		_aiLevel = AILevel;
		StartGame();
	}

	public void RegisterTransform(int myCoordX, int myCoordY, ClickTrigger clickTrigger)
	{
		_triggers[myCoordX, myCoordY] = clickTrigger;
	}

	private void StartGame()
	{
		_triggers = new ClickTrigger[3,3];
		onGameStarted.Invoke();
	}

	public void PlayerSelects(int coordX, int coordY){

		SetVisual(coordX, coordY, playerState);
	}

	public void AiSelects(int coordX, int coordY){
		SetVisual(coordX, coordY, aiState);
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		Instantiate(
			targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
			_triggers[coordX, coordY].transform.position,
			Quaternion.identity
		);
		_isPlayerTurn = !_isPlayerTurn;
	}

    private void PlayerWinConditions()
    {
        if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.diagonalQuadrant1Counter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        if (DataManager.Instance.diagonalQuadrant2Counter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
    }

    private void AIWinConditions()
    {
        if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        if (DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
    }

    private void TieConditions()
    {
        if (DataManager.Instance.tieConditionCounter.Equals(9))
        {
            onPlayerWin.Invoke(-1);
        }
    }

    private void Update()
    {
        if (_isPlayerTurn.Equals(false))
        {

            for (int i = 0; i < _triggers.Length; i++)
            {
                int x = Random.Range(0, 3);
                int y = Random.Range(0, 3);
                if (_triggers[x, y].canClick.Equals(true))
                {
                    _triggers[x, y].canClick = false;
                    AiSelects(x, y);
                    break;
                }
            }
        }

        // for player win conditions
        PlayerWinConditions();

        // for ai win conditions
        AIWinConditions();
        
        // tie condition
        TieConditions();
    }
}
