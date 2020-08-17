//TO DO:
//Add syncro with ScreenManager
//Refactor Jump To Game to a generic Method
//
//
//
//public GameObject camObj;
//
//[Space]
//public PartContainer menuCont;
//public PartContainer gameCont;
//
//[Space]
//public PartContainer currCont;
//
//[Space]
//public bool shallWaitSomething = false;
//public float transitionTime = 1f;
//
//
//private void Start()
//{
//    currCont = menuCont;
//}
//
//public void GoToGame()
//{
//    StartCoroutine(JumpToGame());
//}
//
//IEnumerator JumpToGame()
//{
//    yield return null;
//    gameCont.StartActivation();
//
//    menuCont.StartDeactivation();
//
//    shallWaitSomething = true;
//    float t = 0f;
//    if (shallWaitSomething)
//    {
//        while (!gameCont.isReadyToShow)
//        {
//            t += Time.deltaTime;
//            yield return null;
//        }
//        while (t <= currCont.deactivationTime)
//        {
//            t += Time.deltaTime;
//            yield return null;
//        }
//        while (t <= gameCont.activationTime)
//        {
//            t += Time.deltaTime;
//            yield return null;
//        }
//
//    }
//    else
//    { yield return new WaitForSeconds(currCont.deactivationTime); }
//
//    currCont = gameCont;
//    MoveCamToCurrent();
//
//}
//
//public void MoveCamToCurrent()
//{
//    camObj.transform.position = currCont.camPos.transform.position;
//}
//
//0--//**989qq
//public void GoToMenu()
//{
//
//}
//
//public void GoToCustome()
//{
//
//}
//