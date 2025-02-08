using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCam : MonoBehaviour
{
    [SerializeField] float lerpRate;
    Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, lerpRate * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }
}