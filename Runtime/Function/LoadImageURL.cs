using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadImageURL : MonoBehaviour
{
    public string fileName = @"target";
    string fileSuffixJPG = @".jpg";
    string fileSuffixPNG = @".png";
    void Start()
    {
        //From Local path
        string root = Application.dataPath + "/../";
        string urlLocal = root + fileName + fileSuffixPNG;
        if (!System.IO.File.Exists(urlLocal))
            urlLocal = root + fileName + fileSuffixJPG;

        Debug.Log(urlLocal);
        StartCoroutine(DownloadImage(urlLocal, LoadImageToMesh));

        //From internet
        string urlNet = "https://i.imgur.com/DpRAzV5.png";
        StartCoroutine(DownloadImage(urlNet, LoadImageToMesh));
    }

    IEnumerator DownloadImage(string MediaUrl, System.Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else {
            callback(((DownloadHandlerTexture)request.downloadHandler).texture);
        }
    }

    void LoadImageToMesh(Texture2D tex){
        //targetMesh.material.mainTexture = tex;
    }

    void LoadImageToRawImage(Texture2D tex){
        //targetRawImage.texture = tex;
    }
}
