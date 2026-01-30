using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistedSystems : MonoBehaviour
{

    public static PersistedSystems Instance;


    private void Awake()
    {
        // Singleton pattern (jam-safe version)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
