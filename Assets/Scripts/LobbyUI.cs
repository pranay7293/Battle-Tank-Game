using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonInfo;
    public Button buttonMusic;
    public Button buttonSound;
    public Button close;
    public GameObject musicSlider;
    public GameObject soundSlider;
    public GameObject infoPage;
    public Slider mSlider;
    public Slider sSlider;
    private bool isActive;


    private void Start()
    {
        buttonPlay.onClick.AddListener(GamePlay);
        buttonInfo.onClick.AddListener(LoadInfo);
        close.onClick.AddListener(LoadLobby);
        buttonMusic.onClick.AddListener(ChangeMusicVolume);
        buttonSound.onClick.AddListener(ChangeSoundVolume);
        mSlider.onValueChanged.AddListener(volume => SoundManager.Instance.MusicVolume(volume));
        sSlider.onValueChanged.AddListener(volume => SoundManager.Instance.SoundVolume(volume));

        sSlider.value = 1;
        mSlider.value = 1;
    }

    private void ChangeSoundVolume()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        soundSlider.SetActive(isActive);

        if (isActive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }        
    }

    private void ChangeMusicVolume()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        musicSlider.SetActive(isActive);

        if (isActive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }        
    }

    private void LoadLobby()
    {
        SoundManager.Instance.PlaySound(Sounds.ExitButtonClick);
        infoPage.SetActive(false);
    }

    private void LoadInfo()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        infoPage.SetActive(true);
    }

    private void GamePlay()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        SceneManager.LoadScene(1);
    }


}
