using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace SlateShipyard.NetworkingInterface
{
    //TODO add docs to ObjectNetworkingInterface
    public abstract class ObjectNetworkingInterface : MonoBehaviour
    {
        protected Dictionary<string, SyncableField> FieldsToSync = new();
        protected Dictionary<string, NetworkableMethod> NetworkableMethods = new();

        public abstract bool IsPuppet { get; set; }

        public virtual void Awake() 
        {
            var fields = GetType().GetFields();

            for(int i = 0; i < fields.Length; i++) 
            {
                FieldInfo field = fields[i];
                var attrs = field.GetCustomAttributes(typeof(SyncableField), true);
                if (attrs.Length>0)
                {
                    SyncableField syncableField = (SyncableField)attrs[0];
                    if (string.IsNullOrEmpty(syncableField.SyncName)) 
                    {
                        syncableField.SyncName = field.Name;
                    }
                    syncableField.Field = field;
                    syncableField.Type = field.FieldType;

                    syncableField.Object = this;

                    FieldsToSync.Add(syncableField.SyncName, syncableField);
                }
            }

            var methods = GetType().GetMethods();

            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                var attrs = method.GetCustomAttributes(typeof(NetworkableMethod), true);
                if (attrs.Length > 0)
                {
                    NetworkableMethod networkableMethod = (NetworkableMethod)attrs[0];
                    if (string.IsNullOrEmpty(networkableMethod.NetworkName))
                    {
                        networkableMethod.NetworkName = method.Name;
                    }
                    networkableMethod.SetMethod(method);
                    networkableMethod.Object = this;

                    NetworkableMethods.Add(networkableMethod.NetworkName, networkableMethod);
                }
            }
        }

        public void SetValue(string fieldName, object value) 
        {
            if (!FieldsToSync.ContainsKey(fieldName))
                throw new Exception($"This class doesn't have a syncable field called {fieldName}");

            SyncableField field = FieldsToSync[fieldName];

            if (field.Type != value.GetType())
                throw new Exception($"The field {fieldName} is of type {field.Type}, not {value.GetType()}");

            field.SetValue(fieldName);
        }

        public object GetValue(string fieldName)
        {
            if (!FieldsToSync.ContainsKey(fieldName))
                throw new Exception($"This class doesn't have a syncable field called {fieldName}");

            return FieldsToSync[fieldName].GetValue();
        }

        public void InvokeMethod(string methodName, object[] parameters, out bool hasReturnValue, out object returnValue)
        {
            if (!FieldsToSync.ContainsKey(methodName))
                throw new Exception($"This class doesn't have a networkable method called {methodName}");

            NetworkableMethod method = NetworkableMethods[methodName];

            if (!method.AreParametersAcceptable(parameters))
                throw new Exception($"The types on the passed parameters don't match the method ({methodName}) parameter types");

            method.InvokeMethod(parameters, out hasReturnValue, out returnValue);
        }
    }

    //TODO add docs to SyncableField
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SyncableField : Attribute
    {
        public string SyncName;
        public Type Type;
        public FieldInfo Field;
        public object Object;
        SyncableField() 
        {
        }
        SyncableField(string syncName)
        {
            SyncName = syncName;
        }
        public object GetValue()
        {
            return Field.GetValue(Object);
        }
        public void SetValue(object value)
        {
            Field.SetValue(Object, value);
        }
    }
    //TODO add docs to NetworkableMethod

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NetworkableMethod : Attribute
    {
        public string NetworkName;
        public ParameterInfo[] ParameterTypes;
        public MethodInfo Method;
        public object Object;
        NetworkableMethod()
        {
        }
        NetworkableMethod(string networkName)
        {
            NetworkName = networkName;
        }
        public void SetMethod(MethodInfo method) 
        {
            Method = method;
            ParameterTypes = method.GetParameters();
        }
        public bool AreParametersAcceptable(object[] parameters)
        {
            bool isAcceptable = true;

            int i;
            for (i = 0; i < ParameterTypes.Length && i < parameters.Length; i++) 
            {
                if (ParameterTypes[i].ParameterType.IsEquivalentTo(parameters[i].GetType())) 
                {
                    isAcceptable = false;
                    break; 
                }
            }
            if(i + 1 < ParameterTypes.Length && !ParameterTypes[i + 1].IsOptional) 
            {
                isAcceptable = false;
            }

            return isAcceptable;
        }
        public void InvokeMethod(object[] parameters, out bool hasReturnValue, out object returnValue)
        {
            returnValue = Method.Invoke(Object, parameters);            
            hasReturnValue = returnValue != null;
        }
    }
}
