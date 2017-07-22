using UnityEngine;
using System.Collections;

/// <summary>
/// Würfel bekommt eine zufällige Rotation und einen Kraftimpuls in eine zufällige Richtung nach unten
/// </summary>
public class RandomDiceThrower : MonoBehaviour {

	void Start () {
        Rigidbody RB;
        float rotationspeed = 160;
        float forceStrength = 800;
        RB = GetComponent<Rigidbody>();

        //Quaternion -> Drehe
        RB.angularVelocity =  Random.insideUnitSphere * rotationspeed;
        Vector3 forceDirectionDown = Random.insideUnitSphere;
        if (forceDirectionDown.y > 0)
        {
            forceDirectionDown.y *= -1;
        }

        forceDirectionDown.x *= forceStrength;
        forceDirectionDown.z *= forceStrength;
        forceDirectionDown.y *= forceStrength * 2;

        //Werfe nach unten irgendwohin
        RB.AddForce(forceDirectionDown);
	
	}

}
