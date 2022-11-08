using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetTest : MonoBehaviour
{

    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTesting());
    }

    IEnumerator GetTesting()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/test.php");
        yield return webRequest.SendWebRequest();
        text.text = webRequest.downloadHandler.text;
    }

}
