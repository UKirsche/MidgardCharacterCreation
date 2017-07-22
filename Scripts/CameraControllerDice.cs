using UnityEngine;
using System.Collections;

public class CameraControllerDice : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    //Nachdem alles erledigt ist
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
