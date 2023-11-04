using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpable
{
    float Force { get; }
    void Jump(CharacterController cc);
}
