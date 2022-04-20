using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomChild : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private GameObject activeObject;

    public bool giveRandomMaterial;
    [SerializeField] private Material[] mats;

    private void Start()
    {
        ActivateRandom();
        if (giveRandomMaterial)
            GiveRandomMaterial();
    }

    public void ActivateRandom()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(false);
        activeObject = objects[Random.Range(0, objects.Length)];
        activeObject.SetActive(true);
    }

    private void GiveRandomMaterial()
    {
        if (!gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer))
            return;
        var newMaterials = meshRenderer.materials;
        newMaterials[1] = mats[Random.Range(0, mats.Length)];
        meshRenderer.materials = newMaterials;
    }
}
