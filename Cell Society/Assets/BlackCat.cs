using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
        [SerializeField] private float lifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime));
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
            Destroy(gameObject);
    }
}
