using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform finishLine;
    [SerializeField] private Slider slider;

    private float _maxDistance;

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