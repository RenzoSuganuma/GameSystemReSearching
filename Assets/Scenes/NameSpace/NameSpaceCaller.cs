using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NameSpaceFirst;

public class NameSpaceCaller : MonoBehaviour
{
    NameSpaceFunctions _nameSpaceFunc;
    NameSpaceSubFunctions _nameSpaceSubFunc;

    // Start is called before the first frame update
    void Start()
    {
        //インスタンス化
        this._nameSpaceFunc = new NameSpaceFunctions();
        this._nameSpaceSubFunc = new NameSpaceSubFunctions();
    }

    // Update is called once per frame
    void Update()
    {
        this._nameSpaceFunc.OutPutLog("NAME SPACE FUNC (1)");
        this._nameSpaceSubFunc.OutPutLog("NAME SPACE FUNC (2)");
    }
}
