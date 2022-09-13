using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    [SerializeField]
    private float _dayLength; //3 =3min, 0.5 = 30s
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    [SerializeField]
    [Range(2f,1000)]
    private float _changeSpeed; // Acceleration or deceleration speed
    private float _timeScale = 100f;

    [Header("Sun Light")]
    [SerializeField]
    private Transform dailyRotation;
    [SerializeField]
    private Light sun;
    [SerializeField]
    private Gradient sunColor;
    private float intensity;

    private float sunBaseIntensity = 0.6f;
    private float sunVariation = 1.0f;


    [Header("Moon Light")]
    [SerializeField]
    private Light moon;
    [SerializeField]
    private Gradient moonColor;

    private float moonBaseIntensity = 0.3f;

    [Header("Skybox")]
    public MySkybox[] skyboxes;
    public MySkybox currentSkybox;

    [System.Serializable]
    public struct MySkybox {
        public string name;
        public Material skybox;
    }

    private Dictionary<string, MySkybox> _skyDic;

 
    // Start is called before the first frame update
    void Start()
    {
        _skyDic = new Dictionary<string, MySkybox>();
        foreach (MySkybox skybox in skyboxes) {
            _skyDic.Add(skybox.name, skybox); 
        }
        
        UpdataTimeScale();
        UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {

        ControlTime();
        //UpdataTimeScale();
        //UpdateTime();

        AdjustSun();
        SunIntensity();
        AdjustMoon();
    }

    // change the real time to the game time
    private void UpdataTimeScale()
    {
        _timeScale = 24 / (_dayLength / 60);
    }

    private void UpdateTime()
    {
         _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if (_timeOfDay >= 1)
        {
            _timeOfDay -= 1;
        }
        else if (_timeOfDay < 0) {
            _timeOfDay += 1;
        }
        if (_timeOfDay > 0 && _timeOfDay < 0.32) 
        {
            currentSkybox = _skyDic["earlyDusk"];
        }
        else if (_timeOfDay < 0.32)
        {
            currentSkybox = _skyDic["earlyMorning"];
        }
        else if (_timeOfDay < 0.45)
        {
            currentSkybox = _skyDic["brightMorning"]; 
        }
        else if (_timeOfDay < 0.68)
        {
            currentSkybox = _skyDic["noon"];
        }
        else if (_timeOfDay < 0.74)
        {
            currentSkybox = _skyDic["earlyDusk"];
        }
        else
        {
            currentSkybox = _skyDic["midNight"];
        }
        RenderSettings.skybox = new Material(currentSkybox.skybox);
    }



    private void AdjustSun()
    {
        float angle = _timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // adjust color
        sun.color = sunColor.Evaluate(intensity);
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustMoon()
    {
        moon.color = moonColor.Evaluate(1 - intensity);
        moon.intensity = (1 - intensity) * moonBaseIntensity + 0.1f;
    }

    // fast forward and backward
    private void ControlTime() {
        // Day/Night,  left alt for fast forwad, left ctrl for backforward
        float direction = Input.GetAxis("Day/Night");
        if ( direction > 0.9 || direction < -0.9) {
            _timeOfDay = _timeOfDay + direction * Time.deltaTime * _timeScale / 86400 * _changeSpeed;
            UpdataTimeScale();
            UpdateTime();
        }
    }

}
