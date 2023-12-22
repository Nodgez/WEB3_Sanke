using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This class serve as an abstract implementation of any UI object that needs to be opened or closed
public abstract class UIItem : MonoBehaviour
{
    protected abstract void Open();
    protected abstract void Close();
}
