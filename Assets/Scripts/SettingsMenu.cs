using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer m_AudioMixer;
    public Slider m_VolumeSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float volume = PlayerPrefs.GetFloat("volume");

            Assert.IsNotNull(m_AudioMixer);
            m_AudioMixer.SetFloat("volume", volume);

            Assert.IsNotNull(m_VolumeSlider);
            m_VolumeSlider.SetValueWithoutNotify(volume);
        }
    }

    public void SetVolume(float volume)
    {
        m_AudioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
