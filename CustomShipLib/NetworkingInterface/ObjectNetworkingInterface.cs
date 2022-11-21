using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using UnityEngine;
using SlateShipyard.ShipSpawner;

namespace SlateShipyard.NetworkingInterface
{
    //TODO add docs to ObjectNetworkingInterface
    public abstract class ObjectNetworkingInterface : MonoBehaviour
    {
        protected Dictionary<string, SyncableField> FieldsToSync = new();
        protected Dictionary<string, SyncableProperty> PropertiesToSync = new();
        protected Dictionary<string, NetworkableMethod> NetworkableMethods = new();

        public abstract bool IsPuppet { get; set; }
        public abstract string UniqueScriptID { get; }

        public ShipData shipData;

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

            var properties = GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                var attrs = property.GetCustomAttributes(typeof(SyncableProperty), true);
                if (attrs.Length > 0)
                {
                    SyncableProperty syncableProperty = (SyncableProperty)attrs[0];
                    if (string.IsNullOrEmpty(syncableProperty.SyncName))
                    {
                        syncableProperty.SyncName = property.Name;
                    }
                    syncableProperty.Property = property;
                    syncableProperty.Type = property.PropertyType;

                    syncableProperty.Object = this;

                    PropertiesToSync.Add(syncableProperty.SyncName, syncableProperty);
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

        public void SetValue(string memberName, object value) 
        {
            if (FieldsToSync.ContainsKey(memberName))
            {
                SyncableField field = FieldsToSync[memberName];

                if (field.Type != value.GetType())
                    throw new Exception($"The field {memberName} is of type {field.Type}, not {value.GetType()}");

                field.SetValue(value);
            }
            else if (PropertiesToSync.ContainsKey(memberName)) 
            {
                SyncableProperty property = PropertiesToSync[memberName];

                if (property.Type != value.GetType())
                    throw new Exception($"The property {memberName} is of type {property.Type}, not {value.GetType()}");

                property.SetValue(value);
            }
        }

        public object GetValue(string memberName)
        {
            if (FieldsToSync.ContainsKey(memberName))
            {
                return FieldsToSync[memberName].GetValue();
            }
            else if (PropertiesToSync.ContainsKey(memberName)) 
            {
                return PropertiesToSync[memberName].GetValue();
            }

            throw new Exception($"This class doesn't have a syncable field called {memberName}");
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

        public SyncableMember[] GetValues()
        {
            //TODO make this better
            return ((SyncableMember[])FieldsToSync.Values.ToArray()).Concat(PropertiesToSync.Values.ToArray()).ToArray();
        }
    }

    //TODO add docs to SyncableField
    public abstract class SyncableMember : Attribute
    {
        public string SyncName;
        public Type Type;
        public object Object;
        public SyncableMember()
        {
        }
        public SyncableMember(string syncName)
        {
            SyncName = syncName;
        }
        public abstract object GetValue();
        public abstract void SetValue(object value);
    }

    //TODO add docs to SyncableField
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SyncableField : SyncableMember
    {
        public FieldInfo Field;
        public SyncableField() 
        {
        }
        public SyncableField(string syncName)
        {
            SyncName = syncName;
        }
        public override object GetValue()
        {
            return Field.GetValue(Object);
        }
        public override void SetValue(object value)
        {
            Field.SetValue(Object, value);
        }
    }
    //TODO add docs to SyncableProperty
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SyncableProperty : SyncableMember
    {
        public PropertyInfo Property;
        public SyncableProperty()
        {
        }
        public SyncableProperty(string syncName)
        {
            SyncName = syncName;
        }
        public override object GetValue()
        {
            return Property.GetValue(Object);
        }
        public override void SetValue(object value)
        {
            Property.SetValue(Object, value);
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
        public NetworkableMethod()
        {
        }
        public NetworkableMethod(string networkName)
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
