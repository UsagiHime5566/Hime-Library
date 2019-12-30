using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 殘影特效
/// </summary>
public class AfterImageEffects : MonoBehaviour
{

    //開啟殘影
    public bool _OpenAfterImage;

    //殘影顏色
    public Color _AfterImageColor = Color.black;
    //殘影的生存時間
    public float _SurvivalTime = 1;
    //生成殘影的間隔時間
    public float _IntervalTime = 0.2f;
    private float _Time = 0;
    //殘影初始透明度
    [Range(0.1f, 1.0f)]
    public float _InitialAlpha = 1.0f;

    private List<AfterImage> _AfterImageList;

    private MeshFilter _meshFilter;
    private MeshRenderer _SkinnedMeshRenderer;

    void Awake()
    {
        _AfterImageList = new List<AfterImage>();
        _meshFilter = GetComponent<MeshFilter>();
        _SkinnedMeshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (_OpenAfterImage && _AfterImageList != null)
        {
            if (_SkinnedMeshRenderer == null)
            {
                _OpenAfterImage = false;
                return;
            }

            _Time += Time.deltaTime;
            //生成殘影
            CreateAfterImage();
            //刷新殘影
            UpdateAfterImage();
        }
    }
    /// <summary>
    /// 生成殘影
    /// </summary>
    void CreateAfterImage()
    {
        //生成殘影
        if (_Time >= _IntervalTime)
        {
            _Time = 0;

            Mesh mesh = Instantiate(_meshFilter.mesh);
            //_SkinnedMeshRenderer.BakeMesh(mesh);

            Material material = new Material(_SkinnedMeshRenderer.material);
            SetMaterialRenderingMode(material, RenderingMode.Fade);

            _AfterImageList.Add(new AfterImage(
                mesh,
                material,
                transform.localToWorldMatrix,
                _InitialAlpha,
                Time.realtimeSinceStartup,
                _SurvivalTime));
        }
    }
    /// <summary>
    /// 刷新殘影
    /// </summary>
    void UpdateAfterImage()
    {
        //刷新殘影，根據生存時間銷毀已過時的殘影
        for (int i = 0; i < _AfterImageList.Count; i++)
        {
            float _PassingTime = Time.realtimeSinceStartup - _AfterImageList[i]._StartTime;

            if (_PassingTime > _AfterImageList[i]._Duration)
            {
                _AfterImageList.Remove(_AfterImageList[i]);
                Destroy(_AfterImageList[i]);
                continue;
            }

            if (_AfterImageList[i]._Material.HasProperty("_Color"))
            {
                _AfterImageList[i]._Alpha *= (1 - _PassingTime / _AfterImageList[i]._Duration);
                _AfterImageColor.a = _AfterImageList[i]._Alpha;
                _AfterImageList[i]._Material.SetColor("_Color", _AfterImageColor);
            }

            Graphics.DrawMesh(_AfterImageList[i]._Mesh, _AfterImageList[i]._Matrix, _AfterImageList[i]._Material, gameObject.layer);
        }
    }
    /// <summary>
    /// 設置紋理渲染模式
    /// </summary>
    void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
public enum RenderingMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent,
}
class AfterImage : Object
{
    //殘影網格
    public Mesh _Mesh;
    //殘影紋理
    public Material _Material;
    //殘影位置
    public Matrix4x4 _Matrix;
    //殘影透明度
    public float _Alpha;
    //殘影啟動時間
    public float _StartTime;
    //殘影保留時間
    public float _Duration;

    public AfterImage(Mesh mesh, Material material, Matrix4x4 matrix4x4, float alpha, float startTime, float duration)
    {
        _Mesh = mesh;
        _Material = material;
        _Matrix = matrix4x4;
        _Alpha = alpha;
        _StartTime = startTime;
        _Duration = duration;
    }
}

//————————————————
//版權聲明：本文為CSDN博主「神碼編程」的原創文章，遵循 CC 4.0 BY-SA 版權協議，轉載請附上原文出處鏈接及本聲明。
//原文鏈接：https://blog.csdn.net/qq992817263/article/details/52994907