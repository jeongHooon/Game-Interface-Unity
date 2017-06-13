using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

    public PlayerController PC;
    void Update()
    {

        if (PC.GetStage() == 1)
        {
            ;
        }
        else if (PC.GetStage() == 2)
        {
            iTween.MoveBy(gameObject, iTween.Hash("z", 4, "easeType", iTween.EaseType.easeInOutElastic
                                                          , "loopType", iTween.LoopType.pingPong));
        }
        else
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);

            iTween.MoveBy(gameObject, iTween.Hash("x", 10, "easeType", iTween.EaseType.easeInOutCirc
                                                          , "loopType", iTween.LoopType.pingPong));
        }
    }
}
