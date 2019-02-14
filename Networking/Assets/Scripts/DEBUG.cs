using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PrintTeacherList();
	}

    private void PrintTeacherList()
    {
        InfoController ic = (InfoController)FindObjectOfType(typeof(InfoController));

        for(int i = 0; i < ic.getTeachers().Length; i++)
        {
            Debug.Log(ic.getTeachers()[i]);
        }
    }
	
}
