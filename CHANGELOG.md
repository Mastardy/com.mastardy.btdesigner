# Changelog

All changes to this package will be documented in here. The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)

## [1.1.0] - 24/11/2022

### Added

- Now you can open the window from an asset in Unity Project's window

### Changed

- Changed the GUI for the nodes in the BTDesigner window


## [1.0.0] - 18/11/2022

### Added

- Implemented a basic GUI for the Behavior Tree under BTDesigner/Behavior Tree Designer

### Fixed

- Fixed Editor Assembly Definition Root Namespace to BTDesigner instead of Mastardy

### Changed

- Renamed BTDesign to BTDesigner in the Components menu and in the Assets Menu

## [0.1.0] - 18/11/2022

### Added

- Implemented BehaviorTreeDesign for holding all the nodes and the Behavior Tree logic
- Implemented Node as the base class for all the nodes
- Implemented RootNode as the start node for the Behavior Tree
- Implemented ActionNode as the base class for Nodes responsible for changing something in the Game World
- Implemented CompositionNode as the base class for Nodes responsible for managing the flow of the Behavior Tree
- Implemented UtilityNode as the base class for Nodes responsible for helping with the development of the Behavior Tree
- Implemented a few examples for each type of Node: WaitLog, Sequencer and Repeater.

## [0.0.1] - 17/11/2022

### Added

- package.json file for ready-to-use Unity Package
- CHANGELOG.md file for keeping package changes documented
- LICENSE.md file for the license
- README.md file for introducing people into the Package