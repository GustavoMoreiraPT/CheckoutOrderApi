# CheckoutOrderApi

For the purpose of this prototype, there are 3 main entities to have in consideration:
Customer - Composed by Id, email and password. An order is always associated to a specific customer.
Items - The items that can be used inside of an order. An item is composed by: Name, Category, Id and Price.
Order - The actual order that a costumer can create, update, read and delete. 

For this demo, 2 costumers have been added by default, which should be used in order to create new orders (By default, there is no way
to add new customers unless we manually changed the code.)
Note: the email and password are there just to add aditional information and to look real, because they will never be used (For now).

Customer number 1:

Id = 8ffbacec-4ad8-4ef0-a016-1b90b35a37e9
Email = "arminvanbuuren@netherlands.com",
Password = "SunnyDays"

Customer number 2:

Id = eed9980b-f90c-47fb-8c2f-9708b0ec636c
Email = "avicii@sweden.com",
Password = "SOS"
