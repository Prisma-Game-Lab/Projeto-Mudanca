using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[Serializable]
[CreateAssetMenu]
public class PlayerInfo : ScriptableObject {
     public bool derrota =false;
     public int sceneindex;
     public float posx;
     public float posy;
     public float posz;
     public int saved;
     public List<DialogueBlock> Listadedialogos = new List<DialogueBlock>();
}
