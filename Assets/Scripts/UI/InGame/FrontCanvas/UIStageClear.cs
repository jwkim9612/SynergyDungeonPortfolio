using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStageClear : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton = null;

    public void Initialize()
    {
        backToMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
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
