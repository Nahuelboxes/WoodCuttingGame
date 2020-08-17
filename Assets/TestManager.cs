using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{

    public Dropdown modeDrop;
    public InputField treeSizeField;
    public Toggle showNumbersToggle;

    public Text timer;
    public float t = 0f;
    public bool playing = false;

    public TreeScript tree;


    void Update()
    {
        if (playing)
        {
            t += Time.deltaTime;//Time.time - inicio;
            string minutos = Mathf.Floor(((int)t / 60)).ToString();
            string segundos = Mathf.Floor((t % 60)).ToString("f00");
            string milisegundos = Mathf.Floor((t * 10 % 10)).ToString("f00");

            timer.text = minutos + ":" + segundos + ":" + milisegundos;
        }
    }

    public void StartGame()
    {
        ////Game Mode
        //switch (modeDrop.value)
        //{
        //    case 0:
        //        tree.targetZone.currGameMode = gameMode.normal;

        //    break;

        //    case 1:
        //        tree.targetZone.currGameMode = gameMode.simon;

        //        break;

        //    default:
        //        break;
        //}

        //tree.treeHeight = int.Parse (treeSizeField.text);
        //if (tree.treeHeight <= 1)
        //{
        //    tree.treeHeight = 50;
        //}

        //tree.targetZone.simonMode.showNumbers = showNumbersToggle.isOn;

        //tree.CreateTree();


        //playing = true;
        //t = 0f;

    }

    public void EndGame()
    {
        playing = false;
    }


    public void ReloadLvl()
    {
        SceneManager.LoadScene(0);
    }

}
