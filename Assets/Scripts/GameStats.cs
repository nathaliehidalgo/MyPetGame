using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return instance; } }
    private static GameStats instance;

    //Score
    public float score;
    public float highscore;
    public float distanceModifier = 1.5f;

    //Bone
    public int totalBone;
    public int boneCollectedThisSession;
    public float pointsPerBone = 10.0f;

    //Internal CoolDown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    //Action
    public Action<int> OnCollectBone;
    public Action<float> OnScoreChange;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += boneCollectedThisSession * pointsPerBone;

        if (s > score)
        {
            score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(score);
            }
            
        }
    }

    public void CollectBone()
    {
        boneCollectedThisSession++;
        OnCollectBone?.Invoke(boneCollectedThisSession);
    }

    public void ResetSession()
    {
        score = 0;
        boneCollectedThisSession = 0;

        OnCollectBone?.Invoke(boneCollectedThisSession);
        OnScoreChange?.Invoke(score);
    }

    public string ScoreToText()
    {
        return score.ToString("0000000");
    }

    public string BoneToText()
    {
        return boneCollectedThisSession.ToString("000");
    }
}
