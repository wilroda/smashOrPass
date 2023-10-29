using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinVisualRandomizer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRend;
    [SerializeField] private Gradient possibleBodyColorsGrad;
    [SerializeField] private Gradient possibleStemColorsGrad;
    [Space]
    [SerializeField] private Projector _projector;
    [SerializeField] private Material[] _possibleFaces;

    public SkinnedMeshRenderer Mesh => skinnedMeshRend;
     
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skinnedMeshRend.sharedMesh.blendShapeCount; i++)
        {
            skinnedMeshRend.SetBlendShapeWeight(i, Random.Range(0f, 100f));
        }
        skinnedMeshRend.material.SetColor("_Color1", possibleBodyColorsGrad.Evaluate(Random.Range(0f, 1f)));
        skinnedMeshRend.material.SetColor("_Color2", possibleStemColorsGrad.Evaluate(Random.Range(0f, 1f)));

        _projector.material = _possibleFaces[Random.Range(0, _possibleFaces.Length)];
        _projector.orthographicSize = Random.Range(.35f, .75f);
    }
    
}
