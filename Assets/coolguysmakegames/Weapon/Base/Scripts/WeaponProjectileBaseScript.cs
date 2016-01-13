using UnityEngine;
using System.Collections;

// The base player weapon projectile script
// Functionality for colliding
// Matthew Cormack
// 13/01/16 - 06:29

public class WeaponProjectileBaseScript : MonoBehaviour
{
	void Update()
	{
		// Cull bullets outside of the world
		if ( ( transform.position.y > 10 ) || ( transform.position.y < -10 ) )
		{
			OnCollide( null );
		}
	}

	void OnCollisionEnter( Collision collision )
	{
		OnCollide( collision );
	}

	virtual protected void OnCollide( Collision collision )
	{
		// Kill enemy
		if ( ( collision != null ) && ( collision.transform.gameObject.layer == LayerMask.NameToLayer( "Enemy" ) ) )
		{
			EnemyUnitBaseScript enemyunit = collision.transform.GetComponent<EnemyUnitBaseScript>();
			if ( enemyunit )
			{
				enemyunit.Die_Killed();
            }

			Destroy( collision.transform.gameObject );
		}

		// Kill bullet
		Destroy( transform.parent.gameObject );
	}
}
