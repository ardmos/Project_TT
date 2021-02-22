using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Text Mesh, MeshRenderer를 이용한 테스트 표현 시,  소팅 레이어 기능 .   해당 오브젝트에 붙이면 됨. 
/// </summary>


public class SortingLayerInMeshRenderer : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;


    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }
}
