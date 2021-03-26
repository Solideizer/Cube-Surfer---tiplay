using System;
using Managers;
using UnityEngine;

public class ObtacleDetector : MonoBehaviour
{
    public static event Action LevelComplete = delegate { };
    private GameObject stack;
    private int finishMultiplier;

    private void Awake ()
    {
        stack = GameObject.FindGameObjectWithTag ("Stack");
    }
    private void Update ()
    {
        CheckForObstacles ();
    }
    private void CheckForObstacles ()
    {
        Vector3 fwd = transform.TransformDirection (Vector3.forward);
        RaycastHit hitInfo;
        //Debug.DrawRay (transform.position, fwd * 5f, Color.red);
        if (Physics.Raycast (transform.position, fwd, out hitInfo, 0.5f))
        {
            if (hitInfo.transform.CompareTag ("ObstacleCube"))
            {
                Destroy (this.gameObject);
                AudioManager.PlaySound ("obstacle");
            }
            else
            {
                CheckFinishMultipliers (hitInfo);

            }

        }

    }

    private void CheckFinishMultipliers (RaycastHit hitInfo)
    {
        switch (hitInfo.transform.tag)
        {
            case "1x":
                finishMultiplier = 1;
                HandleFinish ();
                break;
            case "2x":
                finishMultiplier = 2;
                HandleFinish ();
                break;
            case "3x":
                finishMultiplier = 3;
                HandleFinish ();
                break;
            case "4x":
                finishMultiplier = 4;
                HandleFinish ();
                break;
            case "5x":
                finishMultiplier = 5;
                HandleFinish ();
                break;
        }
    }

    private void HandleFinish ()
    {
        Destroy (this.gameObject);
        AudioManager.PlaySound ("gem");
        CheckCubeCount ();
    }

    private void CheckCubeCount ()
    {
        if (stack.transform.childCount - 1 <= 0)
        {
            Movement.instance.StopMovement ();
            Debug.Log (finishMultiplier);
            UiManager.instance.HandleGemCollection (0, finishMultiplier);
            LevelComplete ();
        }
    }
}