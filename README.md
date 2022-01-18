<h1 align='center'>
<img src='/ReadmeAssets/MapEditorTitle.png' alt='AMAZEING'/>
</h1>

<h3 align='center'>
Console labyrinth project for school
</h3>

<p align='center'>
<img src='/ReadmeAssets/MapEditorScreenShot.png'/>
</p>

# Summary
* [Requirements](#requirements)
* [Quickstart](#quickstart)
* [Project overview](#projectOverview)
* [Project features](#projectFeatures)
* [Remarks](#remarks)

## <a name='requirements'>Requirements</a>
C# version 8.0

Unity version 2020.3.19.f1

*Note: This is the version of school computers, it may run on lower one.*

## <a name='quickstart'>Quickstart</a>

command:

```bash
$ git clone "https://github.com/LeoKaiser2000/Unity-Map-Editor.git"
```
Clone the project and open it with unity.

## <a name='projectOverview'>Project overview</a>

This project is a unity EditorWindow of a Map Editor and a Map Inspector.

### Map Editor
The Map Editor is used to edit a map. It is split in 3 parts

* The loading part
* The map management part
* The map properties part, which contain map edition


### Map Inspector
The Map Inspector is a map visualizer working with generic reflection. It is split in 2 parts
* The loading part
* The map visualizing part


## <a name='projectFeatures'>Project features</a>

### Mandatory

* Map Editor (1 point)
  * EditorWindow ✓
  * Add additional nodes to ANY NODE ✓
  * Connected nodes (0.5 point)
    * North ✓
    * South ✓
    * West ✓
    * East ✓
  * Other nodes data
    * Name ✓
    * Descrption ✓
    * Is Starting Node ✓
  * Buttons (0.5 point)
    * Save ✓
    * Load ✓


* Map Inspector (1 point)
  * EditorWindow ✓
  * Pick a map file from a file picker ✓
  * Display nodes
    * Name ✓
    * Descrption ✓
    * Is Starting Node ✓
    * North node name ✓
    * South node name ✓
    * West node name ✓
    * East node name ✓
  * Button loading a map and logs it using reflection 10000 times ✓


* Reflexion Acceptability (1 point)
  * Comment in code
    * Is it acceptable to use reflection in runtime of the games? ✓
    * Would you use it at runtime? ✓
    * Why would / would’t you use it in runtime? ✓


## <a name='remarks'>Remarks</a>

*Note: I made it on a Linux platform, but check compilation on windows too.*

*If you have any trouble, please contact me.*