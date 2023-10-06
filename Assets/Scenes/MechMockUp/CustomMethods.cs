using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DGW
{
    public class CustomMethods
    {
        /// <summary> 
        /// <para>第１引数が真の時のみ第２引数の処理を実行する </para>
        /// When 1st Argument is True, Do 2nd Arguments Process
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public void When(bool condition , Action action)
        {
            if(condition) { action(); }
        }
    }
}
