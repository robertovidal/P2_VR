using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject soundPanel;
    public GameObject aboutUsPanel;
    public GameObject howToPlayPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainScene() {
        SceneManager.LoadScene(1);
    }

    public void ExitApp(){
        Application.Quit();
    }

    public void ShowVolumeMenu() {
        DeactivateAllPanels();
        soundPanel.SetActive(true);
    }

    public void ShowAboutUs() {
        DeactivateAllPanels();
        aboutUsPanel.SetActive(true);
    }

    public void ShowHowToPlay() {
        DeactivateAllPanels();
        howToPlayPanel.SetActive(true);
    }

    public void IncreaseVolume(Text text) {
        var currentValue = float.Parse(text.text);
        float newValue;
        if (currentValue<1.0f) {
            newValue = currentValue+0.1f;
            AudioListener.volume = newValue;
            text.text = newValue.ToString("0.0");
        }
    }
    
    public void DecreaseVolume(Text text) {
        var currentValue = float.Parse(text.text);
        float newValue;
        if (currentValue>0.0f) {
            newValue = currentValue-0.1f;
            AudioListener.volume = newValue;
            text.text = newValue.ToString("0.0");
        }
    }

    private void DeactivateAllPanels() {
        soundPanel.SetActive(false);
        aboutUsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }
}
