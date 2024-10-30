using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Unity.AI.Navigation;

public class TileNavMesh : MonoBehaviour
{
    NavMeshSurface navMeshe;
    private void Awake()
    {
        navMeshe = GetComponent<NavMeshSurface>();

        navMeshe.BuildNavMesh();

    }
}
