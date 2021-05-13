using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumData
{
    public List<float> listSpectrum = new List<float>();
    public float instantaneousEnergy;
    public float averageEnergy;
}

public class GetSpectrumDataExample : MonoBehaviour
{
    AudioSource audio;
    public float[] spectrum = new float[256];
    public float[] maxSpectrum = new float[256];
    public Dictionary<int, SpectrumData> dicSpectrumData = new Dictionary<int, SpectrumData>();

    void Start()
    {
        audio = GetComponent<AudioSource>();

        int maxSp = 256 / 4;
        int sp = 0;
        while (sp < maxSp)
        {
            dicSpectrumData.Add(sp, new SpectrumData());
        }


    }

    void Update()
    {
        audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        int i = 0;
        while (i < spectrum.Length)
        {
            if (spectrum[i] > maxSpectrum[i])
            {
                maxSpectrum[i] = spectrum[i];
            }
            i++;
        }

        for (int j = 0; j < 64; j++)
        {
            dicSpectrumData[j / 4].listSpectrum.Add(spectrum[i]);
        }

        //int i = 1;
        //while (i < spectrum.Length - 1)
        //{
        //    Debug.Log("In While");
        //    Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
        //    Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
        //    i++;
        //}


    }
}
