// tutorial https://www.youtube.com/watch?v=l6-nlk3njv4&pp=2Ab6AQ%3D%3D (he is THE BEST)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{ // ENCAPSULATED PROPERTIES
    [SerializeField] private int level;
    [SerializeField] private int currentExp;
    [SerializeField] private int expToLevel = 10;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int ExpToLevel => expToLevel;



    public float expGrowthMultiplier = 1.2f;    //Add 20% more EXP to level each new level
    public Slider expSlider;
    public TMP_Text currentLevelText;

    [SerializeField] private int expPerLoot = 2;



    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainExperience(2);
        }
    }

    public void GainExperience(int amount) // If you gain enough EXP to jump multiple levels, your code levels up (not only once))
    {
        currentExp += amount;

        while (currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }



    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel; // if 11/10, 1 will go to 1hp on a new level
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);

    }
    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel; // max value is always the same as our exp level
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
    private void OnLootPickedUp(ItemSO itemSO, int quantity) // ON LOOT - ADDS EXP.... (for now)
    {
        if (itemSO != null && itemSO.isGold) return;
        GainExperience(expPerLoot * quantity);
    }


    private void OnEnable()
    {
        Loot.OnItemLooted += OnLootPickedUp;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= OnLootPickedUp;
    }

}