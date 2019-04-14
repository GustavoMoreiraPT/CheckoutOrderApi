# CheckoutOrderApi

The starting page contains additional information on how to use the endpoints. Each endpoint has a short description for it's purpose, and each parameter also contains an explanation.

For the purpose of this prototype, there are 3 main entities to have in consideration:
Customer - Composed by Id, email and password. An order is always associated to a specific customer.
Items - The items that can be used inside of an order. An item is composed by: Name, Category, Id and Price.
Order - The actual order that a costumer can create, update, read and delete. 

For this demo, 2 customers have been added by default, which should be used in order to create new orders (By default, there is no way
to add new customers unless we manually changed the code.)
Note: the email and password are there just to add additional information and to look real, because they will never be used (For now).

Customer number 1:

Id = 8ffbacec-4ad8-4ef0-a016-1b90b35a37e9
Email = "arminvanbuuren@netherlands.com",
Password = "SunnyDays"

Customer number 2:

Id = eed9980b-f90c-47fb-8c2f-9708b0ec636c
Email = "avicii@sweden.com",
Password = "SOS"

There are also 3 items that can be consulted from the items endpoint:

Item number 1:

Id = 14ad7315-dd47-462c-ae05-925289420250"
Name = "Ultra Music Festival",
Category = "Eletronic Dance Music",
Price = 299

Item number 2:

Id = 89d6f5bf-48d5-47bf-863f-f8b99f78d4c7
Name = "Tomorrowland",
Category = "Eletronic Dance Music",
Price = 699

Item number 3:

Id = bd88ce04-6e43-479e-a39c-2d95b77aa97f
Name = "Creamfields London",
Category = "Eletronic Dance Music",
Price = 99
