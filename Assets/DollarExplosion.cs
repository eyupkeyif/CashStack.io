using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarExplosion : MonoBehaviour
{
    [SerializeField] ParticleSystem treasure;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMoneyColor(Color color)
    {
        var main = treasure.main;
        main.startColor = color;
    }
}
