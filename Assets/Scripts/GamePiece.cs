using System.Collections;
using UnityEngine;

public class GamePiece : MonoBehaviour{

	[SerializeField]
	private float _animTime = 2f;
	[SerializeField]
	AnimationCurve _growthCurve;

	public int playerWinCounter = 0;
	public int aiWinCounter = 0;

	private void OnEnable()
	{
		StartCoroutine(SpawnRoutine());
	}

	IEnumerator SpawnRoutine(){
		yield return null;
		for(float t = 0 ; t <= _animTime; t += Time.deltaTime){
			yield return new WaitForFixedUpdate();
			transform.localScale = Vector3.one * _growthCurve.Evaluate( t/_animTime);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
			playerWinCounter++;
			print(playerWinCounter++);
			if (playerWinCounter.Equals(3))
            {
				print("player wins");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Player")
		{
			playerWinCounter++;
			print(playerWinCounter++);
			if (playerWinCounter.Equals(3))
			{
				print("player wins");
			}
		}
	}


}
