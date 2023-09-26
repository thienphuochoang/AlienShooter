using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    [SerializeField] private Renderer[] meshes;
    private HealthSystem healthSystem;
    [SerializeField]
    private List<Material> _originalMatList;
    [SerializeField] private Material _hitMat;
    [SerializeField] private float flashDuration = 0.1f;

    private void Start()
    {
        //Material mat = mesh.material;
        //mesh.material = new Material(mat);
        //_originalMat = mesh.material;
        //originalColor = mesh.material.GetColor(emissionColorPropertyName);
        _originalMatList = new List<Material>();
        healthSystem = GetComponent<HealthSystem>();
        GetMeshMaterials();
        healthSystem.onDamaged += TookDamage;
    }

    private void TookDamage(float amountOfHealth, GameObject attacker)
    {
        StartCoroutine(nameof(FlashFX));
    }

    private void GetMeshMaterials()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            _originalMatList.Add(meshes[i].material);
        }
    }

    private void SetMeshMaterial(List<Material> mats)
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].material = mats[i];
        }
    }

    public IEnumerator FlashFX()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].material = _hitMat;
        }
        //mesh.material = _hitMat;
        yield return new WaitForSeconds(flashDuration);
        //mesh.material = _originalMat;
        SetMeshMaterial(_originalMatList);
    }
}
