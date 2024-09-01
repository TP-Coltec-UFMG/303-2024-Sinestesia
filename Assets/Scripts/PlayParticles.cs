using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    public ParticleSystem sistemaParticulas;

    public void ActivateParticles()
    {
        if (sistemaParticulas != null)
        {
            sistemaParticulas.Play(); // Ativa o Particle System
        }
    }
}
