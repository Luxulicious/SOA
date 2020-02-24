# Scriptable Object Architecture
 
//TODO

Known bugs and todo's:
Variables:

Reference:

Generation:
Unknown namespaces also get filled in in using (ex: "using ;" which errors)
Have generation overwriting be selective per file...
Generation does not include new constructors in references
UI for generation crashes sometimes when removing/adding templates
UI throws unwarranted errors sometimes when searching/adding types
Automate editor check
Have dependency check build into templates

Lists:
Add reference to listvariables so they can be used from a monobehaviour

Events:
Add a standard component for event listeners
Find a way for a game event listener to auto (un)subscribe whenever the reference is changed (probably most easily accomplished via propertydrawer)

Packaging:

Additional features:
Stacktracing event and variable change invocation
Finding all references shortcut for game events and variables
Decorators for modular scriptable object logic