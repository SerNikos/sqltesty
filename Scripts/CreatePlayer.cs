using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreatePlayer : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public Button RegisterButton;
    public TMP_Text RegisterButtonText;

    public void RegisterNewPlayer()
    {
        RegisterButton.interactable = false;
        if (usernameInput.text.Length < 3)
        {
            ErrorMessage("Username is too short");
            usernameInput.GetComponent<Image>().color = Color.red;
        }
        else if (emailInput.text.Length < 5)
        {
            ErrorMessage("email is too short");
            emailInput.GetComponent<Image>().color = Color.red;
        }
        else if (passwordInput.text.Length < 5)
        {
            ErrorMessage("password is too short");
            passwordInput.GetComponent<Image>().color = Color.red;
        }
        else
        {
            SetButtonToSending();
            StartCoroutine(CreatePlayerPostRequest());
        }
    }

    public void ErrorMessage(string message)
    {
        RegisterButton.GetComponent<Image>().color = Color.red;
        RegisterButtonText.text = message;
        RegisterButtonText.fontSize = 10;

    }

    public void ResetRegisterButton()
    {
        RegisterButton.GetComponent<Image>().color = Color.white;
        RegisterButtonText.text = "Register";
        RegisterButtonText.fontSize = 10;
        RegisterButton.interactable = true;
    }

    public void SetButtonToSending()
    {
        RegisterButton.GetComponent<Image>().color = Color.gray;
        RegisterButtonText.text = "Sending";
        RegisterButtonText.fontSize = 10;
    }

    public void SetButtonSuccess()
    {
        RegisterButton.GetComponent<Image>().color = Color.green;
        RegisterButtonText.text = "Success";
        RegisterButtonText.fontSize = 10;
    }
    /* public void ResetInputToWhite()
     {
         this.GetComponent<Image>().color = Color.white;
     }*/


    IEnumerator CreatePlayerPostRequest()
    {
        WWWForm newPlayerInfo = new WWWForm();
        //"thisisfromtheapp!" is the key to emsure that our server communication
        newPlayerInfo.AddField("appassword", "thisisfromtheapp!");
        newPlayerInfo.AddField("username", usernameInput.text);
        newPlayerInfo.AddField("email", emailInput.text);
        newPlayerInfo.AddField("password", passwordInput.text);
        UnityWebRequest CreatePostRequest = UnityWebRequest.Post("http://localhost/cruds/newPlayer.php", newPlayerInfo);
        yield return CreatePostRequest.SendWebRequest();

        if (CreatePostRequest.error == null)
        {
            Debug.Log(CreatePostRequest.downloadHandler.text);
            string response = CreatePostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "4" || response == "6")
            {
                ErrorMessage("Server Error");
            }
            else if (response == "3")
            {
                ErrorMessage("Username Already exists");
            }
            else if (response == "5")
            {
                ErrorMessage("Email Already exists");
            }
            else
            {
                SetButtonSuccess();
            }
        }
        else
        {
            Debug.Log(CreatePostRequest.error);
        }
    }
}
