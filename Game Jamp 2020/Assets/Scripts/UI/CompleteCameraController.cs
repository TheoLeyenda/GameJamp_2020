using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    public Player player;        //Public variable to store a reference to the player game object


    // LateUpdate is called after Update each frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+ 6.75f, transform.position.z);
    }
}
