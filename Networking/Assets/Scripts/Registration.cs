using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour {

    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", SHA256(passwordField.text));

        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);

        yield return www;

        if(www.text.Equals("0", StringComparison.InvariantCultureIgnoreCase))
        {
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if(www.text == string.Empty)
        {
            Debug.Log("Unable to connect to the server!");
        }
        else
        {
            Debug.Log("User creation failed. Error code " + www.text);
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
        foreach(byte x in hash)
        {
            cipher_text += String.Format("{0:x2}", x);
        }

        //Return cipehr text
        return cipher_text;
    }
}