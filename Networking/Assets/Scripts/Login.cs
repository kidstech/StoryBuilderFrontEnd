using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", SHA256(passwordField.text));

        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);

        yield return www;

        if(www.text == "1")
        {
            Debug.Log("Error communicating with the database");
        }
        else if(www.text == "2")
        {
            Debug.Log("Either your username or password is incorrect");
        }
        else if(www.text == string.Empty)
        {
            Debug.Log("Unable to connect to the server");
        }
        else
        {
            Debug.Log("User login successful!");
            Debug.Log(www.text);

            //Get the 'global' controller for storing objects
            InfoController ic = (InfoController)FindObjectOfType(typeof(InfoController));
            ic.setTeachers(www.text);
            ic.setUsername(nameField.text);

           UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }

    public void VerifyInputs()
    {
        //Make sure the username is larger than 4 characters and the password is longer than 8 before we actually let the users click the submit button.
        submitButton.interactable = (nameField.text.Length >= 2 && passwordField.text.Length >= 2);
    }

    //Strong hash function
    public string SHA256(string plain_text)
    {
        //Turn the characters in bytes
        byte[] bytes = Encoding.UTF8.GetBytes(plain_text);

        //Construct object to apply SHA256
        SHA256Managed hashstring = new SHA256Managed();

        //Get the hash of computing SHA256 with our string in bytes
        byte[] hash = hashstring.ComputeHash(bytes);

        //Ready a string for returning
        string cipher_text = string.Empty;

        //For each byte, format it back into a letter or number
        foreach (byte x in hash)
        {
            cipher_text += String.Format("{0:x2}", x);
        }

        //Return cipehr text
        return cipher_text;
    }
}