using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teacher : MonoBehaviour
{

    // Prefab for the buttons created for each teacher.
    public GameObject buttonPrefab;
    public Canvas canvas;

    private string[] teachers;

    //This is dangerous if you're not actually starting at the main menu!!
    InfoController ic;


    // Create buttons for each teacher on stage up.
    void Start ()
    {
        // Max of 8 teachers at once.
        GameObject[] buttonArray = new GameObject[8];

        ic = (InfoController)FindObjectOfType(typeof(InfoController));
        teachers = ic.getTeachers();

        //DEBUG_PACKS();

        for (int i = 0; i < teachers.Length; i++)
        {
            var fix = i;
            buttonArray[i] = Instantiate(buttonPrefab) as GameObject;
            buttonArray[i].transform.SetParent(canvas.transform);

            // Perform trig to make teacher's things appear as buttons
            float c = 360 / teachers.Length;
            float xcord = Mathf.Cos(c * i * Mathf.Deg2Rad + Mathf.PI / 2) * Screen.height / 4;
            float ycord = Mathf.Sin(c * i * Mathf.Deg2Rad + Mathf.PI / 2) * Screen.height / 4;

            buttonArray[i].transform.position = new Vector3(xcord + Screen.width / 2, ycord + Screen.height / 2, 0);
            buttonArray[i].GetComponentInChildren<Text>().text = teachers[i];
            buttonArray[i].GetComponent<Button>().onClick.AddListener(() => CallRequestContentPacks(teachers[fix]));

        }
    }

    void CallRequestContentPacks(string name)
    {
        StartCoroutine(RequestContentPacks(name));
    }

    IEnumerator RequestContentPacks(string name)
    {
        WWWForm form = new WWWForm();
        Debug.Log(ic.getUsername());
        form.AddField("name", ic.getUsername());
        form.AddField("teacher", name);

        WWW www = new WWW("http://localhost/sqlconnect/class.php", form);

        yield return www;

        Debug.Log(www.text);

        /*Data handling needs to be worked on...
        if (www.text == "1")
        {
            Debug.Log("Error communicating with the database");
        }
        else if (www.text == "2")
        {
            Debug.Log("Either your username or password is incorrect");
        }
        else if (www.text == string.Empty)
        {
            Debug.Log("Unable to connect to the server");
        }
        else
        {
            Debug.Log("Success!");
        }*/
    }

    void DEBUG_PACKS()
    {
        teachers = new string[5];
        teachers[0] = "nic";
        teachers[1] = "elena";
        teachers[2] = "kk";
        teachers[3] = "kk";
        teachers[4] = "ben";
    }
}
