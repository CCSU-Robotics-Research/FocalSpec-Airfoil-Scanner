**Windows Form Application with**

**GPhoto2 and  ABB Robotics Integration**

This repository contains a Windows Form Application that leverages the SSH.net to communicate witha linux system running photo2 in combination with the ABB Robotics PC SDK to manipulate a robot. The application provides a user-friendly interface for controlling the robot and managing camera operations.

**Features**

- **Robot Manipulation**: Utilize the ABB Robotics PC SDK to control and manipulate the robot.
- **Camera Control**: Use gphoto2 through an SSH connection to manage camera operations.
- **User Interface**: A Windows Form Application that provides an intuitive interface for interacting with the robot and camera.

**Project Structure**

- **Rapid.cs**: Contains commands and controls for interfacing with the robot.
- **gphoto.cs**: Manages the gphoto2 and SSH.net interfaces.

**Getting Started**

**Prerequisites**

- Windows 10 or later with WSL installed.
- ABB Robotics PC SDK.
- gphoto2 installed on a linux pc (Raspberry PI) With SSH Enabled.

**Installation**

1. Clone the repository:
2. git clone https://github.com/spydazcomputers/Robot_Photogrammetry_Inspection.git
3. Open the solution in Visual Studio.
4. Build the solution to restore the necessary packages.

**Usage**

1. Launch the Windows Form Application.
2. Use the provided interface to control the robot and manage camera operations.

**Contributing**

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

**License**

This project is licensed under the MIT License. See the LICENSE file for details.
