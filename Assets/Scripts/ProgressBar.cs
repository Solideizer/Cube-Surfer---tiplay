using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Variable Declarations
#pragma warning disable 0649
    [SerializeField] private Transform player;
    [SerializeField] private Transform finishLine;
    [SerializeField] private Slider slider;
#pragma warning restore 0649

    private float _maxDistance;
    #endregion
    void Start ()
    {
        _maxDistance = GetDistance ();
    }

    void Update ()
    {
        if (player.position.z <= _maxDistance && player.position.z <= finishLine.position.z)
        {
            float distance = 1 - (GetDistance () / _maxDistance);
            SetProgress (distance);

        }
    }

    float GetDistance ()
    {
        return Vector3.Distance (player.position, finishLine.position);
    }

    void SetProgress (float p)
    {
        slider.value = p;
    }
}