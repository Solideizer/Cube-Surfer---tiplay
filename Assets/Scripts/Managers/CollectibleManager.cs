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

#pragma warning restore 0649
        private Vector3 _spawnPos;
        private Animator _anim;

        #endregion
        private void Awake ()
        {
            _anim = GetComponentInChildren<Animator> ();
            _spawnPos = stackTransform.position;

        }
        private void Start ()
        {
            CreateFirstCube ();
        }

        private void CreateFirstCube ()
        {
            var firstCube = Instantiate (cubePrefab, new Vector3 (0f, 0.1f, 0f), Quaternion.identity);
            firstCube.transform.SetParent (stackTransform.transform, false);
        }

        private void OnTriggerEnter (Collider other)
        {
            CheckForCubes (other);
            CheckForGems (other);
        }

        private void CheckForCubes (Collider other)
        {
            if (other.CompareTag ("CollectibleCube"))
            {
                Destroy (other.gameObject);
                AudioManager.PlaySound ("collect");
                UiManager.instance.TextPopup ();
                AddCube ();
            }
        }

        private void CheckForGems (Collider other)
        {
            if (other.CompareTag ("Gem"))
            {
                Destroy (other.gameObject);
                AudioManager.PlaySound ("gem");

                UiManager.instance.HandleGemCollection (1, 1);

            }
        }

        private void AddCube ()
        {
            playerModel.transform.position += new Vector3 (0, 1f, 0);

            var cube = Instantiate (cubePrefab, _spawnPos + new Vector3 (0, 1f, 0), Quaternion.identity);
            cube.transform.SetParent (stackTransform.transform, false);
            cube.transform.localScale = new Vector3 (1f, 1f, 1f);

            _spawnPos += new Vector3 (0f, 1f, 0);
        }
    }
}