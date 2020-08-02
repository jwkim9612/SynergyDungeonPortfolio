using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton = null;
    [SerializeField] private Text currentWaveText = null;

    public void Initialize()
    {
        backToMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }

    public void SetCurrentWaveText()
    {
        int currentWave = StageManager.Instance.currentWave;
        currentWaveText.text = $"현재 웨이브 : {currentWave}";
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
