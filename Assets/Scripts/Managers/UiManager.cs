using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        #region Variable Declarations
        public static UiManager instance;
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI gemText;
        [SerializeField] private TextMeshProUGUI finishGemText;
        [SerializeField] private TextMeshProUGUI floatingTextPrefab;
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject finishUI;
        [SerializeField] private Canvas canvas;
#pragma warning restore 0649
        private int _gemScore;
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
            _gemScore += amount;
            _gemScore *= multiplier;
            gemText.text = _gemScore.ToString ();
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
            finishGemText.text = _gemScore.ToString ();
            finishUI.SetActive (true);
        }
        public void ActivateRestartButton ()
        {
            restartButton.gameObject.SetActive (true);
        }
        public void Restart ()
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        }
        public void DisableStartText ()
        {
            startText.gameObject.SetActive (false);
        }

    }
}