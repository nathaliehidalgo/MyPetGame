using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    public AudioSource boneCollectSound;
    [SerializeField] private TextMeshProUGUI boneCount;
    [SerializeField] private TextMeshProUGUI scoreCount;

    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStats.Instance.OnCollectBone += OnCollectBone;
        GameStats.Instance.OnScoreChange += OnScoreChange;
        
        gameUI.SetActive(true);
    }

    private void OnCollectBone(int amnCollected)
    {
        boneCount.text = GameStats.Instance.BoneToText();
        if(amnCollected > 0){
            boneCollectSound.Play();
        }
    }

    private void OnScoreChange(float score)
    {
        scoreCount.text = GameStats.Instance.ScoreToText();
    }
    

    public override void Destruct()
    {
        gameUI.SetActive(false);

        GameStats.Instance.OnCollectBone -= OnCollectBone;
        GameStats.Instance.OnScoreChange -= OnScoreChange;
    }


    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.sceneChunkGeneration.ScanPosition();
    }


}


