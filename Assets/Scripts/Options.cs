using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject _options;
    [SerializeField] private Dropdown _fpsDropdown;
    [SerializeField] private Dropdown _graphicsDropdown;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundEffectsToggle;
    [SerializeField] private Slider _sensitivitySlider;

    private AudioSource _musicSource;
    private AudioSource _audioSource;

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        LoadFPS();
        LoadGraphicsQuality();
        LoadMusic();
        LoadSoundEffects();
        LoadSensitivity();
    }

    private void LoadFPS()
    {
        int savedFPS = PlayerPrefs.GetInt("TargetFPS", 0);
        _fpsDropdown.value = savedFPS;
        SetTargetFPS(savedFPS);

        _fpsDropdown.onValueChanged.AddListener(delegate {
            SetTargetFPS(_fpsDropdown.value);
            PlayerPrefs.SetInt("TargetFPS", _fpsDropdown.value);
        });
    }

    private void LoadGraphicsQuality()
    {
        int savedGraphics = PlayerPrefs.GetInt("GraphicsQuality", 0);
        _graphicsDropdown.value = savedGraphics;
        SetGraphicsQuality(savedGraphics);

        _graphicsDropdown.onValueChanged.AddListener(delegate {
            SetGraphicsQuality(_graphicsDropdown.value);
            PlayerPrefs.SetInt("GraphicsQuality", _graphicsDropdown.value);
        });
    }

    private void LoadMusic()
    {
        bool savedMusic = PlayerPrefs.GetInt("Music", 1) == 1;
        _musicToggle.isOn = savedMusic;
        SetMusic(savedMusic);

        _musicToggle.onValueChanged.AddListener(delegate {
            SetMusic(_musicToggle.isOn);
            PlayerPrefs.SetInt("Music", _musicToggle.isOn ? 1 : 0);
        });
    }

    private void LoadSoundEffects()
    {
        bool savedSoundEffects = PlayerPrefs.GetInt("SoundEffects", 1) == 1;
        _soundEffectsToggle.isOn = savedSoundEffects;
        SetSoundEffects(savedSoundEffects);

        _soundEffectsToggle.onValueChanged.AddListener(delegate {
            SetSoundEffects(_soundEffectsToggle.isOn);
            PlayerPrefs.SetInt("SoundEffects", _soundEffectsToggle.isOn ? 1 : 0);
        });
    }

    private void LoadSensitivity()
    {
        int savedSensitivity = PlayerPrefs.GetInt("CameraSensitivity", 2);
        _sensitivitySlider.value = savedSensitivity;
        SetCameraSensitivity(savedSensitivity);

        _sensitivitySlider.onValueChanged.AddListener(delegate {
            SetCameraSensitivity(_sensitivitySlider.value);
            PlayerPrefs.SetInt("CameraSensitivity", (int)_sensitivitySlider.value);
        });
    }

    private void SetTargetFPS(int dropdownValue)
    {
        int targetFPS = 30;

        switch (dropdownValue)
        {
            case 0:
                targetFPS = 30;
                break;
            case 1:
                targetFPS = 60;

                break;
            case 2:
                targetFPS = 90;
                break;
        }

        Application.targetFrameRate = targetFPS;
    }

    private void SetGraphicsQuality(int dropdownValue)
    {
        switch (dropdownValue)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 1:
                QualitySettings.SetQualityLevel(2, true);
                break;
            case 2:
                QualitySettings.SetQualityLevel(5, true);
                break;
        }
    }

    private void SetMusic(bool isOn)
    {
        if (_musicSource != null)
        {
            Debug.Log("1");
            _musicSource.mute = !isOn;
        }
    }

    private void SetSoundEffects(bool isOn)
    {
        if (_audioSource != null)
        {
            _audioSource.mute = !isOn;
        }
    }

    public void SetCameraSensitivity(float value)
    {
    }

    public void OpenOptions()
    {
        if (!_options.activeSelf)
        {
            _options.SetActive(true);
           // _statusGame.StopGame();
        }
        else
        {
            _options.SetActive(false);
           // _statusGame.ResumeGame();
        }
    }
}
