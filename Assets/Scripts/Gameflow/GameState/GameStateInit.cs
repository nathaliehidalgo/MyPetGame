using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    public AudioSource menuSound;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI bonecountText;
    [SerializeField] private AudioClip menuLoopMusic;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        hiscoreText.text = "Highscore: " + SaveManager.Instance.save.Highscore.ToString();
        bonecountText.text = "Bone: " + SaveManager.Instance.save.Bone.ToString();

        menuUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithXFade(menuLoopMusic, 0.5f);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
        menuSound.Play();
    }

    public void OnShopClick()
    {
      brain.ChangeState(GetComponent<GameStateShop>());
      menuSound.Play();
    }

}

