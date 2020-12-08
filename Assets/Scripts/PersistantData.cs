using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    
	public int credits = 0;
	
	
	// Reset all data
	public void resetData() {
		credits = 0;
	}
	
	void Awake() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PersistantData");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
	
}
