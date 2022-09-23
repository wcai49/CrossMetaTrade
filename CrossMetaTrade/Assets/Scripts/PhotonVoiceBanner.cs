using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


using Photon.Voice.Unity;
using Photon.Voice.PUN;


public class PhotonVoiceBanner : MonoBehaviour
{
#if UNITY_WEBGL
    private void Awake()
    {
        Destroy(GetComponent<PhotonVoiceView>());
    }
#endif

#if !UNITY_WEBGL
    public GameObject Teams_Logo;
    GameObject faceCanvas;
    GameObject voiceActiveLogo;
    PhotonVoiceView voiceView;
    PhotonView view;


    private void Awake()
    {
        view = GetComponent<PhotonView>();
        voiceView = GetComponent<PhotonVoiceView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (voiceView.IsRecording)
            {
                StartTalking();
            }
            else
            {
                StopTalking();
            }
        }
    }

    public void StartTalking()
    {
        if (voiceActiveLogo != null)
        {
            return;
        }

        voiceActiveLogo = PhotonNetwork.Instantiate(Teams_Logo.name, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        voiceActiveLogo.transform.Rotate(90f, 0, 0);

#if UNITY_IOS
        // Add ios facial relevant stuffs here. @Tab
        GameObject parent = GameObject.Find("LiveCapture");
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == "LiveCaptureCanvas")
            {
                faceCanvas = t.gameObject;
                faceCanvas.SetActive(true);
            }
        }

#endif

    }

    public void StopTalking()
    {
        if (voiceActiveLogo == null)
        {
            return;
        }
        PhotonNetwork.Destroy(voiceActiveLogo);
#if UNITY_IOS
        // Add ios facial relevant stuffs here. @Tab
        faceCanvas.SetActive(false);
#endif
    }
#endif
}
