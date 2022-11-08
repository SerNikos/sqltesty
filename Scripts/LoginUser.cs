using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LoginUser : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button loginButton;
    public TMP_Text loginButtonText;

    public GameObject currentPlayerObject;


    public void Login()
    {
        loginButton.interactable = false;
        loginButtonText.text = "Sending";

        if(usernameInput.text.Length < 3)
        {
            ErrorOnLogInMessage("Check Username");
        }
        else if(passwordInput.text.Length<3)
        {
            ErrorOnLogInMessage("Check Password");
        }
        else
        {
            StartCoroutine(SendLoginForm());
        }
    }

    public void ErrorOnLogInMessage(string message)
    {
        loginButton.GetComponent<Image>().color = Color.red;
        loginButtonText.text = message;
        loginButtonText.fontSize = 15;
    }

    public void ResetLoginButton(string message)
    {
        loginButton.GetComponent<Image>().color = Color.white;
        loginButtonText.text = "Login";
        loginButtonText.fontSize = 23;
        loginButton.interactable = true;
    }

    IEnumerator SendLoginForm()
    {
        WWWForm LoginInfo = new WWWForm();
        LoginInfo.AddField("appassword", "thisisfromtheapp!");
        LoginInfo.AddField("username", usernameInput.text);
        LoginInfo.AddField("password", passwordInput.text);
        UnityWebRequest loginRequest = UnityWebRequest.Post("http://localhost/cruds/loginuser.php", LoginInfo);
        yield return loginRequest.SendWebRequest();
     
        if (loginRequest.error == null)
        {
            //1, 2, 5 server errors
            //3 username is wrong
            //4 the password is incorrect
            string result = loginRequest.downloadHandler.text;
            Debug.Log(result);
          
            if(result =="1" || result == "2" || result == "5")
            {
                ErrorOnLogInMessage("Server Error");
            }else if (result=="3")
            {
                ErrorOnLogInMessage("Check Username");
            }else if (result == "4") //exoume bug ginetai logged in xoris na einai sosto to password
            {
                ErrorOnLogInMessage("Check Password");
            }
            else
            {
                var currentPlayer = Instantiate(currentPlayerObject, new Vector3(0, 0, 0), Quaternion.identity);
                loginButton.GetComponent<Image>().color = Color.green;
                loginButtonText.text = "Login";
            }
        }else
        {
            Debug.Log(loginRequest.error);
        }

    }

}
