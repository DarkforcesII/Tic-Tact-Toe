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
    public bool _isPlayerTurn = false;

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

    public int turnCounter = 0;
    int x, y;

    private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}
    }

    public void StartAI(int AILevel)
    {
        _aiLevel = AILevel;
        StartGame();

        if (_isPlayerTurn.Equals(false))
        {
            // makes sure that ai will take a corner as soon as game starts
            _triggers[0, 0].canClick = false;
            AiSelects(0, 0);
            turnCounter++;
        }
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
        StartCoroutine(AIMoveset());
    }

	public void AiSelects(int coordX, int coordY){
        SetVisual(coordX, coordY, aiState);
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
        _isPlayerTurn = !_isPlayerTurn;
        Instantiate(
			targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
			_triggers[coordX, coordY].transform.position,
			Quaternion.identity
		);
    }

    private void MakeRandomMove()
    {
        
        for (int i = 0; i < 10; i++)
        {
            x = Random.Range(0, 3);
            y = Random.Range(0, 3);
            if (_triggers[x, y].canClick.Equals(true))
            {
                _triggers[x, y].canClick = false;
                AiSelects(x, y);
                break;
            }
        }
    }

    private void FinalMove()
    {
        for (int k = 0; k < 10; k++)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_triggers[i, j].canClick.Equals(true))
                    {
                        print(i);
                        print(j);
                        _triggers[i, j].canClick = false;
                        AiSelects(i, j);
                        break;
                    }

                }
            }
        }
    }

    private IEnumerator AIMoveset()
    {
        yield return new WaitForSeconds(1);
        switch (turnCounter)
        {
            case 1:
                if (_triggers[1, 1].canClick.Equals(false))
                {
                    _triggers[2, 2].canClick = false;
                    AiSelects(2, 2);
                    turnCounter++;
                }
                else if (_triggers[0, 1].canClick.Equals(false))
                {
                    _triggers[1, 1].canClick = false;
                    AiSelects(1, 1);
                    turnCounter++;
                }
                else if (_triggers[2, 0].canClick.Equals(true))
                {
                    _triggers[2, 0].canClick = false;
                    AiSelects(2, 0);
                    turnCounter++;
                }
                else  
                {
                    _triggers[0, 2].canClick = false;
                    AiSelects(0, 2);
                    turnCounter++;
                }
                break;
            case 2:
                if (_triggers[0, 1].canClick.Equals(true) && _triggers[1, 1].canClick.Equals(false))
                {
                    _triggers[0, 1].canClick = false;
                    AiSelects(0, 1);
                    turnCounter++;
                }
                else if (_triggers[2, 0].canClick.Equals(false) && _triggers[0, 0].canClick.Equals(false)
                    && _triggers[2, 2].canClick.Equals(false) && _triggers[0, 2].canClick.Equals(false))
                {
                    ScrewPlayer();
                }
                else if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(2))
                {
                    BlockPlayer();
                }
                else if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(2)
                    && _triggers[0,1].canClick.Equals(true))
                {

                    BlockPlayer();
                }
                else if (_triggers[0, 2].canClick.Equals(true))
                {
                    _triggers[0, 2].canClick = false;
                    AiSelects(0, 2);
                    turnCounter++;
                }
                else if (_triggers[2, 2].canClick.Equals(true))
                {
                    _triggers[2, 2].canClick = false;
                    AiSelects(2, 2);
                    turnCounter++;
                }
                else 
                {
                    print("blocked");
                    MakeRandomMove();
                }
                break;

            case 3:
                if (_triggers[2, 0].canClick.Equals(false) && _triggers[0, 0].canClick.Equals(false)
                    && _triggers[2, 2].canClick.Equals(false) && _triggers[0, 2].canClick.Equals(false))
                {
                    ScrewPlayer();
                }
                else if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(2)
                    && DataManager.Instance.diagonalQuadrant1Counter.Equals(2))
                {
                    BlockPlayer();
                }
                else
                {
                    MakeRandomMove();    
                }
                break;
            case 4:
                FinalMove();
                break;
        }
    }

    private void ScrewPlayer()
    {
        if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(2)
                            && _isPlayerTurn.Equals(false))
        {
            x = 0;
            y = 0;
            turnCounter++;
            print("here");
            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    break;
                }
            }
        }
        else if (DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 1;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    break;
                }
            }
        }


        else if (DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 2;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    print("blocked");
                    break;
                }
            }
        }

        else if (DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 1;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 2;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y + i].canClick.Equals(true))
                {
                    _triggers[x + i, y + i].canClick = false;
                    AiSelects(x + i, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 2;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y - i].canClick.Equals(true))
                {
                    _triggers[x + i, y - i].canClick = false;
                    AiSelects(x + i, y - i);
                    break;
                }
            }
        }
        else
        {
            MakeRandomMove();
        }
    }
    private void BlockPlayer()
    {
        if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(2)
                            && _isPlayerTurn.Equals(false))
        {

            for (int i = 0; i < 3; i++)
            {
                x = 0;
                y = 0;
                turnCounter++;
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    break;
                }
            }
        }
        else if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 1;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    break;
                }
            }
        }


        else if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 2;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y].canClick.Equals(true))
                {
                    _triggers[x + i, y].canClick = false;
                    AiSelects(x + i, y);
                    break;
                }
            }
        }

        else if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 1;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    break;
                }
            }
        }

        else if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 2;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x, y + i].canClick.Equals(true))
                {
                    _triggers[x, y + i].canClick = false;
                    AiSelects(x, y + i);
                    print("blocked");
                    break;
                }
            }
        }

        else if (DataManager.Instance.diagonalQuadrant1Counter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y + i].canClick.Equals(true))
                {
                    _triggers[x + i, y + i].canClick = false;
                    AiSelects(x + i, y + i);
                    print("blocked");
                    break;
                }
            }
        }

        else if (DataManager.Instance.diagonalQuadrant2Counter.Equals(2)
            && _isPlayerTurn.Equals(false))
        {
            turnCounter++;
            x = 0;
            y = 2;

            for (int i = 0; i < 3; i++)
            {
                if (_triggers[x + i, y - i].canClick.Equals(true))
                {
                    _triggers[x + i, y - i].canClick = false;
                    AiSelects(x + i, y - i);
                    break;
                }
            }
        }
        else
        {
            FinalMove();
        }
    }

    private void PlayerWinConditions()
    {
        if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.diagonalQuadrant1Counter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        else if (DataManager.Instance.diagonalQuadrant2Counter.Equals(3))
        {
            onPlayerWin.Invoke(0);
        }
        // tie conditions
        else if (DataManager.Instance.tieConditionCounter.Equals(9))
        {
            onPlayerWin.Invoke(-1);
        }
    }

    private void AIWinConditions()
    {
        if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else  if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(3))
        {
            onPlayerWin.Invoke(1);
        }
        else if (DataManager.Instance.tieConditionCounter.Equals(9))
        {
            onPlayerWin.Invoke(-1);
        }
    }

    private void Update()
    {
        // for player win conditions
        PlayerWinConditions();

        // for ai win conditions
        AIWinConditions();

        // allows game to continue if player and ai run into edge case
        #region
        if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.leftVerticalQuadrantCounter = 0;
            //FinalMove();
        }
        if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.middleVerticalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.rightVerticalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.topHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.middleHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.bottomHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.diagonalQuadrant1Counter.Equals(2)
            && DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(1))
        {
            DataManager.Instance.diagonalQuadrant1Counter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.diagonalQuadrant2Counter.Equals(2)
            && DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(1))
        {
            DataManager.Instance.diagonalQuadrant2Counter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        // ai will randomly select a space after 7 moves to ensure a tie
        if (DataManager.Instance.tieConditionCounter.Equals(7))
        {
            //turnCounter = 4;
        }

        #endregion


        // allows game to continue if player and ai run into edge case
        #region
        if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.leftVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiLeftVerticalQuadrantCounter = 0;
            //FinalMove();
        }
        if (DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.middleVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiMiddleVerticalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(2)
            && DataManager.Instance.rightVerticalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiRightVerticalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.topHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiTopHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.middleHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiMiddleHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(2)
            && DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(1))
        {
            DataManager.Instance.aiBottomHorizontalQuadrantCounter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(2)
            && DataManager.Instance.diagonalQuadrant1Counter.Equals(1))
        {
            DataManager.Instance.aiDiagonalQuadrant1Counter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        if (DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(2)
            && DataManager.Instance.diagonalQuadrant2Counter.Equals(1))
        {
            DataManager.Instance.aiDiagonalQuadrant2Counter = 0;
            //MakeRandomMove(Random.Range(0, 3), Random.Range(0, 3));
        }
        #endregion
    }
}
