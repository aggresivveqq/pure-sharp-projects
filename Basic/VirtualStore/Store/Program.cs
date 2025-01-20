using Store.Interfaces;
using Store.Repository;
using Store.ui;
//repos
IUserRepository userRepository = new UserRepository();
IProductRepository productRepository = new ProductRepository();
IOrderRepository orderRepository = new OrderRepository();
ICartRepository cartRepository = new CartRepository();

//init menu and adminpanel
AdminPanel adminPanel = new AdminPanel(userRepository, productRepository, orderRepository, cartRepository);
Menu menu = new Menu(userRepository, productRepository, orderRepository, cartRepository, adminPanel);

// mainmenu start
menu.ShowMainMenu();