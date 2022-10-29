using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    //UI canvas is getting convoluted, so I think it's better to have
    //skill UI be in its own script
    public int idIndex;
    public TMP_Text title, Description, LevelRequirementText, costText;
    public int[] connectedSkills;

    public GameObject skillTreeMenu;
    void Start()
    {
        
    }

    public void UpdateSkillTreeUI()
    {
        //title.text = $"{SkillTree.instance.skillNamesList[id]}";
        int levelReq = SkillTree.instance.skillsNodes[idIndex].PlayerLevelRequirement;
        int points = SkillTree.instance.skillsNodes[idIndex].Cost;

        title.text = $"{SkillTree.instance.skillsNodes[idIndex].Title}";
        Description.text = $"{SkillTree.instance.skillsNodes[idIndex].Description}";
        LevelRequirementText.text = $"Required Level: {levelReq}";
        costText.text = $"Cost: {points} points";


        //if player is underleveled or doesn't have enough points, then display approiate colors
        if(levelReq > PlayerStats.instance.kentoLevel || points > PlayerStats.instance.skillPoints)
            GetComponent<Image>().color = Color.gray;
        else 
            GetComponent<Image>().color = Color.green; //ideally want this hexadecimal: 3C9821

        //show whether next path is visible to player
        foreach(var connectedSkill in connectedSkills)
        {
            bool showNode = SkillTree.instance.skillsNodes[idIndex].TreeLevel > 0;
            SkillTree.instance.skillList[connectedSkill].gameObject.SetActive(showNode); //show skill node
            SkillTree.instance.connectorList[connectedSkill].SetActive(showNode); //show connectors extending from node
        }
    }

    //done on prefabs
    //have control set to buttons for now
    public void BuySkills()
    {
        //
        if(PlayerStats.instance.skillPoints < SkillTree.instance.skillsNodes[idIndex].Cost)
            return;
        PlayerStats.instance.skillPoints -= SkillTree.instance.skillsNodes[idIndex].Cost;
        SkillTree.instance.skillsNodes[idIndex].TreeLevel++;
        SkillTree.instance.UpdateAllSkillUI();
    }

    // Update is called once per frame
    void Update()
    {
        //skill tree menu opening and closing
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(skillTreeMenu.activeInHierarchy)
            {
                skillTreeMenu.SetActive(false);
            }
                
            else
            {
                //UpdateSkillTreeUI();
                skillTreeMenu.SetActive(true);
            }
                
        }
    }
}