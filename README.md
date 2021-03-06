# Scriptable Object Architecture 

## What is SOA (Scriptable Object Architecture)?
A package for creating an scriptable object based game architecture.
This package is inspired by <a href="https://www.youtube.com/watch?v=raQ3iHhE_Kk">Ryan Hipple's Talk at Unite Austin 2017 </a>

## Features
- Variables as scriptable objects
- References that can be used within scripts to reference either a variable locally or constant
- Game Events as scriptable objects which function similar to unity events
- Game Event Listeners for listening to game events from within scripts
- Tool for generating variables, references, game events and listeners

## Installation
### Releases
Check out the <a href="https://github.com/Luxulicious/SOA/releases">release page</a> and download the package from there.

### Package Manager Installation
Simply modify your `manifest.json` file found at `/PROJECTNAME/Packages/manifest.json` and include:

```
{
	"dependencies": {
		...
		"com.theluxgames.soa": "https://github.com/Luxulicious/SOA.git#upm",
		...
	}
}
```

*Note: "#UPM" can be replaced with any version number for example: "#0.0.14"

## Credits
### Contributors
Made by <a href="https://github.com/Luxulicious/">https://github.com/Luxulicious/</a>
### Inspired by
This package is inspired by  <a href="https://www.youtube.com/watch?v=raQ3iHhE_Kk">Ryan Hipple's Talk at Unite Austin 2017</a>
And has taken ideas and concepts from:
- <a href="https://github.com/sigtrapgames/SmartData">https://github.com/sigtrapgames/SmartData</a>
- <a href="https://github.com/AdamRamberg/unity-atoms">https://github.com/AdamRamberg/unity-atoms</a>
- <a href="https://github.com/DanielEverland/ScriptableObject-Architecture">https://github.com/DanielEverland/ScriptableObject-Architecture</a>

I highly recommend giving these all a try, these are all better in some ways (and *worse* in some ways incase of my particular workflow, hence I made this package in the first place)

## Missing Features & Known Bugs
Known bugs and todo's:
- Making things more pretty

References:
- Collapsing sometimes makes a label appear twice
- References in array can collapse back-in, but can collapse out
- References that are marked as dirty by unity will show 2 labels instead of 1

Generation:
- Temporarily disabled
- Unknown namespaces also get filled in in using (ex: "using ;" which errors)
- Have generation overwriting be selective per file...
- Generation does not include new constructors in references
- UI for generation crashes sometimes when removing/adding templates
- UI throws unwarranted errors sometimes when searching/adding types
- Automate editor check
- Have dependency check build into templates

Attributes:
- Add attributes to lock designers out of readonly reference etc. For example:
["LockPersistence"], [LockScope], [HideOnChange] etc...

Events:
- Display events in a graph
- Provide stack trace for events
- Provide an easy navigatable way through events
- Game event listener components will not auto-listen correctly when used with a prefab

Composite Variables:
- Make member values be variable only
- Make member values be non-collapsable
- Make member value only show reference propertyfield and nothing else
- Make member not be self to avoid infinite recursion
- Abstract it beyond boolean

Additional features:
Stacktracing event and variable change invocation
Decorators for modular scriptable object logic
Add developer descriptions
