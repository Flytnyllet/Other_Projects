using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class HUD : MonoBehaviour
{
    public List<GameObject> HeartSprites = new List<GameObject>();
    public GameObject hud;

    public GameObject HeartUI;
    private int HealthCount;
    public Vector2 startPosition;

    private PlayerController player;
    private GameMaster gm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        HealthCount = player.currentHealth;
        CreateHearts();

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        HealthCount = player.currentHealth;

        while (HeartSprites.Count < HealthCount && HealthCount <= player.maxHealth)
        {
            AddHeart();
        }

        while(HeartSprites.Count > HealthCount && HealthCount >= 0)
        {
            DeleteHeart();
        }

        if (gm.gameOver)
        {
            DeleteAllHearts();
        }
    }
    void CreateHearts()
    {
        for (int i = 0; i < HealthCount; i++)
        {
            RectTransform rt = HeartUI.GetComponent<RectTransform>();

            GameObject tempHeart = GameObject.Instantiate(HeartUI, hud.transform);

            RectTransform tempRt = tempHeart.GetComponent<RectTransform>();
            tempRt.localPosition = new Vector2(startPosition.x + i * rt.rect.width * tempRt.localScale.x, startPosition.y);
            //tempRt.localScale = new Vector3(1, 1, 1);

            HeartSprites.Add(tempHeart);
        }
    }

    void AddHeart()
    {
        RectTransform rt = HeartUI.GetComponent<RectTransform>();

        GameObject tempHeart = GameObject.Instantiate(HeartUI, hud.transform);

        RectTransform tempRt = tempHeart.GetComponent<RectTransform>();
        tempRt.localPosition = new Vector2(startPosition.x + HeartSprites.Count * rt.rect.width, startPosition.y);
        tempRt.localScale = new Vector3(1, 1, 1);

        HeartSprites.Add(tempHeart);
    }

    void DeleteHeart()
    {
        Destroy(HeartSprites[HeartSprites.Count - 1]);
        HeartSprites.RemoveAt(HeartSprites.Count - 1);
    }

    void DeleteAllHearts()
    {
        for(int i = 0; i < HeartSprites.Count; i++)
        {
            Destroy(HeartSprites[i]);
        }
        HeartSprites.Clear();
    }
}
