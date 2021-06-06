using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityAppot;

public class AppotTest : MonoBehaviour
{
    void Start()
    {
        Appot appot = new Appot("00042643db8400000001");
        StartCoroutine(appot.getAppInfoList((error, res) => {
            if (error != null) {
                Debug.Log(error);
                return;
            }
            Debug.Log(res);
        }));
    }
    void Update()
    {


    }
}
