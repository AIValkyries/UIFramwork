using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 存储变量，可以在模块之间方便的访问变量
/// </summary>
public class ModuleBlackboard : ISingletonModuleComponent
{
    /// <summary>
    /// Key:获取变量的关键key
    /// Value:值
    /// </summary>
    Dictionary<string, int> _intVariables = new Dictionary<string, int>();
    Dictionary<string, List<int>> _intsVariables = new Dictionary<string, List<int>>();

    Dictionary<string, float> _floatVariables = new Dictionary<string, float>();
    Dictionary<string, List<float>> _floatsVariables = new Dictionary<string, List<float>>();

    Dictionary<string, double> _doubleVariables = new Dictionary<string, double>();
    Dictionary<string, List<double>> _doublesVariables = new Dictionary<string, List<double>>();

    Dictionary<string, string> _stringVariables = new Dictionary<string, string>();
    Dictionary<string, List<string>> _stringsVariables = new Dictionary<string, List<string>>();

    Dictionary<string, Type> _typeVariables = new Dictionary<string, Type>();
    Dictionary<string, List<Type>> _typesVariables = new Dictionary<string, List<Type>>();

    Dictionary<string, Vector2> _vector2Variables = new Dictionary<string, Vector2>();
    Dictionary<string, List<Vector2>> _vector2sVariables = new Dictionary<string, List<Vector2>>();

    Dictionary<string, Vector3> _vector3Variables = new Dictionary<string, Vector3>();
    Dictionary<string, List<Vector3>> _vector3sVariables = new Dictionary<string, List<Vector3>>();

    Dictionary<string, Vector4> _vector4Variables = new Dictionary<string, Vector4>();
    Dictionary<string, List<Vector4>> _vector4sVariables = new Dictionary<string, List<Vector4>>();

    Dictionary<string, Quaternion> _quaternionVariables = new Dictionary<string, Quaternion>();
    Dictionary<string, List<Quaternion>> _quaternionsVariables = new Dictionary<string, List<Quaternion>>();

    Dictionary<string, Matrix4x4> _matrix4x4Variables = new Dictionary<string, Matrix4x4>();
    Dictionary<string, List<Matrix4x4>> _matrix4x4sVariables = new Dictionary<string, List<Matrix4x4>>();

    Dictionary<string, object> _objectVariables = new Dictionary<string, object>();
    //Dictionary<string, List<object>> _objectsVariables = new Dictionary<string, List<object>>();

    Dictionary<string, UnityEngine.Object> _unityObjectVariables = new Dictionary<string, UnityEngine.Object>();
    Dictionary<string, List<UnityEngine.Object>> _unityObjectsVariables = new Dictionary<string, List<UnityEngine.Object>>();

    public void Initialize()
    {
         
    }


