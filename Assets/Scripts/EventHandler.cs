using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public Action OnEpisodeReset;
    public Action OnEpisodeEnd;
    public Action OnForeignObjectDestroy;
    public Action OnCharacterDestroy;
}
