using System;
using System.Collections;
using Managers;
using UnityEngine;

public class ObtacleDetector : MonoBehaviour
{
    public static event Action LevelComplete = delegate { };
    private Animator _playerAnim;
    private GameObject _stack;
    private int _finishMultiplier;
    private bool _levelComplete;
    private static readonly int Death = Animator.StringToHash("death");

    private void Awake ()
    {
        _stack = GameObject.FindGameObjectWithTag ("Stack");
        _playerAnim = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
        _levelComplete = false;
    }
    private void Update ()
    {
        CheckForObstacles ();
        StartCoroutine (CheckForRestart ());
    }
    private void CheckForObstacles ()
    {
        Vector3 fwd = transform.TransformDirection (Vector3.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast (transform.position, fwd, out hitInfo, 0.6f))
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
                _finishMultiplier = 1;
                HandleFinish ();
                break;
            case "2x":
                _finishMultiplier = 2;
                HandleFinish ();
                break;
            case "3x":
                _finishMultiplier = 3;
                HandleFinish ();
                break;
            case "4x":
                _finishMultiplier = 4;
                HandleFinish ();
                break;
            case "5x":
                _finishMultiplier = 5;
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
        if (_stack.transform.childCount <= 1)
        {
            Movement.instance.StopMovement ();
            UiManager.instance.HandleGemCollection (0, _finishMultiplier);
            LevelComplete ();
            _levelComplete = true;
        }
    }
    private IEnumerator CheckForRestart ()
    {
        if (_levelComplete) yield break;
        yield return new WaitForSeconds (1f);

        if (_stack.transform.childCount <= 0)
        {
            _playerAnim.SetTrigger (Death);
            Movement.instance.StopMovement ();
            UiManager.instance.ActivateRestartButton ();
        }

    }
}