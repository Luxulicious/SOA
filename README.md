# Scriptable Object Architecture
 
//TODO

Known bugs and todo's:
Variables:


Generation:
Optimize type search for generation.
Unknown namespaces also get filled in in using (ex: "using ;" which errors)
Exclude non-serializable classes since they cant be generated properly
Generation is rushed and could be improved a lot in general...
Have generation over writing be selective per file...

Lists:
Add reference to listvariables so they can be used from a monobehaviour

Events:
Add a standard component for event listeners
Change name of game event listener reference to be not "Unity Event SO"
Responses in game event listener doesn't have to be a list
Find a way for a game event listener to auto (un)subscribe whenever the reference is changed (probably most easily accomplished via propertydrawer)

