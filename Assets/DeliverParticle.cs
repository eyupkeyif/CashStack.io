using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverParticle : MonoBehaviour
{
    public ParticleSystem deliverParticle;
    [SerializeField] LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
          
                StartParticle();
        }

        
    }
    public void StartParticle()
    {
        deliverParticle.Play();
    }
}
