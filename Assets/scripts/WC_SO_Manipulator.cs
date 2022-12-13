using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WC_SO_Manipulator : MonoBehaviour
{
    [SerializeField] private WolrdContextImport So;

    public void ChangeLevel(int lvl)
    {
        lvl++;
        
        if (lvl > 10)
        {
            lvl = 10;
        }

        if (lvl <= 0)
        {
            lvl = 1;
        }
        
        So.BaseLevel = lvl;
    }

    public void ChangeDanger(int danger)
    {
        if (danger >= 3)
        {
            danger = 3;
        }

        So.level = (DangerLevel)danger;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
