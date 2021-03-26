using TMPro;
using UnityEngine;
namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        #region Variable Declarations
        public static UiManager instance = null;
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI gemText;
        [SerializeField] private TextMeshProUGUI finishGemText;
        [SerializeField] private TextMeshProUGUI floatingTextPrefab;
        [SerializeField] private GameObject finishUI;
        [SerializeField] private Canvas canvas;
#pragma warning restore 0649
        private int gemScore;
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

        private void Start ()
        {
            gemText.text = 0. ToString ();
        }

        public void HandleGemCollection (int amount, int multiplier)
        {
            gemScore += amount;
            gemScore *= multiplier;
            gemText.text = gemScore.ToString ();
        }
        public void TextPopup ()
        {
            var floatingText = Instantiate (floatingTextPrefab, Vector3.zero, Quaternion.identity);
            floatingText.text = "+1";
            floatingText.transform.SetParent (canvas.transform);
            floatingText.transform.position = new Vector3 (230f, 210f, 0f);
            Destroy (floatingText, 0.4f);
        }
        private void OnEnable ()
        {
            ObtacleDetector.LevelComplete += LevelComplete;
        }
        private void Disable ()
        {
            ObtacleDetector.LevelComplete -= LevelComplete;
        }
        private void LevelComplete ()
        {
            finishGemText.text = gemScore.ToString ();
            finishUI.SetActive (true);
        }

    }
}