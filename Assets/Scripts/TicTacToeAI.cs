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

    // arbitrarily chose a non zero starting value in order for logic  
    // to work correctly in set visual script
    private int _count = 3;

    private int x, y;
    private int randomChanceCounter = 0;

    [SerializeField]
    private int[] numbers;

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
        //if (_count != 0)
        //{
        //    _isPlayerTurn = !_isPlayerTurn;
        //}
        SetVisual(coordX, coordY, playerState);
    }

	public void AiSelects(int coordX, int coordY){
        //if (_count != 0)
        //{
        //    _isPlayerTurn = !_isPlayerTurn;
        //}
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

    private void AIMoveset()
    {
        // to do
        // set random range if ai level is easy
        // and set to 1 if ai level is hard
        if (_isPlayerTurn.Equals(false))
        {

            // garbage
            #region
            /*
            else if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 0;
                    y = 1;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x + 1, y].canClick.Equals(true))
                    {
                        _triggers[x + 1, y].canClick = false;
                        AiSelects(x + 1, y);
                        print("blocked");
                    }
                    else if (_triggers[x + 2, y].canClick.Equals(true))
                    {
                        _triggers[x + 2, y].canClick = false;
                        AiSelects(x + 2, y);
                        print("blocked");
                    }

                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/

            /*
            else if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(2))
            {

                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 0;
                    y = 2;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x + 1, y].canClick.Equals(true))
                    {
                        _triggers[x + 1, y].canClick = false;
                        AiSelects(x + 1, y);
                        print("blocked");
                    }
                    else if (_triggers[x + 2, y].canClick.Equals(true))
                    {
                        _triggers[x + 2, y].canClick = false;
                        AiSelects(x + 2, y);
                        print("blocked");
                    }

                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/

            /*
            else if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 0;
                    y = 0;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 1].canClick.Equals(true))
                    {
                        _triggers[x, y + 1].canClick = false;
                        AiSelects(x, y + 1);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 2].canClick.Equals(true))
                    {
                        _triggers[x, y + 2].canClick = false;
                        AiSelects(x, y + 2);
                        print("blocked");
                    }
                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/

            /*
            else if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 1;
                    y = 0;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 1].canClick.Equals(true))
                    {
                        _triggers[x, y + 1].canClick = false;
                        AiSelects(x, y + 1);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 2].canClick.Equals(true))
                    {
                        _triggers[x, y + 2].canClick = false;
                        AiSelects(x, y + 2);
                        print("blocked");
                    }
                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/

            /*
            else if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 2;
                    y = 0;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 1].canClick.Equals(true))
                    {
                        _triggers[x, y + 1].canClick = false;
                        AiSelects(x, y + 1);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 2].canClick.Equals(true))
                    {
                        _triggers[x, y + 2].canClick = false;
                        AiSelects(x, y + 2);
                        print("blocked");
                    }
                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/


            /*
            else if (DataManager.Instance.diagonalQuadrant1Counter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 0;
                    y = 0;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x + 1, y + 1].canClick.Equals(true))
                    {
                        _triggers[x + 1, y + 1].canClick = false;
                        AiSelects(x + 1, y + 1);
                        print("blocked");
                    }
                    else if (_triggers[x, y + 2].canClick.Equals(true))
                    {
                        _triggers[x + 2, y + 2].canClick = false;
                        AiSelects(x + 2, y + 2);
                        print("blocked");
                    }
                    
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
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }*/

            /*
            else if (DataManager.Instance.diagonalQuadrant2Counter.Equals(2))
            {
                randomChanceCounter = 1;
                if (randomChanceCounter.Equals(1))
                {
                    x = 0;
                    y = 2;

                    if (_triggers[x, y].canClick.Equals(true))
                    {
                        _triggers[x, y].canClick = false;
                        AiSelects(x, y);
                        print("blocked");
                    }
                    else if (_triggers[x, y - 1].canClick.Equals(true))
                    {
                        _triggers[x, y - 1].canClick = false;
                        AiSelects(x, y - 1);
                        print("blocked");
                    }
                    else if (_triggers[x, y - 2].canClick.Equals(true))
                    {
                        _triggers[x, y - 2].canClick = false;
                        AiSelects(x, y - 2);
                        print("blocked");
                    }
                    
                    for (int i = 0; i < 3; i++)
                    {
                        if (_triggers[x + i, y - i].canClick.Equals(true))
                        {
                            _triggers[x + i, y - i].canClick = false;
                            AiSelects(x + i, y - i);
                            print("blocked");
                            break;
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < _triggers.Length; i++)
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
            }
            else
            {

                for (int i = 0; i < _triggers.Length; i++)
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
            }*/
            #endregion
        }
        randomChanceCounter = 1;
        print(randomChanceCounter);

        

    }

    private void PlayerWinConditions()
    {
        if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.middleVerticalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.rightVerticalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.topHorizontalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.middleHorizontalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.bottomHorizontalQuadrantCounter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.diagonalQuadrant1Counter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.diagonalQuadrant2Counter.Equals(3))
        {
            _count = 0;
            onPlayerWin.Invoke(_count);
        }
        // tie condiitions
        else if (DataManager.Instance.tieConditionCounter.Equals(9) && _count != 0 && _count != 1)
        {
            onPlayerWin.Invoke(-1);
        }
    }

    private void AIWinConditions()
    {
        if (DataManager.Instance.aiLeftVerticalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiMiddleVerticalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiRightVerticalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiTopHorizontalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiMiddleHorizontalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiBottomHorizontalQuadrantCounter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else  if (DataManager.Instance.aiDiagonalQuadrant1Counter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.aiDiagonalQuadrant2Counter.Equals(3))
        {
            _count = 1;
            onPlayerWin.Invoke(_count);
        }
        else if (DataManager.Instance.tieConditionCounter.Equals(9) && _count != 0 && _count != 1)
        {
            onPlayerWin.Invoke(-1);
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
        // will give ai 50/50 chance of blocking player from winning
        if (_isPlayerTurn.Equals(false))
        {
            //AIMoveset();
        }

        // for player win conditions
        PlayerWinConditions();

        // for ai win conditions
        AIWinConditions();

        //updating every frame so that each counter would be accurately tracked
        #region
        numbers[0] = DataManager.Instance.leftVerticalQuadrantCounter;
        numbers[1] = DataManager.Instance.middleVerticalQuadrantCounter;
        numbers[2] = DataManager.Instance.rightVerticalQuadrantCounter;
        numbers[3] = DataManager.Instance.topHorizontalQuadrantCounter;
        numbers[4] = DataManager.Instance.middleHorizontalQuadrantCounter;
        numbers[5] = DataManager.Instance.bottomHorizontalQuadrantCounter;
        numbers[6] = DataManager.Instance.diagonalQuadrant1Counter;
        numbers[7] = DataManager.Instance.diagonalQuadrant2Counter;
        #endregion

        #region
        //if (DataManager.Instance.leftVerticalQuadrantCounter.Equals(2))
        //{
        //    x = 0;
        //    y = 0;
        //    DataManager.Instance.leftVerticalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {

        //        if (_triggers[x + i, y].canClick.Equals(true))
        //        {
        //            _triggers[x + i, y].canClick = false;
        //            AiSelects(x + i, y);
        //            print("blocked");
        //            print(x + i);
        //            print(y);
        //            break;
        //        }
        //    }
        //}
        //else if (numbers[1].Equals(2))
        //{
        //    x = 0;
        //    y = 1;
        //    DataManager.Instance.middleVerticalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x + i, y].canClick.Equals(true))
        //        {
        //            _triggers[x + i, y].canClick = false;
        //            AiSelects(x + i, y);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}


        //else if (numbers[2].Equals(2))
        //{
        //    x = 0;
        //    y = 1;
        //    DataManager.Instance.rightVerticalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x + i, y].canClick.Equals(true))
        //        {
        //            _triggers[x + i, y].canClick = false;
        //            AiSelects(x + i, y);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}

        //else if (numbers[3].Equals(2))
        //{
        //    x = 0;
        //    y = 0;
        //    DataManager.Instance.topHorizontalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x, y + i].canClick.Equals(true))
        //        {
        //            _triggers[x, y + i].canClick = false;
        //            AiSelects(x, y + i);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}

        //else if (numbers[4].Equals(2))
        //{
        //    x = 1;
        //    y = 0;
        //    DataManager.Instance.middleHorizontalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x, y + i].canClick.Equals(true))
        //        {
        //            _triggers[x, y + i].canClick = false;
        //            AiSelects(x, y + i);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}

        //else if (numbers[5].Equals(2))
        //{
        //    x = 2;
        //    y = 0;
        //    DataManager.Instance.bottomHorizontalQuadrantCounter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x, y + i].canClick.Equals(true))
        //        {
        //            _triggers[x, y + i].canClick = false;
        //            AiSelects(x, y + i);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}

        //else if (numbers[6].Equals(2))
        //{
        //    x = 0;
        //    y = 0;
        //    DataManager.Instance.diagonalQuadrant1Counter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x + i, y + i].canClick.Equals(true))
        //        {
        //            _triggers[x + i, y + i].canClick = false;
        //            AiSelects(x + i, y + i);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}

        //else if (numbers[7].Equals(2))
        //{
        //    x = 0;
        //    y = 2;
        //    DataManager.Instance.diagonalQuadrant2Counter = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_triggers[x + i, y - i].canClick.Equals(true))
        //        {
        //            _triggers[x + i, y - i].canClick = false;
        //            AiSelects(x + i, y - i);
        //            print("blocked");
        //            break;
        //        }
        //    }
        //}
        #endregion

    }
}
