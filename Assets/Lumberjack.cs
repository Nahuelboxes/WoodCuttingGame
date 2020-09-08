using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    public bool inGame = false;

    public Animator anim;
    public string hitTriggerName;
    public string wrongTriggerName;
    public string loosingLvl;
    public string winningLvl;
    public string goToIdle;
    public string startGame;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void GoToIdle()
    {
        anim.SetTrigger(goToIdle);
    }

    public void HitTree()
    {
        //print("Golpe Del Leñador!");
        anim.SetTrigger(hitTriggerName);
    }

    public void HitWrong()
    {
        //print("Uhhh el leñador le pifió...");
        anim.SetTrigger(wrongTriggerName);
    }

    public void LooseLvl()
    {
        anim.SetTrigger(loosingLvl);
    }

    public void WinLvl()
    {
        anim.SetTrigger(winningLvl);
    }

    public void Spawn(){
        anim.CrossFade("Spawn",0);
    }
    public void StartGame(){
        anim.SetTrigger(startGame);
    }
}