    public void TestToString()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        //  变量
        {
            string str1 = Newtonsoft.Json.JsonConvert.SerializeObject(_intVariables);
            string str2 = Newtonsoft.Json.JsonConvert.SerializeObject(_intsVariables);

            string str3 = Newtonsoft.Json.JsonConvert.SerializeObject(_floatVariables);
            string str4 = Newtonsoft.Json.JsonConvert.SerializeObject(_floatsVariables);


            string str5 = Newtonsoft.Json.JsonConvert.SerializeObject(_doubleVariables);
            string str6 = Newtonsoft.Json.JsonConvert.SerializeObject(_doublesVariables);

            string str7 = Newtonsoft.Json.JsonConvert.SerializeObject(_stringVariables);
            string str8 = Newtonsoft.Json.JsonConvert.SerializeObject(_stringsVariables);

            string str9 = Newtonsoft.Json.JsonConvert.SerializeObject(_typeVariables);
            string str10 = Newtonsoft.Json.JsonConvert.SerializeObject(_typesVariables);

            string str11 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector2Variables);
            string str12 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector2sVariables);

            string str13 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector3Variables);
            string str14 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector3sVariables);

            string str15 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector4Variables);
            string str16 = Newtonsoft.Json.JsonConvert.SerializeObject(_vector4sVariables);


            string str17 = Newtonsoft.Json.JsonConvert.SerializeObject(_quaternionVariables);
            string str18 = Newtonsoft.Json.JsonConvert.SerializeObject(_quaternionsVariables);

            string str19 = Newtonsoft.Json.JsonConvert.SerializeObject(_matrix4x4Variables);
            string str20 = Newtonsoft.Json.JsonConvert.SerializeObject(_matrix4x4sVariables);

            //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            //解决循环引用问题
            //jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            //string str21 = Newtonsoft.Json.JsonConvert.SerializeObject(_objectVariables, jsonSerializerSettings);
            //string str22 = Newtonsoft.Json.JsonConvert.SerializeObject(_objectsVariables);

            string str23 = Newtonsoft.Json.JsonConvert.SerializeObject(_unityObjectVariables);
            string str24 = Newtonsoft.Json.JsonConvert.SerializeObject(_unityObjectsVariables);

            builder.Append("\n" + str1 + "\n");
            builder.Append(str2 + "\n");
            builder.Append(str3 + "\n");
            builder.Append(str4 + "\n");
            builder.Append(str5 + "\n");
            builder.Append(str6 + "\n");
            builder.Append(str7 + "\n");
            builder.Append(str8 + "\n");
            builder.Append(str9 + "\n");
            builder.Append(str10 + "\n");
            builder.Append(str11 + "\n");
            builder.Append(str12 + "\n");
            builder.Append(str13 + "\n");
            builder.Append(str14 + "\n");
            builder.Append(str15 + "\n");
            builder.Append(str16 + "\n");
            builder.Append(str17 + "\n");
            builder.Append(str18 + "\n");
            builder.Append(str19 + "\n");
            builder.Append(str20 + "\n");
            //builder.Append(str21);
            //builder.Append(str22);
            builder.Append(str23 + "\n");
            builder.Append(str24 + "\n");

            Debug.Log("ModuleBlackboard=" + builder.ToString());
        }
    }

    #region add

    public void AddVariables(string key, int variable)
    {
        if (!_intVariables.ContainsKey(key))
            _intVariables.Add(key, variable);
        _intVariables[key] = variable;
    }

    public void AddVariables(string key, List<int> variable)
    {
        if (!_intsVariables.ContainsKey(key))
            _intsVariables.Add(key, variable);
        _intsVariables[key] = variable;
    }

    public void AddVariables(string key, float variable)
    {
        if (!_floatVariables.ContainsKey(key))
            _floatVariables.Add(key, variable);
        _floatVariables[key] = variable;
    }

    public void AddVariables(string key, List<float> variable)
    {
        if (!_floatsVariables.ContainsKey(key))
            _floatsVariables.Add(key, variable);
        _floatsVariables[key] = variable;
    }

    public void AddVariables(string key, double variable)
    {
        if (!_doubleVariables.ContainsKey(key))
            _doubleVariables.Add(key, variable);
        _doubleVariables[key] = variable;
    }

    public void AddVariables(string key, List<double> variable)
    {
        if (!_doublesVariables.ContainsKey(key))
            _doublesVariables.Add(key, variable);
        _doublesVariables[key] = variable;
    }

    public void AddVariables(string key, string variable)
    {
        if (!_stringVariables.ContainsKey(key))
            _stringVariables.Add(key, variable);
        _stringVariables[key] = variable;
    }

    public void AddVariables(string key, List<string> variable)
    {
        if (!_stringsVariables.ContainsKey(key))
            _stringsVariables.Add(key, variable);
        _stringsVariables[key] = variable;
    }

    public void AddVariables(string key, Type variable)
    {
        if (!_typeVariables.ContainsKey(key))
            _typeVariables.Add(key, variable);
        _typeVariables[key] = variable;
    }


    public void AddVariables(string key, List<Type> variable)
    {
        if (!_typesVariables.ContainsKey(key))
            _typesVariables.Add(key, variable);
        _typesVariables[key] = variable;
    }

    public void AddVariables(string key, Vector2 variable)
    {
        if (!_vector2Variables.ContainsKey(key))
            _vector2Variables.Add(key, variable);
        _vector2Variables[key] = variable;
    }

    public void AddVariables(string key, List<Vector2> variable)
    {
        if (!_vector2sVariables.ContainsKey(key))
            _vector2sVariables.Add(key, variable);
        _vector2sVariables[key] = variable;
    }

    public void AddVariables(string key, Vector3 variable)
    {
        if (!_vector3Variables.ContainsKey(key))
            _vector3Variables.Add(key, variable);
        _vector3Variables[key] = variable;
    }

    public void AddVariables(string key, List<Vector3> variable)
    {
        if (!_vector3sVariables.ContainsKey(key))
            _vector3sVariables.Add(key, variable);
        _vector3sVariables[key] = variable;
    }

    public void AddVariables(string key, Vector4 variable)
    {
        if (!_vector4Variables.ContainsKey(key))
            _vector4Variables.Add(key, variable);
        _vector4Variables[key] = variable;
    }

    public void AddVariables(string key, List<Vector4> variable)
    {
        if (!_vector4sVariables.ContainsKey(key))
            _vector4sVariables.Add(key, variable);
        _vector4sVariables[key] = variable;
    }

    public void AddVariables(string key, Quaternion variable)
    {
        if (!_quaternionVariables.ContainsKey(key))
            _quaternionVariables.Add(key, variable);
        _quaternionVariables[key] = variable;
    }

    public void AddVariables(string key, List<Quaternion> variable)
    {
        if (!_quaternionsVariables.ContainsKey(key))
            _quaternionsVariables.Add(key, variable);
        _quaternionsVariables[key] = variable;
    }

    public void AddVariables(string key, Matrix4x4 variable)
    {
        if (!_matrix4x4Variables.ContainsKey(key))
            _matrix4x4Variables.Add(key, variable);
        _matrix4x4Variables[key] = variable;
    }

    public void AddVariables(string key, List<Matrix4x4> variable)
    {
        if (!_matrix4x4sVariables.ContainsKey(key))
            _matrix4x4sVariables.Add(key, variable);
        _matrix4x4sVariables[key] = variable;
    }

    public void AddVariables(string key, object variable)
    {
        if (!_objectVariables.ContainsKey(key))
            _objectVariables.Add(key, variable);
        _objectVariables[key] = variable;
    }

    public void AddVariables(string key, UnityEngine.Object variable)
    {
        if (!_unityObjectVariables.ContainsKey(key))
            _unityObjectVariables.Add(key, variable);
        _unityObjectVariables[key] = variable;
    }

    //public void AddVariables(string key, List<UnityEngine.Object> variable)
    //{
    //    if (!_unityObjectsVariables.ContainsKey(key))
    //        _unityObjectsVariables.Add(key, variable);
    //    _unityObjectsVariables[key] = variable;
    //}

    #endregion

    #region remove

    public void RemoveIntVariables(string key)
    {
        if (_intVariables.ContainsKey(key))
            _intVariables.Remove(key);
    }

    public void RemoveIntsVariables(string key)
    {
        if (_intsVariables.ContainsKey(key))
            _intsVariables.Remove(key);
    }

    public void RemoveFloatVariables(string key)
    {
        if (_floatVariables.ContainsKey(key))
            _floatVariables.Remove(key);
    }

    public void RemoveFloatsVariables(string key)
    {
        if (_floatsVariables.ContainsKey(key))
            _floatsVariables.Remove(key);
    }

    public void RemovedoubleVariables(string key)
    {
        if (_doubleVariables.ContainsKey(key))
            _doubleVariables.Remove(key);
    }

    public void RemovedoublesVariables(string key)
    {
        if (_doublesVariables.ContainsKey(key))
            _doublesVariables.Remove(key);
    }

    public void RemoveStringVariables(string key)
    {
        if (_stringVariables.ContainsKey(key))
            _stringVariables.Remove(key);
    }

    public void RemoveStringsVariables(string key)
    {
        if (_stringsVariables.ContainsKey(key))
            _stringsVariables.Remove(key);
    }

    public void RemoveTypeVariables(string key)
    {
        if (_typeVariables.ContainsKey(key))
            _typeVariables.Remove(key);
    }

    public void RemoveTypesVariables(string key)
    {
        if (_typesVariables.ContainsKey(key))
            _typesVariables.Remove(key);
    }

    public void RemoveVector2Variables(string key)
    {
        if (_vector2Variables.ContainsKey(key))
            _vector2Variables.Remove(key);
    }

    public void RemoveVector2sVariables(string key)
    {
        if (_vector2sVariables.ContainsKey(key))
            _vector2sVariables.Remove(key);
    }

    public void RemoveVector3Variables(string key)
    {
        if (_vector3Variables.ContainsKey(key))
            _vector3Variables.Remove(key);
    }

    public void RemoveVector3sVariables(string key)
    {
        if (_vector3sVariables.ContainsKey(key))
            _vector3sVariables.Remove(key);
    }

    public void RemoveVector4Variables(string key)
    {
        if (_vector4Variables.ContainsKey(key))
            _vector4Variables.Remove(key);
    }

    public void RemoveVector4sVariables(string key)
    {
        if (_vector4sVariables.ContainsKey(key))
            _vector4sVariables.Remove(key);
    }


    public void RemoveQuaternionVariables(string key)
    {
        if (_quaternionVariables.ContainsKey(key))
            _quaternionVariables.Remove(key);
    }

    public void RemoveQuaternionsVariables(string key)
    {
        if (_quaternionsVariables.ContainsKey(key))
            _quaternionsVariables.Remove(key);
    }

    public void RemoveMatrix4x4Variables(string key)
    {
        if (_matrix4x4Variables.ContainsKey(key))
            _matrix4x4Variables.Remove(key);
    }

    public void RemoveMatrix4x4sVariables(string key)
    {
        if (_matrix4x4sVariables.ContainsKey(key))
            _matrix4x4sVariables.Remove(key);
    }

    public void RemoveobjectVariables(string key)
    {
        if (_objectVariables.ContainsKey(key))
            _objectVariables.Remove(key);
    }

    //public void RemoveobjectsVariables(string key)
    //{
    //    if (_objectsVariables.ContainsKey(key))
    //        _objectsVariables.Remove(key);
    //}

    public void RemoveObjectsVariables(string key)
    {
        if (_unityObjectVariables.ContainsKey(key))
            _unityObjectVariables.Remove(key);
    }

    public void RemoveObjectssVariables(string key)
    {
        if (_unityObjectsVariables.ContainsKey(key))
            _unityObjectsVariables.Remove(key);
    }

    #endregion

    #region get

    public int GetIntVariables(string key)
    {
        if (_intVariables.ContainsKey(key))
        {
            return _intVariables[key];
        }
        return 0;
    }

    public List<int> GetIntsVariables(string key)
    {
        if (_intsVariables.ContainsKey(key))
        {
            return _intsVariables[key];
        }
        return null;
    }

    public float GetFloatVariables(string key)
    {
        if (_floatVariables.ContainsKey(key))
        {
            return _floatVariables[key];
        }
        return 0;
    }

    public List<float> GetFloatsVariables(string key)
    {
        if (_floatsVariables.ContainsKey(key))
        {
            return _floatsVariables[key];
        }
        return null;
    }

    public double GetDoubleVariables(string key)
    {
        if (_doubleVariables.ContainsKey(key))
        {
            return _doubleVariables[key];
        }
        return 0;
    }

    public List<double> GetDoublesVariables(string key)
    {
        if (_doublesVariables.ContainsKey(key))
        {
            return _doublesVariables[key];
        }
        return null;
    }

    public string GetStringVariables(string key)
    {
        if (_stringVariables.ContainsKey(key))
        {
            return _stringVariables[key];
        }
        return null;
    }

    public List<string> GetStringsVariables(string key)
    {
        if (_stringsVariables.ContainsKey(key))
        {
            return _stringsVariables[key];
        }
        return null;
    }

    public Type GetTypeVariables(string key)
    {
        if (_typeVariables.ContainsKey(key))
        {
            return _typeVariables[key];
        }
        return null;
    }

    public List<Type> GetTypesVariables(string key)
    {
        if (_typesVariables.ContainsKey(key))
        {
            return _typesVariables[key];
        }
        return null;
    }

    public Vector2 GetVector2Variables(string key)
    {
        if (_vector2Variables.ContainsKey(key))
        {
            return _vector2Variables[key];
        }
        return Vector2.zero;
    }

    public List<Vector2> GetVector2sVariables(string key)
    {
        if (_vector2sVariables.ContainsKey(key))
        {
            return _vector2sVariables[key];
        }
        return null;
    }

    public Vector3 GetVector3Variables(string key)
    {
        if (_vector3Variables.ContainsKey(key))
        {
            return _vector3Variables[key];
        }
        return Vector3.zero;
    }

    public List<Vector3> GetVector3sVariables(string key)
    {
        if (_vector3sVariables.ContainsKey(key))
        {
            return _vector3sVariables[key];
        }
        return null;
    }

    public Vector4 GetVector4Variables(string key)
    {
        if (_vector4Variables.ContainsKey(key))
        {
            return _vector4Variables[key];
        }
        return Vector4.zero;
    }

    public List<Vector4> GetVector4sVariables(string key)
    {
        if (_vector4sVariables.ContainsKey(key))
        {
            return _vector4sVariables[key];
        }
        return null;
    }


    public Quaternion GetQuaternionVariables(string key)
    {
        if (_quaternionVariables.ContainsKey(key))
        {
            return _quaternionVariables[key];
        }
        return Quaternion.identity;
    }


    public List<Quaternion> GetQuaternionsVariables(string key)
    {
        if (_quaternionsVariables.ContainsKey(key))
        {
            return _quaternionsVariables[key];
        }
        return null;
    }

    public Matrix4x4 GetMatrix4x4Variables(string key)
    {
        if (_matrix4x4Variables.ContainsKey(key))
        {
            return _matrix4x4Variables[key];
        }
        return Matrix4x4.identity;
    }

    public List<Matrix4x4> GetMatrix4x4sVariables(string key)
    {
        if (_matrix4x4sVariables.ContainsKey(key))
        {
            return _matrix4x4sVariables[key];
        }
        return null;
    }

    public object GetobjectVariables(string key)
    {
        if (_objectVariables.ContainsKey(key))
        {
            return _objectVariables[key];
        }
        return null;
    }

    public UnityEngine.Object GetUnityObjectVariables(string key)
    {
        if (_unityObjectVariables.ContainsKey(key))
        {
            return _unityObjectVariables[key];
        }
        return null;
    }

    public List<UnityEngine.Object> GetUnityObjectsVariables(string key)
    {
        if (_unityObjectsVariables.ContainsKey(key))
        {
            return _unityObjectsVariables[key];
        }
        return null;
    }

    #endregion

}
