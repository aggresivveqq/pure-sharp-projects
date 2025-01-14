using VirtStore.repository;
using VirtStore.service;
using VirtStore.UI;

var userRepository = new UserRepository(); 
var userService = new UserService(userRepository);


var mainMenu = new MainMenu(userService);

mainMenu.Show();