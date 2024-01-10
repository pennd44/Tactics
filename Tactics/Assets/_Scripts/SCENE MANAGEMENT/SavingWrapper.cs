using System;
using System.Collections;
using System.Collections.Generic;
using Tactics.Saving;
using UnityEngine;

namespace Tactics.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if ( Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }
        }

        private void Save()
        {
           GetComponent<JsonSavingSystem>().Save(defaultSaveFile);
        }

        private void Load()
        {
            GetComponent<JsonSavingSystem>().Load(defaultSaveFile);
        }
    }
}
