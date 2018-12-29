using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;

namespace TQ_DirectX9_Engine_3_0.ComponentsPart
{
    public sealed class ComponentObject : NullBool
    {
        List<Component> components = new List<Component>();

        List<Component> orderToStart = new List<Component>();

        public string name { get; set; }

        public ComponentObject(string _name)
        {
            name = _name;
            AddComponent<Transform>();
        }

        public T AddComponent <T> ()
        {
            T t = (T)typeof(T).GetConstructor(new Type[0]).Invoke(new object[0]);
            
            if (t is Component)
            {
                Component c = t as Component;
                c.InitComponent(this);
                orderToStart.Add(c);
                components.Add(c);
            }
            return t;
        }
        public Component GetComponent<T>()
        {
            Component r = components.FirstOrDefault((Component c) => c is T);
            return r;
        }
        public void RemoveComponent <T> ()
        {
            Component r = GetComponent<T>();
            r.OnDestroy();
            components.Remove(r);
            
        }
        public void Update()
        {
            foreach (var o in orderToStart)
            {
                if (o)
                {
                    o.Start();
                }
                orderToStart.Remove(o);
            }
            foreach (var c in components)
            {
                c.Update();
            }
        }
    }
    public abstract class NullBool
    {
        public static implicit operator bool (NullBool nb)
        {
            return nb != null;
        }
    }
    public abstract class Component : NullBool
    {
        public Transform transform
        {
            get
            {
                trans = trans ? trans : (Transform)GetComponent<Transform>();
                return trans;
            }
        }
        Transform trans;

        public static List<Component> allComponents
        {
            get
            {
                return _all;
            }
        }

        static List<Component> _all = new List<Component>();

        public ComponentObject componentObject { get; private set; }

        public Component GetComponent<T>()
        {
            return componentObject.GetComponent<T>();
        }

        public static T FindOfType <T> ()
        {
            return FindComponentsOfType<T>().FirstOrDefault();
        }
        public static T[] FindComponentsOfType <T> ()
        {
            return allComponents.Where((Component c) => c is T).Cast<T>().ToArray();
        }

        public Component()
        {
            Init();
        }

        public void InitComponent(ComponentObject obj)
        {
            componentObject = obj;
        }

        void Init()
        {
            allComponents.Add(this);
        }
        void Destroy()
        {
            allComponents.Remove(this);
        }

        public virtual void Start()
        {
            Init();
        }
        public virtual void Update()
        {
            // update
        }
        public virtual void CameraUpdate(d3d.Device device)
        {
            // camera update
        }
        public virtual void OnDestroy()
        {
            Destroy();
        }
    }
}