using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// The main over-arching game logic
// 
// Matthew Cormack
// 13/01/16 - 12:12

public class GameLogicScript : MonoBehaviour
{
	public GameObject UI;
	public Text Text_Lose;
	public Text Text_Win;

	public EnemySpawnerScript[] Spawners;
	public PlayerGateTriggerScript PlayerGate;
	public GameObject GameWinPrefab;

	private bool FinishedRoundSpawning = false;
	private bool RoundOutcomeLose = false;

	void Update()
	{
		// Once all enemies have spawned, check for them dying
		if ( FinishedRoundSpawning && ( !RoundOutcomeLose ) )
		{
			if ( !GameObject.FindWithTag( "Enemy" ) )
			{
				CheckForGameEnd( false );
            }
		}
	}

	public void CheckForGameEnd( bool lose )
	{
		// If the UI is already active then this has already been run, don't run multiple times
		if ( UI.activeSelf ) return;

		// Turn on menu ui for both outcomes
		UI.SetActive( true );
		// Turn on the menu with special 'you lost/won' text
		Text_Lose.enabled = lose;
		Text_Win.enabled = !lose;

		// Store for stopping a quick win if the last enemy alive takes the last health point of the gate
		RoundOutcomeLose = lose;

		if ( !lose )
		{
			GameObject effect = (GameObject) Instantiate( GameWinPrefab );
			effect.transform.SetParent( GameObject.Find( "GameObjectContainer" ).transform );
		}
    }

	public void Button_PlayAgain()
	{
		// Reenable spawners
		if ( Spawners.Length > 0 )
		{
			for ( int spawner = 0; spawner < Spawners.Length; spawner++ )
			{
				Spawners[spawner].Reset();
            }
        }

		// Cleanup old round specific gameobjects
		foreach( Transform oldtransform in GameObject.Find( "GameObjectContainer" ).GetComponentsInChildren<Transform>() )
		{
			if ( oldtransform.gameObject.name != "GameObjectContainer" )
			{
				Destroy( oldtransform.gameObject );
			}
        }

		// Hide UI now
		UI.SetActive( false );

		// Reset round end flag
		FinishedRoundSpawning = false;
		RoundOutcomeLose = false;

		// Reset the health of the gate
		PlayerGate.Health = PlayerGate.MaxHealth;
		PlayerGate.TakeHealth( 0 ); // (to update the ui text)
    }

	public void SetFinishedRoundSpawning( bool finish )
	{
		FinishedRoundSpawning = finish;
    }
}
