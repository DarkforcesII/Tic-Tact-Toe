using System;
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

	public List<int> xCoordinates = new List<int>() { 0, 1, 2 };
	public List<int> yCoordinates = new List<int>() { 0, 1, 2 };

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
		//coordX = UnityEngine.Random.Range(0, xCoordinates.Count);
		//coordY = UnityEngine.Random.Range(0, yCoordinates.Count);
		SetVisual(coordX, coordY, aiState);
	}

	public IEnumerator AI(int coordX, int coordY, float time)
    {
		yield return new WaitForSeconds(time);
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

    private void Update()
    {
        if (_isPlayerTurn.Equals(false))
        {
			AiSelects(UnityEngine.Random.Range(0, xCoordinates.Count), UnityEngine.Random.Range(0, yCoordinates.Count));
        }
    }
}
