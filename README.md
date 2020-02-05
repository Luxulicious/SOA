# Scriptable Object Architecture
 
//TODO

Known bugs and todo's:
Variables:

Reference:
OnChange and OnChangeWithHistory auto-listeners don't seem to work, 
ISerializationCallbackReceiver doesn't seem to keep the reference once entering playmode most likely
Add more constructors in base reference

Generation:
Optimize type search for generation.
Unknown namespaces also get filled in in using (ex: "using ;" which errors)
Exclude non-serializable classes since they cant be generated properly
Generation is rushed and could be improved a lot in general...
Have generation over writing be selective per file...
Generation uses old UnitySO name
Generation does not include new constructors in references
Exclude monobehaviours from generation

Lists:
Add reference to listvariables so they can be used from a monobehaviour

Events:
Add a standard component for event listeners
Find a way for a game event listener to auto (un)subscribe whenever the reference is changed (probably most easily accomplished via propertydrawer)

Packaging:
Exclude test project from package but include it in the repository

Additional features:
Stacktracing event and variable change invocation
Finding all references shortcut for game events and variables