using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private IJumpable _jumpable;
    private CharacterController _cc;

    public CmdJump(IJumpable jumpable, CharacterController cc)
    {
        _jumpable = jumpable;
        _cc = cc;
    }

    public void Execute() => _jumpable.Jump(_cc);
}
