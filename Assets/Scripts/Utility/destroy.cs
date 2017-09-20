using System.Collections;
using UnityEngine;

public class destroy : MonoBehaviour {

    //This script destroys any gameobject that it is attatched to, after "DestroyAfterSeconds" seconds

    [SerializeField]private float DestroyAfterSeconds;


    private void Awake()
    {
        Destroy(gameObject, DestroyAfterSeconds);
    }
}
