using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<int> gameObjectsList;

    [SerializeField]
    private bool isPlayersTurn;
    int counter = 9;
    int randomIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        isPlayersTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlayersTurn.Equals(true))
            {
                isPlayersTurn = false;
                PlayerTurn();
                Invoke("AITurn", 2);
            }
        }

        else if (isPlayersTurn.Equals(false))
        {
            //isPlayersTurn = true;
            //Invoke("AITurn", 2);
        }
    }

    private void PlayerTurn()
    {
        counter--;
        gameObjectsList.Remove(counter);
        Debug.Log("AI's Turn " + counter);
    }

    private void AITurn()
    {
        counter--;
        isPlayersTurn = true;
        gameObjectsList.Remove(counter);
        Debug.Log("Player's Turn " + counter);
    }
}
