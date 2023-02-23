using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Apply : MonoBehaviour
{
    [SerializeField] SettingSaver settingSaver;
    [SerializeField] Transform optionsHolder;

    public List<float> GetMidRangeData()
    {
        List<float> data = new List<float>();

        foreach (Transform holder in optionsHolder)
        {
            foreach (Transform setting in holder)
            {
                Slider s = setting.GetComponent<Slider>();
                float v = (s.minValue + s.maxValue) / 2;
                data.Add(v);
            }
        }

        return data;
    }

    List<float> GetData()
    {
        List<float> data = new List<float>();

        foreach (Transform holder in optionsHolder)
        {
            foreach (Transform setting in holder)
            {
                data.Add(setting.GetComponent<Setting>().value);
            }
        }

        for (int i = 0; i < data.Count; i++)
        {
            data[i] = (float)Math.Round((double)data[i], 2);
        }

        return data;
    }

    public void SetSettings(List<float> data)
    {
        int index = 0;
        foreach (Transform holder in optionsHolder)
        {
            foreach (Transform setting in holder)
            {
                setting.GetComponent<Setting>().value = data[index];
                setting.GetComponent<Setting>().UpdateUI();
                index++;
            }
        }
    }
    public void ApplySettings()
    {
        settingSaver.SaveOptionsToFile(GetData());
        if (settingSaver.inGame)
        {
            ApplyOptionsToWorld();
            settingSaver.transform.parent.GetComponent<Menu>().OpenCloseSwitch();
        }

        settingSaver.GetComponent<PlayerOptions>().CloseOptions();
    }

    public void ApplyOptionsToWorld()
    {
        List<float> data = GetData();
        Game.Get().Player.GetComponent<Look>().rotationSpeed = data[0];

        FMOD.Studio.Bus Master = FMODUnity.RuntimeManager.GetBus("bus:/MusicMaster");
        Master.setVolume(data[1]);

        FMOD.Studio.Bus SFX = FMODUnity.RuntimeManager.GetBus("bus:/SoundMaster");
        SFX.setVolume(data[2]);
    }
}
