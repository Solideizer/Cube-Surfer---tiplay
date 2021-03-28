using Managers;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variable Declarations
    public static Movement instance;

#pragma warning disable 0649
    [SerializeField] private bool canMove = false;
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sensitivityMultiplier;
    [SerializeField] private float deltaThreshold;
#pragma warning restore 0649

    private Vector2 _firstTouchPos;
    private Vector2 _currentTouchPos;
    private float _finalTouchX, _finalTouchZ;
    private float _currentMoveSpeed;
    private Rigidbody _rb;
    private Camera _mainCam;
    #endregion
    private void Awake ()
    {
        CreateSingletonInstance ();
    }

    private void CreateSingletonInstance ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy (gameObject);
        }
    }

    void Start ()
    {
        _rb = GetComponentInChildren<Rigidbody> ();
        _mainCam = Camera.main;
        _currentMoveSpeed = movementSpeed;
        ResetInputValues ();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            UiManager.instance.DisableStartText ();
            canMove = true;

        };
        if (canMove)
        {
            HandleMovement ();
        }

    }

    private void FixedUpdate ()
    {
        if (canMove)
        {
            HandleEndlessRun ();
        }
    }

    void ResetInputValues ()
    {
        var velocity = _rb.velocity;
        velocity = new Vector3 (0f, velocity.y, velocity.z);
        _rb.velocity = velocity;
        _firstTouchPos = Vector2.zero;
        _finalTouchX = 0f;
        _currentTouchPos = Vector2.zero;
    }

    void HandleEndlessRun ()
    {
        var velocity = _rb.velocity;
        velocity = new Vector3 (velocity.x, velocity.y, _currentMoveSpeed * Time.fixedDeltaTime);
        _rb.velocity = velocity;
    }

    void HandleMovement ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            _firstTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButton (0))
        {
            _currentTouchPos = Input.mousePosition;
            var touchDelta = (_currentTouchPos - _firstTouchPos);

            if (_firstTouchPos == _currentTouchPos)
            {
                var velocity = _rb.velocity;
                velocity = new Vector3 (0f, velocity.y, velocity.z);
                _rb.velocity = velocity;
            }
            _finalTouchX = transform.position.x;

            if (Mathf.Abs (touchDelta.x) >= deltaThreshold)
            {
                _finalTouchX = (transform.position.x + (touchDelta.x * sensitivityMultiplier));
            }

            _rb.position = new Vector3 (_finalTouchX, transform.position.y, transform.position.z);
            _rb.position = new Vector3 (Mathf.Clamp (_rb.position.x, minXPos, maxXPos), _rb.position.y, _rb.position.z);

            _firstTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp (0))
        {
            ResetInputValues ();
        }

    }

    public void StopMovement ()
    {
        canMove = false;
    }

}