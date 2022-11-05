using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveState
{
    public int Highscore {set; get;}
    public int Bone {set; get;}
    public DateTime LastSaveTime {set; get;}

    public SaveState()
    {
        Highscore = 0;
        Bone = 0;
        LastSaveTime = DateTime.Now;
    }

}
