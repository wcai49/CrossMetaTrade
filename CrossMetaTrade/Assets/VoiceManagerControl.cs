using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class VoiceManagerControl : MonoBehaviour
{
#if UNITY_WEBGL

    private void Awake()
    {
        Destroy(GetComponent<Recorder>());
        Destroy(GetComponent<PhotonVoiceNetwork>());
    }
#endif
}
