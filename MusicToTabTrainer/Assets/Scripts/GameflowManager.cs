using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class GameflowManager 
{
    List<string> _tokens = new List<string>();

    public static implicit operator bool(GameflowManager obj) => obj == null ? false : true;

    public async Task UpdateConditional(System.Action opertaion,
                                           System.Func<bool> condion,
                                            int delay = 10,
                                            [CallerMemberName] string callerName = "",
                                            [CallerLineNumber] int lineNum = 0)
    {
        var id = $"{callerName}{lineNum}";
        if(!_tokens.Contains(id))
        {
            _tokens.Add(id);
            while (condion.Invoke())
            {
                opertaion.Invoke();
                await Task.Delay(delay);
            }
            _tokens.Remove(id);
        }
    }


    //public IEnumerator UpdateConditionalCoroutine(System.Action opertaion,
    //                                   System.Func<bool> condion,
    //                                    int delay = 3,
    //                                    [CallerMemberName] string callerName = "",
    //                                    [CallerLineNumber] int lineNum = 0)
    //{
    //    var id = $"{callerName}{lineNum}";
    //    if (!_tokens.Contains(id))
    //    {
    //        _tokens.Add(id);
    //        while (condion.Invoke())
    //        {
    //            opertaion.Invoke();
    //            yield return new WaitForSeconds(delay);
    //        }
    //        _tokens.Remove(id);
    //    }
    //}

    //Aufgabe 1. Implementiere eine conditional update für die bewegung des balls, 2. füge einen zweiten delay vor der while ein der die while erst starten lässt wenn die musik zu spielen beginnt

}
