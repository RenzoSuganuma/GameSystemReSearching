using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DGW
{
    public class CustomMethods
    {
        /// <summary> 
        /// <para>‘æ‚Pˆø”‚ª^‚Ì‚Ì‚İ‘æ‚Qˆø”‚Ìˆ—‚ğÀs‚·‚é </para>
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
