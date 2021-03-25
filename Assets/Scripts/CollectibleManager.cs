using TMPro;
using UnityEngine;

namespace Managers
{
    public class CollectibleManager : MonoBehaviour
    {
        #region Variable Declarations
#pragma warning disable 0649
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private GameObject playerModel;
        [SerializeField] private Transform stackTransform;
        [SerializeField] private TextMeshProUGUI floatingTextPrefab;
        [SerializeField] private TextMeshProUGUI gemText;
        [SerializeField] private Canvas canvas;

#pragma warning restore 0649
        private Vector3 _spawnPos;
        private Animator _anim;
        private int gemScore;

        #endregion
        private void Awake ()
        {
            //playerRb = playerModel.GetComponent<Rigidbody> ();
            _anim = GetComponentInChildren<Animator> ();
            _spawnPos = stackTransform.position;
            gemText.text = 0. ToString ();
        }
        private void Start ()
        {
            var firstCube = Instantiate (cubePrefab, new Vector3 (0f, 0.1f, 0f), Quaternion.identity);
            firstCube.transform.SetParent (stackTransform.transform, false);
        }
        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag ("CollectibleCube"))
            {
                Destroy (other.gameObject);
                AudioManager.PlaySound ("collect");
                TextPopup ();
                AddCube ();
            }
            else if (other.CompareTag ("Gem"))
            {
                Destroy (other.gameObject);
                AudioManager.PlaySound ("gem");
                gemScore++;
                gemText.text = gemScore.ToString ();

            }
        }

        private void TextPopup ()
        {
            var floatingText = Instantiate (floatingTextPrefab, Vector3.zero, Quaternion.identity);
            floatingText.text = "+1";
            floatingText.transform.SetParent (canvas.transform);
            floatingText.transform.position = new Vector3 (230f, 210f, 0f);
            Destroy (floatingText, 0.4f);
        }

        private void AddCube ()
        {
            playerModel.transform.position += new Vector3 (0, 1f, 0);
            _anim.SetTrigger ("Jump");

            var cube = Instantiate (cubePrefab, _spawnPos + new Vector3 (0, 1f, 0), Quaternion.identity);
            cube.transform.SetParent (stackTransform.transform, false);
            cube.transform.localScale = new Vector3 (1f, 1f, 1f);

            _spawnPos += new Vector3 (0f, 1f, 0);
        }
    }
}