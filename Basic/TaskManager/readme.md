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

# Setup 
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

# Usage
Console menu!
[Screenshot](https://github.com/user-attachments/assets/a367b43e-8a4a-41cd-b81a-d4594232948c)
# Add Task
- Enter the title for the task.
- Enter a description.
- Enter the due time in days (a number).
- Choose the progress status (Done, In Progress, Undone).
[Screenshot 2](https://github.com/user-attachments/assets/98975cdd-069e-41f2-916d-1bb7516e3fbe)


# View All Tasks
- Displays all tasks with their details: ID, title, description, and progress.
[Screenshot 3](https://github.com/user-attachments/assets/317de231-ec76-405d-89d7-d18404077ad0)


# Remove Task
- Enter the task ID to remove the task from the list.
[Screenshot 4](https://github.com/user-attachments/assets/0a468671-dff0-4d5d-ada4-3e464ae57068)

# Update Task Progress
- You can update a task's progress status using the available progress options: Done, In Progress, and Undone.
![Screenshot 5](https://github.com/user-attachments/assets/880951aa-11af-462b-ba6e-1a4621b09d97)

#File Storage
- Tasks are stored in a tasks.json file in the data folder. If the file doesn't exist, it will be created when the application starts.

#JSON File Format
- The JSON file contains a list of tasks with the following structure:

[
  {
    "Id": 1,
    "Title": "Task 1",
    "Description": "Description of Task 1",
    "CreatedAt": "2024-11-27T12:00:00",
    "UpdatedAt": "2024-11-27T12:00:00",
    "DueTime": "2024-11-29T12:00:00",
    "Status": "Undone"
  }
]





