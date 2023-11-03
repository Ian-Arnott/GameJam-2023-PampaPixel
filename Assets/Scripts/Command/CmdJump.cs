using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private IJumpable _jumpable;
    private Rigidbody _rb;

    public CmdJump(IJumpable jumpable, Rigidbody rb)
    {
        _jumpable = jumpable;
        _rb = rb;
    }

    public void Execute() => _jumpable.Jump(_rb);
}
