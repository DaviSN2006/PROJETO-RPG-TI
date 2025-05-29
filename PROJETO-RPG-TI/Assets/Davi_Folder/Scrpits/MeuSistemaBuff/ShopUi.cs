using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    public GameObject buffButtonPrefab;
    public Transform buffButtonContainer;

    private List<GameObject> buttonPool = new List<GameObject>();

    private void Start()
    {
        InitializeButtonPool();
        CreateButtons();
    }

    private void InitializeButtonPool()
    {
        for (int i = 0; i < Shop.Instance.availableBuffs.Length; i++)
        {
            GameObject buttonObj = Instantiate(buffButtonPrefab, buffButtonContainer);
            buttonObj.SetActive(false);
            buttonPool.Add(buttonObj);
        }
    }

    private void CreateButtons()
    {
        for (int i = 0; i < Shop.Instance.availableBuffs.Length; i++)
        {
            GameObject buttonObj = buttonPool[i];
            buttonObj.SetActive(true);

            BuffData buff = Shop.Instance.availableBuffs[i];
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = $"{buff.buffName}\n{buff.description}\nCusto: {buff.cost}";

            Button button = buttonObj.GetComponent<Button>();
            int buffIndex = i;
            button.onClick.AddListener(() => Shop.Instance.BuyBuff(buffIndex));
        }
    }
}