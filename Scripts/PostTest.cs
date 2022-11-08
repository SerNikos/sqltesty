using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostTest : MonoBehaviour
{

    public TMP_InputField name;
    public TMP_Text result;


    public void Button()
    {
        StartCoroutine(Post());
    }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name.text);
        UnityWebRequest postTest = UnityWebRequest.Post("http://localhost/postTest.php", form);
        yield return postTest.SendWebRequest();
        result.text = postTest.downloadHandler.text;
    }
}
