using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalBone;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;
    public AudioSource menuSound;
    public AudioSource buySound;
    public AudioSource notEnoughBonesSound;
    private bool isInit = false;
    private int hatCount;
    private int unlockedHatCount;

    //Shop Item

    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    //Completion Circle
    public Image completionCircle;
    public TextMeshProUGUI completionText;


    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        hats = Resources.LoadAll<Hat>("Hat");
        shopUI.SetActive(true);   


        if (!isInit)
        {
        totalBone.text = SaveManager.Instance.save.Bone.ToString("000");
        currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
        PopulateShop();
        isInit = true;
        }

        ResetCompletionCircle();

    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer);
            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            // Thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
            //Price
            if(SaveManager.Instance.save.UnlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            else
            {
               go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =  "";
               unlockedHatCount++;
            }
          
        }

    }

    private void OnHatClick(int i) 
    {
        Debug.Log("hat position: " + i);
       if (SaveManager.Instance.save.UnlockedHatFlag[i] == 1) 
      {
            SaveManager.Instance.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            menuSound.Play();
            SaveManager.Instance.Save();
       } 
        else if (hats[i].ItemPrice <= SaveManager.Instance.save.Bone) 
        {
           SaveManager.Instance.save.Bone -= hats[i].ItemPrice;
           SaveManager.Instance.save.UnlockedHatFlag[i] = 1;
           SaveManager.Instance.save.CurrentHatIndex = i;
           currentHatName.text = hats[i].ItemName;
           hatLogic.SelectHat(i);
           totalBone.text = SaveManager.Instance.save.Bone.ToString("000");
           buySound.Play();
           SaveManager.Instance.Save();
           hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =  "";
           unlockedHatCount++;
           ResetCompletionCircle();
       }
       else 
      {
           Debug.Log("Not enough bones");
           notEnoughBonesSound.Play();
       }
    }

    private void ResetCompletionCircle()
    {
        int hatCount = hats.Length - 1;
        int currentlyUnlockedCount = unlockedHatCount - 1;

        completionCircle.fillAmount = (float)currentlyUnlockedCount / (float)hatCount;
        completionText.text = currentlyUnlockedCount + " / " + hatCount;
    }

    public void OnHomeClick()
    {
      brain.ChangeState(GetComponent<GameStateInit>());
      menuSound.Play();
    }

}
