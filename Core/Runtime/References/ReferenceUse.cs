using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    public class ReferenceUse
    {
        private IRegisteredReferenceContainer _container;
        private ContainerType _containerType;
        private HashSet<IRegisteredReference> _references = new HashSet<IRegisteredReference>();

        public ReferenceUse(IRegisteredReferenceContainer container, HashSet<IRegisteredReference> references,
            ContainerType containerType)
        {
            this._container = container;
            this._references = references;
            this._containerType = containerType;
        }

        public IRegisteredReferenceContainer Container
        {
            get => _container;
            set => _container = value;
        }

        public ContainerType ContainerType
        {
            get => _containerType;
            set => _containerType = value;
        }

        public HashSet<IRegisteredReference> References
        {
            get => _references;
            set => _references = value;
        }

        public void AddReference(IRegisteredReference reference)
        {
            if (_references.Contains(reference))
                return;
            _references.Add(reference);
        }

        public void AddReferences(IRegisteredReference[] references)
        {
            foreach (var reference in references)
                AddReference(reference);
        }

        public void RemoveReference(IRegisteredReference reference)
        {
            if (!_references.Contains(reference))
                return;
            _references.Remove(reference);
        }
    }

    public enum ContainerType
    {
        PrefabComponent,
        NonPrefabComponent,
        ScriptableObject,
        Other
    }

    public static class IRegisteredReferenceContainerExtensions
    {
        public static ContainerType GetContainerType(this IRegisteredReferenceContainer use)
        {
            ContainerType containerType;
            try
            {
                if (use is Component pc && pc.gameObject.scene.rootCount == 0)
                    containerType = ContainerType.PrefabComponent;
                else if (use is Component npc && npc.gameObject.scene.rootCount != 0)
                    containerType = ContainerType.NonPrefabComponent;
                else if (use is ScriptableObject)
                    containerType = ContainerType.ScriptableObject;
                else
                    containerType = ContainerType.Other;
            }
            catch (UnityException)
            {
                containerType = ContainerType.Other;
            }
            catch (MissingReferenceException)
            {
                containerType = ContainerType.Other;
            }

            return containerType;
        }
    }

    public class ReferenceUses
    {
        private List<ReferenceUse> _uses = new List<ReferenceUse>();

        public List<ReferenceUse> Uses => _uses;

        public void Clear()
        {
            _uses?.Clear();
        }

        public void Add(IRegisteredReferenceContainer container, IRegisteredReference reference)
        {
            //If collection is empty
            var item = new ReferenceUse
            (
                container,
                new HashSet<IRegisteredReference>() {reference},
                container.GetContainerType()
            );
            if (!_uses.Any())
                //Add new use
                _uses.Add(item);
            //Else if this container is not yet in collection
            else if (_uses.All(x => x.Container as Object != container as Object))
                //Add new use
                _uses.Add(item);
            //Else if this container is in collection but none of them match type
            else if (_uses.Where(x => x.Container as Object == container as Object)
                    .All(x => x.Container.GetContainerType() != container.GetContainerType()))
                //Add new use
                _uses.Add(item);
            else
                //Increment references in collection and of the type
                _uses.FirstOrDefault(x =>
                        x.Container == container &&
                        x.Container.GetContainerType() == container.GetContainerType())?
                    .References.Add(reference);
        }

        public bool Remove(IRegisteredReferenceContainer container, IRegisteredReference reference)
        {
            var result = false;
            //TODO Invert these first few if-statements that are empty
            if (!_uses.Any())
            {
            }
            //If this container is not yet in collection
            else if
            (
                _uses.All(x =>
                    x.Container != container)
            )
            {
            }
            //If this container is in collection but none of them match type
            else if
            (
                _uses.Where(x =>
                    x.Container == container).Any(x =>
                    x.Container.GetContainerType() != container.GetContainerType())
            )
            {
            }
            else
            {
                //Decrement references in collection and of the type
                var referenceRemoved = _uses.First(x =>
                    x.Container == container
                    && x.Container.GetContainerType() == container.GetContainerType()).References.Remove(reference);
                if (referenceRemoved)
                {
                    //Remove uses without references in them
                    var usesToKeep = new List<ReferenceUse>();
                    foreach (var use in _uses)
                        if (use.References.Any(x => x != null))
                            usesToKeep.Add(use);

                    _uses = usesToKeep;
                }

                result = referenceRemoved;
            }

            RemoveInvalidUses();
            return result;
        }

        public void RemoveInvalidUses()
        {
            var uses = new List<ReferenceUse>();
            foreach (var use in _uses)
            {
                if (use?.Container != null && ((use.Container as Object) != null && use.References.Any()))
                        uses.Add(use);
            }
            _uses = uses;
        }
    }
}