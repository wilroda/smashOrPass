using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishPompkin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _mainPS;
    [Space]
    [SerializeField] private ParticleSystem _ps1;
    [SerializeField] private ParticleSystem _ps2;
    [SerializeField] private ParticleSystem _ps3;
    public void SmashIt(PumpkinVisualRandomizer ppvr)
    {
        if(ppvr == null)
            return;
        
        _ps1.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color1", ppvr.Mesh.material.GetColor("_Color1"));
        _ps1.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color2", ppvr.Mesh.material.GetColor("_Color2"));

        _ps2.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color1", ppvr.Mesh.material.GetColor("_Color1"));
        _ps2.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color2", ppvr.Mesh.material.GetColor("_Color2"));

        _ps3.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color1", ppvr.Mesh.material.GetColor("_Color1"));
        _ps3.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color2", ppvr.Mesh.material.GetColor("_Color2"));

        _mainPS.Play();
    }
}
