using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float NormalMoveSpeed = 10;
    public float SlowMoveFactor = 0.25f;
    public float FastMoveFactor = 3;

    private Transform playerTransform;
    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.position += transform.up * (NormalMoveSpeed * FastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (NormalMoveSpeed * FastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.position += transform.up * (NormalMoveSpeed * SlowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (NormalMoveSpeed * SlowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * NormalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * NormalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
    }

}
