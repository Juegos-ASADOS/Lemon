using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimoncinTalk : MonoBehaviour
{
    public void talkLimoncinDemon() {
        FMOD_Manager.instance.SetGlobalParameterByName("Demon",1);
    }

    public void talkLimoncinChill()
    {
        FMOD_Manager.instance.SetGlobalParameterByName("Demon", 0);
    }
}
