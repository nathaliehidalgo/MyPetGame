using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] private Transform hatContainer;
    private List<GameObject> hatModels = new List<GameObject>();
    private Hat[] hats;

    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        SpawnHats();
        SelectHat(SaveManager.Instance.save.CurrentHatIndex);
    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            GameObject newHat = Instantiate(hats[i].Model, hatContainer) as GameObject;
            newHat.transform.position = (hatContainer.position + (Vector3.up * 0.03f));
            newHat.transform.Rotate(0.0f, 0.0f, 110.0f);
            //hatModels.Add(Instantiate(hats[i].Model, hatContainer) as GameObject);
            hatModels.Add(newHat);
        }
    }

    public void DisableAllHats()
    {
        for (int i = 0; i < hatModels.Count; i++)
        {
            hatModels[i].SetActive(false);
        }
    }

    public void SelectHat(int index)
    {
        DisableAllHats();
        hatModels[index].SetActive(true);
    }

}
