using UnityEngine;
using System.Collections;

// The base player weapon script
// Functionality for creating and moving a prefab
// Matthew Cormack
// 13/01/16 - 06:20

public class PlayerWeaponBaseScript : MonoBehaviour
{
	public GameObject Projectile;

	// Update is called once per frame
	void Update()
	{
		if ( Input.GetButtonDown( "Fire1" ) )
		{
			// Create projectile and fire it
			GameObject projectile = (GameObject) Instantiate( Projectile, transform.position + transform.forward - ( transform.up / 2 ), transform.rotation );
			projectile.transform.LookAt( projectile.transform.position + transform.forward );
			projectile.GetComponentInChildren<Rigidbody>().velocity = transform.forward * 10;
			projectile.transform.SetParent( GameObject.Find( "GameObjectContainer" ).transform );

			// Play fire sound
			GetComponent<AudioSource>().volume = Random.Range( 0.85f, 1 );
			GetComponent<AudioSource>().pitch = Random.Range( 0.85f, 1.15f );
			GetComponent<AudioSource>().Play();
		}
	}
}
