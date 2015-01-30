# Screenshot
![Action Screenshot](https://raw.githubusercontent.com/jeanfabre/PlayMakerCustomActions_U3/master/PlayMaker/Screenshots/Light/ActivateGameObject.png)

# Description

Activates/deactivates a Game Object. Use this to hide/show areas, or enable/disable many Behaviours at once.

## Parameters

* ####Game Object   
  The GameObject to activate/deactivate.

* ####Activate   
  Check to activate, uncheck to deactivate Game Object.


* ####Recursive   
  Recursively activate/deactivate all children

* ####Reset On Exit   
  Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.   
  **Note:** Only applies to the last Game Object activated/deactivated (won't work if Game Object changes)

* ####Every Frame   
  Repeat this action every frame. Useful if Activate changes over time

