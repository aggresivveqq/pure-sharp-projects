The Task Manager is a simple console-based application designed to manage tasks. It allows users to perform various operations such as adding, viewing, and removing tasks. Each task has details such as a title, description, creation and update timestamps, due time, and progress status.
# Features
- Add Tasks: Users can create tasks by providing a title, description, and due time (in days).
- View Tasks: Users can view all tasks currently stored in the application.
- Remove Tasks: Users can remove tasks by providing the task's ID.
- Task Progress: Allows updating the task's progress status (Done, In Progress, Undone).


# Requirements
- .NET 5.0 or later
- Newtonsoft.Json library (for JSON handling)
- Console-based user interface

#Setup 
- Clone this repository to your local machine.
- Navigate to the project directory.
- Run the application.

# Folder Structure
- TaskManager.app: The main application that handles user interaction.
- TaskManager.data: Contains the logic to manage tasks, including file reading and writing for persistence.
- TaskManager.model: Defines the data structures and enums used in the application.
# Dependencies
- Newtonsoft.Json: This library is used for serializing and deserializing task data to and from JSON format.
- Via NuGet :dotnet add package Newtonsoft.Json
