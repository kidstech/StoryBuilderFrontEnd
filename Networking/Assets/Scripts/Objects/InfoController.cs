using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    //It is possible to use this to store all of the content packs / word packs / words in general here.
    private string username = "";
    private string[] teachers;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);	
	}

    public void setUsername (string name)
    {
        this.username = name;
    }

    public string getUsername()
    {
        return this.username;
    }

    public void setTeachers(string teachers)
    {
        this.teachers = teachers.Split(',');
    }

    public string[] getTeachers()
    {
        return this.teachers;
    }
	
}
